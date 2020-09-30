import { Apollo, gql } from 'apollo-angular';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { FinancialYear } from 'src/models/financial-year';
import { StartingBalance } from 'src/models/starting-balance';

const listQuery = gql`
  query getFinancialYears {
    financialYears {
      id
      name
      isActive
      dateCreated
    }
  }`;

@Injectable({
  providedIn: 'root'
})
export class FinancialYearService {

  constructor(private apollo: Apollo) { }

  getFinancialYears(): Observable<FinancialYear[]> {
    return this.apollo
      .watchQuery<any>({ query: listQuery })
      .valueChanges.pipe(map(({ data }) => data.financialYears));
  }

  getStartingBalances(financialYearId: string): Observable<StartingBalance[]> {
    return this.apollo
      .query<any>({
        query: gql`
        query getStartingBalances($financialYearId: GUID!) {
          startingBalances(financialYearId: $financialYearId) {
            accountId
            financialYearId
            startingBalanceInCents
          }
        }`,
        variables: { financialYearId: financialYearId }
      })
      .pipe(map(({ data }) => data.startingBalances));
  }

  addFinancialYear(name: string, previousFinancialYearId: string = null, refetchList: boolean = true): Observable<void> {
    return this.apollo
      .mutate({
        mutation: gql`
            mutation addFinancialYear($parameters: AddFinancialYearParameters!) {
              financialYear {
                add(parameters: $parameters) {
                  correlationId
                }
              }
            }`,
        variables: {
          parameters: {
            name: name,
            previousFinancialYearId: previousFinancialYearId
          }
        },
        refetchQueries: refetchList ? [{ query: listQuery }] : []
      }).pipe(map(_ => { }));
  }

  activateFinancialYear(id: string, refetchList: boolean = true): Observable<void> {
    return this.apollo
      .mutate({
        mutation: gql`
          mutation activateFinancialYear($parameters: ActivateFinancialYearParameters!) {
            financialYear {
              activate(parameters: $parameters) {
                correlationId
              }
            }
          }`,
        variables: {
          parameters: {
            id: id
          }
        },
        refetchQueries: refetchList ? [{ query: listQuery }] : []
      }).pipe(map(_ => { }));
  }
}

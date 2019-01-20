import { Injectable } from '@angular/core';
import { Apollo } from 'apollo-angular';
import { FinancialYear } from 'src/models/financial-year';
import gql from 'graphql-tag';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

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
      });
  }
}

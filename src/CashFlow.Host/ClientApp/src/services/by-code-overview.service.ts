import { Apollo, gql } from 'apollo-angular';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { CodeBalance } from './../models/code-balance';
import { Transaction } from './../models/transaction';

@Injectable({
  providedIn: 'root'
})
export class ByCodeOverviewService {

  constructor(private apollo: Apollo) { }

  getCodeBalances(financialYearId: string): Observable<CodeBalance[]> {
    return this.apollo
      .watchQuery<any>({
        query: gql`
          query getCodeBalances($financialYearId: Uuid) {
            codeBalances(financialYearId: $financialYearId) {
              name
              totalExpenseInCents
              totalIncomeInCents
              balanceInCents
            }
          }`,
        variables: { financialYearId: financialYearId }
      })
      .valueChanges.pipe(map(({ data }) => data.codeBalances));
  }

  getTransactions(financialYearId: string, codeName: string): Observable<Transaction[]> {
    return this.apollo
      .watchQuery<any>({
        query: gql`
          query getCodeTransactions($financialYearId: Uuid, $codeName: String!) {
            codeTransactions(financialYearId: $financialYearId, codeName: $codeName) {
              id
              transactionDate
              amountInCents
              isInternalTransfer
              description
            }
          }`,
        variables: {
          financialYearId: financialYearId,
          codeName: codeName
        }
      })
      .valueChanges.pipe(map(({ data }) => data.codeTransactions));
  }
}

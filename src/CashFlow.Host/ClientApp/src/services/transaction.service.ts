import { Injectable } from '@angular/core';
import { Apollo } from 'apollo-angular';
import gql from 'graphql-tag';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Transaction } from './../models/transaction';

const listQuery = gql`
  query getTransactions($financialYearId: GUID!) {
    transactions(financialYearId: $financialYearId) {
      id
      evidenceNumber
      accountId
      dateCreated
      dateModified
      amountInCents
      isInternalTransfer
      description
      comment
      codes {
        codeName
      }
      financialYear {
        id
        name
      }
      supplier {
        id
        name
      }
    }
  }`;

@Injectable({
  providedIn: 'root'
})
export class TransactionService {

  constructor(private apollo: Apollo) { }

  getTransactions(financialYearId: string): Observable<Transaction[]> {
    return this.apollo
      .watchQuery<any>({
        query: listQuery,
        variables: { financialYearId: financialYearId }
      })
      .valueChanges.pipe(map(({ data }) => data.transactions));
  }

  addTransaction(financialYearId: string, accountId: string, supplierId: string, amountInCents: number,
    description: string, comment: string, codeNames: string[], refetchList: boolean = true): Observable<void> {
    return this.apollo
      .mutate({
        mutation: gql`
        mutation addTransaction($parameters: AddTransactionParameters!) {
          transaction {
            add(parameters: $parameters) {
              correlationId
            }
          }
        }`,
        variables: {
          parameters: {
            financialYearId: financialYearId,
            accountId: accountId,
            supplierId: supplierId,
            amountInCents: amountInCents,
            description: description,
            comment: comment,
            codeNames: codeNames
          }
        },
        refetchQueries: refetchList ? [{ query: listQuery }] : []
      });
  }
}

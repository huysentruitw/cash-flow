import { Apollo, gql } from 'apollo-angular';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Transaction } from './../models/transaction';

const listQuery = gql`
  query getTransactions($financialYearId: GUID!) {
    transactions(financialYearId: $financialYearId) {
      __typename
      id
      evidenceNumber
      accountId
      transactionDate
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

  getEvidenceNumberSuggestionForTransaction(transactionId: string): Observable<string> {
    return this.apollo
      .query<any>({
        query: gql`
        query getEvidenceNumberSuggestionForTransaction($transactionId: GUID!) {
          suggestEvidenceNumberForTransaction(transactionId: $transactionId)
        }`,
        variables: { transactionId: transactionId }
      }).pipe(map(({ data }) => data.suggestEvidenceNumberForTransaction));
  }

  addIncome(financialYearId: string, transactionDate: Date, accountId: string, amountInCents: number,
    description: string, comment: string, codeNames: string[], refetchList: boolean = true): Observable<void> {
    return this.apollo
      .mutate({
        mutation: gql`
        mutation addIncome($parameters: AddIncomeTransactionParameters!) {
          transaction {
            addIncome(parameters: $parameters) {
              correlationId
            }
          }
        }`,
        variables: {
          parameters: {
            financialYearId: financialYearId,
            transactionDate: transactionDate,
            accountId: accountId,
            amountInCents: amountInCents,
            description: description,
            comment: comment,
            codeNames: codeNames
          }
        },
        refetchQueries: refetchList ? [{
          query: listQuery,
          variables: { financialYearId: financialYearId }
        }] : []
      }).pipe(map(_ => { }));
  }

  addExpense(financialYearId: string, transactionDate: Date, accountId: string, supplierId: string, amountInCents: number,
    description: string, comment: string, codeNames: string[], refetchList: boolean = true): Observable<void> {
    return this.apollo
      .mutate({
        mutation: gql`
        mutation addExpense($parameters: AddExpenseTransactionParameters!) {
          transaction {
            addExpense(parameters: $parameters) {
              correlationId
            }
          }
        }`,
        variables: {
          parameters: {
            financialYearId: financialYearId,
            transactionDate: transactionDate,
            accountId: accountId,
            supplierId: supplierId,
            amountInCents: amountInCents,
            description: description,
            comment: comment,
            codeNames: codeNames
          }
        },
        refetchQueries: refetchList ? [{
          query: listQuery,
          variables: { financialYearId: financialYearId }
        }] : []
      }).pipe(map(_ => { }));
  }

  addTransfer(financialYearId: string, transactionDate: Date, originAccountId: string, destinationAccountId: string, amountInCents: number,
    description: string, comment: string, codeNames: string[], refetchList: boolean = true): Observable<void> {
    return this.apollo
      .mutate({
        mutation: gql`
        mutation addTransfer($parameters: AddTransferTransactionParameters!) {
          transaction {
            addTransfer(parameters: $parameters) {
              correlationId
            }
          }
        }`,
        variables: {
          parameters: {
            financialYearId: financialYearId,
            transactionDate: transactionDate,
            originAccountId: originAccountId,
            destinationAccountId: destinationAccountId,
            amountInCents: amountInCents,
            description: description,
            comment: comment,
            codeNames: codeNames
          }
        },
        refetchQueries: refetchList ? [{
          query: listQuery,
          variables: { financialYearId: financialYearId }
        }] : []
      }).pipe(map(_ => { }));
  }

  assignCode(transactionId: string, codeName: string, financialYearId: string, refetchList: boolean = true): Observable<void> {
    return this.apollo
      .mutate({
        mutation: gql`
        mutation assignCodeToTransaction($parameters: AssignCodeToTransactionParameters!) {
          transaction {
            assignCode(parameters: $parameters) {
              correlationId
            }
          }
        }`,
        variables: {
          parameters: {
            id: transactionId,
            codeName: codeName
          }
        },
        refetchQueries: refetchList ? [{
          query: listQuery,
          variables: { financialYearId: financialYearId }
        }] : []
      }).pipe(map(_ => { }));
  }

  removeLatest(transactionId: string, financialYearId: string, refetchList: boolean = true): Observable<void> {
    return this.apollo
      .mutate({
        mutation: gql`
        mutation removeLatest($parameters: RemoveLatestTransactionParameters!) {
          transaction {
            removeLatest(parameters: $parameters) {
              correlationId
            }
          }
        }`,
        variables: {
          parameters: {
            id: transactionId
          }
        },
        refetchQueries: refetchList ? [{
          query: listQuery,
          variables: { financialYearId: financialYearId }
        }] : []
      }).pipe(map(_ => { }));
  }

  unassignCode(transactionId: string, codeName: string, financialYearId: string, refetchList: boolean = true): Observable<void> {
    return this.apollo
      .mutate({
        mutation: gql`
        mutation unassignCodeToTransaction($parameters: UnassignCodeFromTransactionParameters!) {
          transaction {
            unassignCode(parameters: $parameters) {
              correlationId
            }
          }
        }`,
        variables: {
          parameters: {
            id: transactionId,
            codeName: codeName
          }
        },
        refetchQueries: refetchList ? [{
          query: listQuery,
          variables: { financialYearId: financialYearId }
        }] : []
      }).pipe(map(_ => { }));
  }

  updateDescription(transactionId: string, description: string, financialYearId: string, refetchList: boolean = true): Observable<void> {
    return this.apollo
      .mutate({
        mutation: gql`
        mutation updateTransactionDescription($parameters: UpdateDescriptionOfTransactionParameters!) {
          transaction {
            updateDescription(parameters: $parameters) {
              correlationId
            }
          }
        }`,
        variables: {
          parameters: {
            id: transactionId,
            description: description
          }
        },
        refetchQueries: refetchList ? [{
          query: listQuery,
          variables: { financialYearId: financialYearId }
        }] : []
      }).pipe(map(_ => { }));
  }

  assignEvidenceNumber(transactionId: string, evidenceNumber: string, financialYearId: string, refetchList: boolean = true): Observable<void> {
    return this.apollo
      .mutate({
        mutation: gql`
        mutation assignEvidenceNumberToTransaction($parameters: AssignEvidenceNumberToTransactionParameters!) {
          transaction {
            assignEvidenceNumber(parameters: $parameters) {
              correlationId
            }
          }
        }`,
        variables: {
          parameters: {
            id: transactionId,
            evidenceNumber: evidenceNumber
          }
        },
        refetchQueries: refetchList ? [{
          query: listQuery,
          variables: { financialYearId: financialYearId }
        }] : []
      }).pipe(map(_ => { }));
  }

  unassignEvidenceNumber(transactionId: string, financialYearId: string, refetchList: boolean = true): Observable<void> {
    return this.apollo
      .mutate({
        mutation: gql`
        mutation unassignEvidenceNumberFromTransaction($parameters: UnassignEvidenceNumberFromTransactionParameters!) {
          transaction {
            unassignEvidenceNumber(parameters: $parameters) {
              correlationId
            }
          }
        }`,
        variables: {
          parameters: {
            id: transactionId
          }
        },
        refetchQueries: refetchList ? [{
          query: listQuery,
          variables: { financialYearId: financialYearId }
        }] : []
      }).pipe(map(_ => { }));
  }
}

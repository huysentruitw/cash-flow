import { Injectable } from '@angular/core';
import { Apollo } from 'apollo-angular';
import gql from "graphql-tag";
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { CodeBalance } from './../models/code-balance';

const listQuery = gql`
  query getCodeBalances($financialYearId: GUID!) {
    codeBalances(financialYearId: $financialYearId) {
      name
      totalExpenseInCents
      totalIncomeInCents
      balanceInCents
    }
  }`;

@Injectable({
  providedIn: 'root'
})
export class ByCodeOverviewService {

  constructor(private apollo: Apollo) { }

  getCodeBalances(financialYearId: string): Observable<CodeBalance[]> {
    return this.apollo
      .watchQuery<any>({
        query: listQuery,
        variables: { financialYearId: financialYearId }
      })
      .valueChanges.pipe(map(({ data }) => data.codeBalances));
  }
}

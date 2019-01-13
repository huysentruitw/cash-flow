import { Injectable } from '@angular/core';
import { Apollo } from 'apollo-angular';
import gql from "graphql-tag";
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Account } from './../models/account';

class ListReponse {
  accounts: Account[];
}

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private apollo: Apollo) { }

  getAccounts(): Observable<Account[]> {
    return this.apollo
      .watchQuery<ListReponse>({
        query: gql`
        {
          accounts {
            id
            name
            type
            dateCreated
            dateModified
          }
        }`
      })
      .valueChanges.pipe(map(({ data }) => data.accounts));
  }
}

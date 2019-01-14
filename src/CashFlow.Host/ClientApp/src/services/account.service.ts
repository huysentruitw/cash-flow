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
      .query<ListReponse>({
        query: gql`
        query getAccounts {
          accounts {
            id
            name
            type
            dateCreated
            dateModified
          }
        }`
      })
      .pipe(map(({ data }) => data.accounts));
  }

  addAccount(name: string, type: string): Observable<void> {
    return this.apollo
      .mutate({
        mutation: gql`
        mutation addAcount($parameters: AddAccountParameters!) {
          account {
            add(parameters: $parameters) {
              correlationId
            }
          }
        }`,
        variables: {
          parameters: {
            name: name,
            type: type
          }
        }
      });
  }

  renameAccount(id: string, name: string): Observable<void> {
    return this.apollo
      .mutate({
        mutation: gql`
        mutation renameAccount($parameters: RenameAccountParameters!) {
          account {
            rename(parameters: $parameters) {
              correlationId
            }
          }
        }`,
        variables: {
          parameters: {
            id: id,
            name: name
          }
        }
      });
  }

  changeAccountType(id: string, type: string): Observable<void> {
    return this.apollo
      .mutate({
        mutation: gql`
        mutation changeAccountType($parameters: ChangeAccountTypeParameters!) {
          account {
            changeType(parameters: $parameters) {
              correlationId
            }
          }
        }`,
        variables: {
          parameters: {
            id: id,
            type: type
          }
        }
      });
  }

  removeAccount(id: string): Observable<void> {
    return this.apollo
      .mutate({
        mutation: gql`
        mutation removeAccount($parameters: RemoveAccountParameters!) {
          account {
            remove(parameters: $parameters) {
              correlationId
            }
          }
        }`,
        variables: {
          parameters: {
            id: id
          }
        }
      });
  }
}

import { Injectable } from '@angular/core';
import { Apollo } from 'apollo-angular';
import gql from "graphql-tag";
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Account } from './../models/account';

const listQuery = gql`
  query getAccounts {
    accounts {
      id
      name
      type
      dateCreated
      dateModified
    }
  }`;

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private apollo: Apollo) { }

  getAccounts(): Observable<Account[]> {
    return this.apollo
      .watchQuery<any>({ query: listQuery })
      .valueChanges.pipe(map(({ data }) => data.accounts));
  }

  addAccount(name: string, type: string, refetchList: boolean = true): Observable<void> {
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
        },
        refetchQueries: refetchList ? [{ query: listQuery }] : []
      });
  }

  renameAccount(id: string, name: string, refetchList: boolean = true): Observable<void> {
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
        },
        refetchQueries: refetchList ? [{ query: listQuery }] : []
      });
  }

  changeAccountType(id: string, type: string, refetchList: boolean = true): Observable<void> {
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
        },
        refetchQueries: refetchList ? [{ query: listQuery }] : []
      });
  }

  removeAccount(id: string, refetchList: boolean = true): Observable<void> {
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
        },
        refetchQueries: refetchList ? [{ query: listQuery }] : []
      });
  }
}

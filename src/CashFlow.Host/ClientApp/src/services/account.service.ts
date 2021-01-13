import { Apollo, gql } from 'apollo-angular';
import { Injectable } from '@angular/core';
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
        mutation addAcount($input: AddAccountInput!) {
          account {
            add(input: $input) {
              correlationId
            }
          }
        }`,
        variables: {
          input: {
            name: name,
            type: type
          }
        },
        refetchQueries: refetchList ? [{ query: listQuery }] : []
      }).pipe(map(_ => { }));
  }

  renameAccount(id: string, name: string, refetchList: boolean = true): Observable<void> {
    return this.apollo
      .mutate({
        mutation: gql`
        mutation renameAccount($input: RenameAccountInput!) {
          account {
            rename(input: $input) {
              correlationId
            }
          }
        }`,
        variables: {
          input: {
            id: id,
            name: name
          }
        },
        refetchQueries: refetchList ? [{ query: listQuery }] : []
      }).pipe(map(_ => { }));
  }

  changeAccountType(id: string, type: string, refetchList: boolean = true): Observable<void> {
    return this.apollo
      .mutate({
        mutation: gql`
        mutation changeAccountType($input: ChangeAccountTypeInput!) {
          account {
            changeType(input: $input) {
              correlationId
            }
          }
        }`,
        variables: {
          input: {
            id: id,
            type: type
          }
        },
        refetchQueries: refetchList ? [{ query: listQuery }] : []
      }).pipe(map(_ => { }));
  }

  removeAccount(id: string, refetchList: boolean = true): Observable<void> {
    return this.apollo
      .mutate({
        mutation: gql`
        mutation removeAccount($input: RemoveAccountInput!) {
          account {
            remove(input: $input) {
              correlationId
            }
          }
        }`,
        variables: {
          input: {
            id: id
          }
        },
        refetchQueries: refetchList ? [{ query: listQuery }] : []
      }).pipe(map(_ => { }));
  }
}

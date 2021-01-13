import { Apollo, gql } from 'apollo-angular';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Code } from './../models/code';

const listQuery = gql`
  query getCodes {
    codes {
      name
      dateCreated
    }
  }`;

@Injectable({
  providedIn: 'root'
})
export class CodeService {

  constructor(private apollo: Apollo) { }

  getCodes(): Observable<Code[]> {
    return this.apollo
      .watchQuery<any>({ query: listQuery })
      .valueChanges.pipe(map(({ data }) => data.codes));
  }

  addCode(name: string, refetchList: boolean = true): Observable<void> {
    return this.apollo
      .mutate({
        mutation: gql`
            mutation addCode($input: AddCodeInput!) {
              code {
                add(input: $input) {
                  correlationId
                }
              }
            }`,
        variables: {
          input: {
            name: name
          }
        },
        refetchQueries: refetchList ? [{ query: listQuery }] : []
      }).pipe(map(_ => { }));
  }

  renameCode(originalName: string, newName: string, refetchList: boolean = true): Observable<void> {
    return this.apollo
      .mutate({
        mutation: gql`
            mutation renameCode($input: RenameCodeInput!) {
              code {
                rename(input: $input) {
                  correlationId
                }
              }
            }`,
        variables: {
          input: {
            originalName: originalName,
            newName: newName
          }
        },
        refetchQueries: refetchList ? [{ query: listQuery }] : []
      }).pipe(map(_ => { }));
  }

  removeCode(name: string, refetchList: boolean = true): Observable<void> {
    return this.apollo
      .mutate({
        mutation: gql`
            mutation removeCode($input: RemoveCodeInput!) {
              code {
                remove(input: $input) {
                  correlationId
                }
              }
            }`,
        variables: {
          input: {
            name: name
          }
        },
        refetchQueries: refetchList ? [{ query: listQuery }] : []
      }).pipe(map(_ => { }));
  }
}

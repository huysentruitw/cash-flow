import { Injectable } from '@angular/core';
import { Apollo } from 'apollo-angular';
import gql from "graphql-tag";
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
            mutation addCode($parameters: AddCodeParameters!) {
              code {
                add(parameters: $parameters) {
                  correlationId
                }
              }
            }`,
        variables: {
          parameters: {
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
            mutation renameCode($parameters: RenameCodeParameters!) {
              code {
                rename(parameters: $parameters) {
                  correlationId
                }
              }
            }`,
        variables: {
          parameters: {
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
            mutation removeCode($parameters: RemoveCodeParameters!) {
              code {
                remove(parameters: $parameters) {
                  correlationId
                }
              }
            }`,
        variables: {
          parameters: {
            name: name
          }
        },
        refetchQueries: refetchList ? [{ query: listQuery }] : []
      }).pipe(map(_ => { }));
  }
}

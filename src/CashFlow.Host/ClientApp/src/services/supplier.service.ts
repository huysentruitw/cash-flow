import { Injectable } from '@angular/core';
import { Apollo } from 'apollo-angular';
import gql from "graphql-tag";
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Supplier } from './../models/supplier';

const listQuery = gql`
  query getSuppliers {
    suppliers {
      id
      name
      contactInfo
      dateCreated
      dateModified
    }
  }`;

@Injectable({
  providedIn: 'root'
})
export class SupplierService {

  constructor(private apollo: Apollo) { }

  getSuppliers(): Observable<Supplier[]> {
    return this.apollo
      .watchQuery<any>({ query: listQuery })
      .valueChanges.pipe(map(({ data }) => data.suppliers));
  }

  addSupplier(name: string, contactInfo: string, refetchList: boolean = true): Observable<void> {
    return this.apollo
      .mutate({
        mutation: gql`
        mutation addSupplier($parameters: AddSupplierParameters!) {
          supplier {
            add(parameters: $parameters) {
              correlationId
            }
          }
        }`,
        variables: {
          parameters: {
            name: name,
            contactInfo: contactInfo
          }
        },
        refetchQueries: refetchList ? [{ query: listQuery }] : []
      }).pipe(map(_ => { }));
  }

  renameSupplier(id: string, name: string, refetchList: boolean = true): Observable<void> {
    return this.apollo
      .mutate({
        mutation: gql`
        mutation renameSupplier($parameters: RenameSupplierParameters!) {
          supplier {
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
      }).pipe(map(_ => { }));
  }

  updateContactInfo(id: string, contactInfo: string, refetchList: boolean = true): Observable<void> {
    return this.apollo
      .mutate({
        mutation: gql`
        mutation updateSupplierContactInfo($parameters: UpdateSupplierContactInfoParameters!) {
          supplier {
            updateContactInfo(parameters: $parameters) {
              correlationId
            }
          }
        }`,
        variables: {
          parameters: {
            id: id,
            contactInfo: contactInfo
          }
        },
        refetchQueries: refetchList ? [{ query: listQuery }] : []
      }).pipe(map(_ => { }));
  }

  removeSupplier(id: string, refetchList: boolean = true): Observable<void> {
    return this.apollo
      .mutate({
        mutation: gql`
        mutation removeSupplier($parameters: RemoveSupplierParameters!) {
          supplier {
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
      }).pipe(map(_ => { }));
  }
}

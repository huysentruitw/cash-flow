import { Apollo, gql } from 'apollo-angular';
import { Injectable } from '@angular/core';
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
        mutation addSupplier($input: AddSupplierInput!) {
          supplier {
            add(input: $input) {
              correlationId
            }
          }
        }`,
        variables: {
          input: {
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
        mutation renameSupplier($input: RenameSupplierInput!) {
          supplier {
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

  updateContactInfo(id: string, contactInfo: string, refetchList: boolean = true): Observable<void> {
    return this.apollo
      .mutate({
        mutation: gql`
        mutation updateSupplierContactInfo($input: UpdateSupplierContactInfoInput!) {
          supplier {
            updateContactInfo(input: $input) {
              correlationId
            }
          }
        }`,
        variables: {
          input: {
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
        mutation removeSupplier($input: RemoveSupplierInput!) {
          supplier {
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

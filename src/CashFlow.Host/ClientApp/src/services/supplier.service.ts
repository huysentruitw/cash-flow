import { Injectable } from '@angular/core';
import { Apollo } from 'apollo-angular';
import gql from "graphql-tag";
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Supplier } from './../models/supplier';

class ListReponse {
  suppliers: Supplier[];
}

@Injectable({
  providedIn: 'root'
})
export class SupplierService {

  constructor(private apollo: Apollo) { }

  getSuppliers(): Observable<Supplier[]> {
    return this.apollo
      .query<ListReponse>({
        query: gql`
        {
          suppliers {
            id
            name
            contactInfo
            dateCreated
            dateModified
          }
        }`
      })
      .pipe(map(({ data }) => data.suppliers));
  }

}

import { Injectable } from '@angular/core';
import { Apollo } from 'apollo-angular';
import gql from "graphql-tag";
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Code } from './../models/code';

class ListReponse {
  codes: Code[];
}

@Injectable({
  providedIn: 'root'
})
export class CodeService {

  constructor(private apollo: Apollo) { }

  getCodes(): Observable<Code[]> {
    return this.apollo
      .query<ListReponse>({
        query: gql`
        {
          codes {
            name
            dateCreated
          }
        }`
      })
      .pipe(map(({ data }) => data.codes));
  }

}

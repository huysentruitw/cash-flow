import { Component } from '@angular/core';
import { Apollo } from 'apollo-angular';
import gql from "graphql-tag";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'ClientApp';

  constructor(private apollo: Apollo) {}

  ngOnInit() {
    this.apollo
      .watchQuery({
        query: gql`
          {
            userInfo {
              name
            }
          }
        `
      })
      .valueChanges.subscribe(result => {
        console.log(result);
      });
  }

}

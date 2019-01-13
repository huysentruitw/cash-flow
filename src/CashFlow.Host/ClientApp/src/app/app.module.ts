import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { GraphQLModule } from "./graphql.module";
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { NgModule, LOCALE_ID } from '@angular/core';

import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { MatToolbarModule } from '@angular/material/toolbar';
import { AccountListComponent } from './account-list/account-list.component';
import { CodeListComponent } from './code-list/code-list.component';
import { SupplierListComponent } from './supplier-list/supplier-list.component';
import { AccountDialogComponent } from './account-dialog/account-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    AccountListComponent,
    CodeListComponent,
    SupplierListComponent,
    AccountDialogComponent
  ],
  imports: [
    AppRoutingModule,
    BrowserAnimationsModule,
    GraphQLModule,
    HttpClientModule,

    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: (http: HttpClient) => new TranslateHttpLoader(http, './assets/i18n/', '.json'),
        deps: [HttpClient]
      }
    }),

    MatButtonModule,
    MatDialogModule,
    MatIconModule,
    MatTableModule,
    MatTabsModule,
    MatToolbarModule
  ],
  entryComponents: [
    AccountDialogComponent
  ],
  providers: [
    { provide: LOCALE_ID, useValue: 'nl-BE' },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

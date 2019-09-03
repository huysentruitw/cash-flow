import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { GraphQLModule } from "./graphql.module";
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { NgModule, LOCALE_ID } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { MaterialModule } from './material.module';

import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

import { AccountDialogComponent } from './account-dialog/account-dialog.component';
import { AccountListComponent } from './account-list/account-list.component';
import { AppComponent } from './app.component';
import { CodeDialogComponent } from './code-dialog/code-dialog.component';
import { CodeListComponent } from './code-list/code-list.component';
import { ConfirmationDialogComponent } from './confirmation-dialog/confirmation-dialog.component';
import { FinancialYearDialogComponent } from './financial-year-dialog/financial-year-dialog.component';
import { FinancialYearSelectorComponent } from './financial-year-selector/financial-year-selector.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { SupplierDialogComponent } from './supplier-dialog/supplier-dialog.component';
import { SupplierListComponent } from './supplier-list/supplier-list.component';
import { ToolbarComponent } from './toolbar/toolbar.component';
import { TransactionCodeDialogComponent } from './transaction-code-dialog/transaction-code-dialog.component';
import { TransactionDialogComponent } from './transaction-dialog/transaction-dialog.component';
import { TransactionListComponent } from './transaction-list/transaction-list.component';
import { MAT_MOMENT_DATE_ADAPTER_OPTIONS } from '@angular/material-moment-adapter';
import { TransactionDescriptionDialogComponent } from './transaction-description-dialog/transaction-description-dialog.component';
import { TransactionEvidenceNumberDialogComponent } from './transaction-evidence-number-dialog/transaction-evidence-number-dialog.component';

@
NgModule({
  declarations: [
    AccountDialogComponent,
    AccountListComponent,
    AppComponent,
    CodeDialogComponent,
    CodeListComponent,
    ConfirmationDialogComponent,
    FinancialYearDialogComponent,
    FinancialYearSelectorComponent,
    SidebarComponent,
    SupplierDialogComponent,
    SupplierListComponent,
    ToolbarComponent,
    TransactionCodeDialogComponent,
    TransactionDescriptionDialogComponent,
    TransactionDialogComponent,
    TransactionEvidenceNumberDialogComponent,
    TransactionListComponent
  ],
  imports: [
    AppRoutingModule,
    BrowserAnimationsModule,
    FormsModule,
    GraphQLModule,
    HttpClientModule,
    MaterialModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: (http: HttpClient) => new TranslateHttpLoader(http, './assets/i18n/', '.json'),
        deps: [HttpClient]
      }
    })
  ],
  entryComponents: [
    AccountDialogComponent,
    CodeDialogComponent,
    ConfirmationDialogComponent,
    FinancialYearDialogComponent,
    SupplierDialogComponent,
    TransactionCodeDialogComponent,
    TransactionDescriptionDialogComponent,
    TransactionDialogComponent,
    TransactionEvidenceNumberDialogComponent
  ],
  providers: [
    { provide: LOCALE_ID, useValue: 'nl-BE' },
    { provide: MAT_MOMENT_DATE_ADAPTER_OPTIONS, useValue: { useUtc: true } }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

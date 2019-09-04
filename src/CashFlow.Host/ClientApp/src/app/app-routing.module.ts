import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AccountListComponent } from './account-list/account-list.component';
import { ByCodeOverviewComponent } from './by-code-overview/by-code-overview.component';
import { CodeListComponent } from './code-list/code-list.component';
import { SupplierListComponent } from './supplier-list/supplier-list.component';
import { TransactionListComponent } from './transaction-list/transaction-list.component';

const routes: Routes = [
  {
    path: 'accounts',
    component: AccountListComponent
  },
  {
    path: 'codes',
    component: CodeListComponent
  },
  {
    path: 'overview/by/code',
    component: ByCodeOverviewComponent
  },
  {
    path: 'suppliers',
    component: SupplierListComponent
  },
  {
    path: 'transactions',
    component: TransactionListComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

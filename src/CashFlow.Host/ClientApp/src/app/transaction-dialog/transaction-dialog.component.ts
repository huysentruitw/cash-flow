import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { Account } from 'src/models/account';
import { FinancialYear } from 'src/models/financial-year';
import { Supplier } from 'src/models/supplier';
import { AccountService } from 'src/services/account.service';
import { FinancialYearService } from 'src/services/financial-year.service';
import { SupplierService } from 'src/services/supplier.service';

export class DialogData {
  financialYearId: string;
  accountId: string;
  supplierId: string;
  amount: number;
  amountInCents: number;
  description: string;
  comment: string;
  codeNames: string[];
}

@Component({
  selector: 'app-transaction-dialog',
  templateUrl: './transaction-dialog.component.html',
  styleUrls: ['./transaction-dialog.component.scss']
})
export class TransactionDialogComponent implements OnInit {
  accounts$: Observable<Account[]>;
  financialYears$: Observable<FinancialYear[]>;
  suppliers$: Observable<Supplier[]>;

  constructor(
    private accountService: AccountService,
    private financialYearService: FinancialYearService,
    private supplierService: SupplierService,
    private dialogRef: MatDialogRef<TransactionDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) { }

  ngOnInit(): void {
    this.data.amount = this.data.amountInCents / 100;
    this.accounts$ = this.accountService.getAccounts().pipe(take(1));
    this.financialYears$ = this.financialYearService.getFinancialYears().pipe(take(1));
    this.suppliers$ = this.supplierService.getSuppliers().pipe(take(1));
  }

  updateAmount(value) {
    this.data.amountInCents = value * 100;
  }

  get isValid(): boolean {
    return !!this.data.financialYearId
      && !!this.data.accountId
      && !!this.data.amountInCents
      && !!this.data.description;
  }
}

import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { TranslateService } from '@ngx-translate/core';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { Account } from 'src/models/account';
import { FinancialYear } from 'src/models/financial-year';
import { Supplier } from 'src/models/supplier';
import { AccountService } from 'src/services/account.service';
import { SupplierService } from 'src/services/supplier.service';
import * as moment from 'moment';

export enum TransactionMode {
  Income,
  Expense,
  Transfer
}

export class DialogData {
  mode: TransactionMode;
  financialYear: FinancialYear;
  transactionDate: Date;
  accountId: string;
  originAccountId: string;
  destinationAccountId: string;
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
  transactionMode = TransactionMode;
  accounts$: Observable<Account[]>;
  suppliers$: Observable<Supplier[]>;

  constructor(
    private accountService: AccountService,
    private supplierService: SupplierService,
    private translate: TranslateService,
    private dialogRef: MatDialogRef<TransactionDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) { }

  ngOnInit(): void {
    this.data.amount = this.data.amountInCents / 100.0;
    this.data.transactionDate = this.data.transactionDate || moment.utc().startOf('day').toDate();
    this.accounts$ = this.accountService.getAccounts().pipe(take(1));
    this.suppliers$ = this.supplierService.getSuppliers().pipe(take(1));
  }

  updateAmount(value) {
    this.data.amountInCents = value * 100.0;
  }

  get isValid(): boolean {
    if (this.data.mode === TransactionMode.Transfer) {
      return !!this.data.originAccountId
        && !!this.data.destinationAccountId
        && this.data.amountInCents > 0
        && !!this.data.description;
    }

    return !!this.data.accountId
      && this.data.amountInCents > 0
      && !!this.data.description;
  }

  get title(): string {
    switch (this.data.mode) {
      case TransactionMode.Income:
        return this.translate.instant('Add income for {{financialYear}}', { financialYear: this.data.financialYear.name });
      case TransactionMode.Expense:
        return this.translate.instant('Add expense for {{financialYear}}', { financialYear: this.data.financialYear.name });
      case TransactionMode.Transfer:
        return this.translate.instant('Add transfer for {{financialYear}}', { financialYear: this.data.financialYear.name });
      default:
        throw new Error('Unknown mode');
    }
  }
}

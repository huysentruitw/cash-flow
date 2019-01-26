import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { switchMap, takeUntil, tap } from 'rxjs/operators';
import { FinancialYear } from 'src/models/financial-year';
import { TransactionWithBalance } from 'src/models/transaction-with-balance';
import { TransactionService } from 'src/services/transaction.service';
import { AppComponent } from '../app.component';

@Component({
  selector: 'app-transaction-list',
  templateUrl: './transaction-list.component.html',
  styleUrls: ['./transaction-list.component.scss']
})
export class TransactionListComponent implements OnInit, OnDestroy {
  private destroy$ = new Subject<void>();
  private financialYear$: Observable<FinancialYear>;
  displayedColumns = ['date', 'evidenceNumber', 'codes', 'description', 'income', 'expense', 'balance'];
  transactions$: Observable<TransactionWithBalance[]>;

  constructor(app: AppComponent, private transactionService: TransactionService) {
    this.financialYear$ = app.financialYear$.pipe(takeUntil(this.destroy$));
  }

  ngOnInit(): void {
    this.transactions$ = this.financialYear$
      .pipe(
      switchMap(financialYear => this.transactionService.getTransactions<TransactionWithBalance>(financialYear.id)),
      tap(transactions => {
        var balanceInCents = 0;
        transactions.forEach(transaction => {
          balanceInCents += transaction.amountInCents;
          transaction.balanceInCents = balanceInCents;
        });
      }));
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

}

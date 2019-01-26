import { Component, OnDestroy, OnInit } from '@angular/core';
import { combineLatest, Observable, Subject } from 'rxjs';
import { filter, map, switchMap, takeUntil } from 'rxjs/operators';
import { FinancialYear } from 'src/models/financial-year';
import { TransactionWithBalance } from 'src/models/transaction-with-balance';
import { BusService } from 'src/services/bus.service';
import { FinancialYearService } from 'src/services/financial-year.service';
import { TransactionService } from 'src/services/transaction.service';

@Component({
  selector: 'app-transaction-list',
  templateUrl: './transaction-list.component.html',
  styleUrls: ['./transaction-list.component.scss']
})
export class TransactionListComponent implements OnInit, OnDestroy {
  private destroy$ = new Subject<void>();
  private financialYear$: Observable<FinancialYear>;
  private startingBalance$: Observable<number>;
  displayedColumns = ['date', 'evidenceNumber', 'codes', 'description', 'income', 'expense', 'balance'];
  transactions$: Observable<TransactionWithBalance[]>;

  constructor(
    private transactionService: TransactionService,
    private financialYearService: FinancialYearService,
    private busService: BusService) { }

  ngOnInit(): void {
    this.financialYear$ = this.busService.activeFinancialYear$
      .pipe(takeUntil(this.destroy$), filter(financialYear => !!financialYear));

    this.startingBalance$ = this.financialYear$.pipe(
      switchMap(financialYear => this.financialYearService.getStartingBalances(financialYear.id)),
      map(startingBalances => startingBalances.reduce((acc, startingBalance) => acc + startingBalance.startingBalanceInCents, 0))
    );
 
    const transactions$ = this.financialYear$.pipe(
      switchMap(financialYear => this.transactionService.getTransactions(financialYear.id))
    );

    this.transactions$ = combineLatest(this.startingBalance$, transactions$).pipe(
      map(([balanceInCents, transactions]) => transactions.map(transaction => {
        const transactionWithBalance = <TransactionWithBalance>transaction;
        balanceInCents += transaction.amountInCents;
        transactionWithBalance.balanceInCents = balanceInCents;
        return transactionWithBalance;
      }))
    );
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

}

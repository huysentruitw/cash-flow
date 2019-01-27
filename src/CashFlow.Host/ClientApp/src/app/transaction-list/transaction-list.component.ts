import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';
import { BehaviorSubject, combineLatest, Observable, Subject } from 'rxjs';
import { filter, map, switchMap, take, takeUntil } from 'rxjs/operators';
import { Account } from 'src/models/account';
import { FinancialYear } from 'src/models/financial-year';
import { Transaction } from 'src/models/transaction';
import { TransactionWithBalance } from 'src/models/transaction-with-balance';
import { AccountService } from 'src/services/account.service';
import { BusService } from 'src/services/bus.service';
import { FinancialYearService } from 'src/services/financial-year.service';
import { TransactionService } from 'src/services/transaction.service';
import { TransactionDialogComponent } from '../transaction-dialog/transaction-dialog.component';

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
  accounts$: Observable<Account[]>;
  selectedAccount$ = new BehaviorSubject<Account>(null);

  constructor(
    private accountService: AccountService,
    private transactionService: TransactionService,
    private financialYearService: FinancialYearService,
    private busService: BusService,
    private dialog: MatDialog) { }

  ngOnInit(): void {
    this.initAccountStream();
    this.initFinancialYearStream();
    this.initStartingBalanceStream();
    this.initTransactionsStream();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
  
  selectAccount(account): void {
    this.selectedAccount$.next(account);
  }

  addTransaction(): void {
    combineLatest(this.selectedAccount$, this.busService.activeFinancialYear$)
      .pipe(take(1))
      .subscribe(([selectedAccount, financialYear]) => {
        const dialogRef = this.dialog.open(TransactionDialogComponent,
          {
            width: '600px',
            data: {
              financialYearId: financialYear.id,
              accountId: !!selectedAccount ? selectedAccount.id : null
            }
          });

        dialogRef.afterClosed().subscribe(result => {
          if (!!result) {
            this.transactionService.addTransaction(
              result.financialYearId,
              result.accountId,
              result.supplierId,
              result.amountInCents,
              result.description,
              result.comment,
              []).subscribe(
                () => { },
                error => {
                  console.error(error);
                });
          }
        });
      });
  }

  private initAccountStream(): void {
    this.accounts$ = this.accountService.getAccounts().pipe(take(1));
  }

  private initFinancialYearStream(): void {
    this.financialYear$ = this.busService.activeFinancialYear$
      .pipe(takeUntil(this.destroy$), filter(financialYear => !!financialYear));
  }

  private initStartingBalanceStream(): void {
    const startingBalances$ = this.financialYear$.pipe(
      switchMap(financialYear => this.financialYearService.getStartingBalances(financialYear.id))
    );

    this.startingBalance$ = combineLatest(this.selectedAccount$, startingBalances$).pipe(
      map(([selectedAccount, startingBalances]) => startingBalances.filter(x => !selectedAccount || x.accountId === selectedAccount.id)),
      map(startingBalances => startingBalances.reduce((acc, startingBalance) => acc + startingBalance.startingBalanceInCents, 0))
    );
  }

  private initTransactionsStream(): void {
    const transactions$ = this.financialYear$.pipe(
      switchMap(financialYear => this.transactionService.getTransactions(financialYear.id))
    );

    this.transactions$ = combineLatest(this.selectedAccount$, this.startingBalance$, transactions$).pipe(
      map<[Account, number, Transaction[]], [number, Transaction[]]>(([selectedAccount, balanceInCents, transactions]) => ([balanceInCents, transactions.filter(x => !selectedAccount || x.accountId === selectedAccount.id)])),
      map(([balanceInCents, transactions]) => transactions.map(transaction => {
        const transactionWithBalance = <TransactionWithBalance>transaction;
        balanceInCents += transaction.amountInCents;
        transactionWithBalance.balanceInCents = balanceInCents;
        return transactionWithBalance;
      }))
    );
  }
}

import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { BehaviorSubject, combineLatest, Observable, Subject } from 'rxjs';
import { filter, map, switchMap, take, takeUntil } from 'rxjs/operators';
import { Account } from 'src/models/account';
import { FinancialYear } from 'src/models/financial-year';
import { Transaction, TransactionCode } from 'src/models/transaction';
import { TransactionWithBalance } from 'src/models/transaction-with-balance';
import { AccountService } from 'src/services/account.service';
import { BusService } from 'src/services/bus.service';
import { FinancialYearService } from 'src/services/financial-year.service';
import { TransactionService } from 'src/services/transaction.service';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';
import { TransactionCodeDialogComponent } from '../transaction-code-dialog/transaction-code-dialog.component';
import { DialogData, TransactionDialogComponent, TransactionMode } from '../transaction-dialog/transaction-dialog.component';
import { TranslateService } from '@ngx-translate/core';
import { TransactionDescriptionDialogComponent } from '../transaction-description-dialog/transaction-description-dialog.component';
import { TransactionEvidenceNumberDialogComponent } from '../transaction-evidence-number-dialog/transaction-evidence-number-dialog.component';

@Component({
  selector: 'app-transaction-list',
  templateUrl: './transaction-list.component.html',
  styleUrls: ['./transaction-list.component.scss']
})
export class TransactionListComponent implements OnInit, OnDestroy {
  private destroy$ = new Subject<void>();
  private financialYear$: Observable<FinancialYear>;
  startingBalance$: Observable<number>;
  displayedColumns = ['date', 'evidenceNumber', 'codes', 'description', 'income', 'expense', 'balance', 'remove'];
  transactions$: Observable<TransactionWithBalance[]>;
  accounts$: Observable<Account[]>;
  selectedAccount$ = new BehaviorSubject<Account>(null);

  constructor(
    private accountService: AccountService,
    private transactionService: TransactionService,
    private financialYearService: FinancialYearService,
    private translateService: TranslateService,
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

  addIncome(): void {
    this.addTransactionDialog(TransactionMode.Income);
  }

  addExpense(): void {
    this.addTransactionDialog(TransactionMode.Expense);
  }

  addTransfer(): void {
    this.addTransactionDialog(TransactionMode.Transfer);
  }

  private addTransactionDialog(mode: TransactionMode): void {
    combineLatest([this.selectedAccount$, this.busService.activeFinancialYear$])
      .pipe(take(1))
      .subscribe(([selectedAccount, financialYear]) => {
        const dialogRef = this.dialog.open(TransactionDialogComponent,
          {
            width: '280px',
            data: {
              mode: mode,
              financialYear: financialYear,
              accountId: !!selectedAccount ? selectedAccount.id : null
            }
          });

        dialogRef.afterClosed()
          .pipe(
            filter(data => !!data),
            switchMap(result => this.addTransaction(result))
          )
          .subscribe(
            () => {},
            error => {
              console.error(error);
            });
        });
  }

  private addTransaction(dialogData: DialogData): Observable<void> {
    if (dialogData.mode === TransactionMode.Income) {
      return this.transactionService.addIncome(
        dialogData.financialYear.id,
        dialogData.transactionDate,
        dialogData.accountId,
        dialogData.amountInCents,
        dialogData.description,
        dialogData.comment,
        []);
    }

    if (dialogData.mode === TransactionMode.Expense) {
      return this.transactionService.addExpense(
        dialogData.financialYear.id,
        dialogData.transactionDate,
        dialogData.accountId,
        dialogData.supplierId,
        dialogData.amountInCents,
        dialogData.description,
        dialogData.comment,
        []);
    }

    if (dialogData.mode === TransactionMode.Transfer) {
      return this.transactionService.addTransfer(
        dialogData.financialYear.id,
        dialogData.transactionDate,
        dialogData.originAccountId,
        dialogData.destinationAccountId,
        dialogData.amountInCents,
        dialogData.description,
        dialogData.comment,
        []);
    }

    throw new Error(`Unknown transaction mode ${dialogData.mode}`);
  }

  isLatestTransaction(transactions: Transaction[], transaction: Transaction): boolean {
    return transactions.length > 0 && transaction === transactions[transactions.length - 1];
  }

  removeLatest(transaction: Transaction): void {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent,
      {
        width: '400px',
        data: {
          title: this.translateService.instant('Remove transaction {{description}}?', { description: transaction.description }),
          icon: 'delete'
        }
      });

    dialogRef.afterClosed().subscribe(result => {
      if (!!result) {
        this.transactionService.removeLatest(
          transaction.id,
          transaction.financialYear.id).subscribe(
          () => { },
          error => {
            console.error(error);
          });
      }
    });
  }

  assignCode(transaction: Transaction): void {
    const dialogRef = this.dialog.open(TransactionCodeDialogComponent,
      {
        width: '400px',
        data: {}
      });

    dialogRef.afterClosed().subscribe(result => {
      if (!!result) {
        this.transactionService.assignCode(
          transaction.id,
          result.codeName,
          transaction.financialYear.id).subscribe(
          () => { },
          error => {
            console.error(error);
          });
      }
    });
  }

  unassignCode(transaction: Transaction, code: TransactionCode): void {
    this.transactionService.unassignCode(transaction.id, code.codeName, transaction.financialYear.id).subscribe(
      () => { },
      error => {
        console.error(error);
      });
  }

  editDescription(transaction: Transaction): void {
    const dialogRef = this.dialog.open(TransactionDescriptionDialogComponent, {
      width: '400px',
      data: {
        description: transaction.description
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (!!result) {
        this.transactionService.updateDescription(transaction.id, result.description, transaction.financialYear.id).subscribe(
          () => { },
          error => {
            console.error(error);
          });
      }
    });
  }

  assignEvidenceNumber(transaction: Transaction): void {
    this.transactionService.getEvidenceNumberSuggestionForTransaction(transaction.id)
      .pipe(
        take(1),
        switchMap(suggestedEvidenceNumber =>
          this.dialog.open(TransactionEvidenceNumberDialogComponent, {
            width: '400px',
            data: {
              evidenceNumber: transaction.evidenceNumber || suggestedEvidenceNumber || `${transaction.financialYear.name}/`
            }
          }).afterClosed()
        )
      )
      .subscribe(result => {
        if (!!result) {
          this.transactionService.assignEvidenceNumber(transaction.id, result.evidenceNumber, transaction.financialYear.id).subscribe(
            () => { },
            error => {
              console.error(error);
            });
        }
      });
  }

  unassignEvidenceNumber(transaction: Transaction): void {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent,
      {
        width: '400px',
        data: {
          title: this.translateService.instant('Unassign evidence number {{evidenceNumber}} from transaction?', { evidenceNumber: transaction.evidenceNumber }),
          icon: 'delete'
        }
      });

    dialogRef.afterClosed().subscribe(result => {
      if (!!result) {
        this.transactionService.unassignEvidenceNumber(transaction.id, transaction.financialYear.id).subscribe(
            () => { },
            error => {
              console.error(error);
            });
      }
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

    this.startingBalance$ = combineLatest([this.selectedAccount$, startingBalances$]).pipe(
      map(([selectedAccount, startingBalances]) => startingBalances.filter(x => !selectedAccount || x.accountId === selectedAccount.id)),
      map(startingBalances => startingBalances.reduce((acc, startingBalance) => acc + startingBalance.startingBalanceInCents, 0))
    );
  }

  private initTransactionsStream(): void {
    const transactions$ = this.financialYear$.pipe(
      switchMap(financialYear => this.transactionService.getTransactions(financialYear.id))
    );

    this.transactions$ = combineLatest([this.selectedAccount$, this.startingBalance$, transactions$]).pipe(
      map<[Account, number, Transaction[]], [number, Transaction[]]>(([selectedAccount, balanceInCents, transactions]) => ([balanceInCents, transactions.filter(x => !selectedAccount || x.accountId === selectedAccount.id)])),
      map(([balanceInCents, transactions]) => transactions.map(transaction => {
        balanceInCents += transaction.amountInCents;
        let result: TransactionWithBalance = {
          ...transaction,
          balanceInCents: balanceInCents,
        };

        return result;
      }))
    );
  }
}

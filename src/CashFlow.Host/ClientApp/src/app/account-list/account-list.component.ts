import { Component, OnInit, OnDestroy } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { switchMap, takeUntil } from 'rxjs/operators';
import { AccountService } from './../../services/account.service';
import { Account } from './../../models/account';
import { MatDialog } from '@angular/material/dialog';
import { AccountDialogComponent } from './../account-dialog/account-dialog.component';
import { ConfirmationDialogComponent } from './../confirmation-dialog/confirmation-dialog.component';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-account-list',
  templateUrl: './account-list.component.html',
  styleUrls: ['./account-list.component.scss']
})
export class AccountListComponent implements OnInit, OnDestroy {
  private destroy$ = new Subject<void>();
  displayedColumns = ['name', 'type', 'createdAt', 'modifiedAt', 'modify', 'remove'];
  accounts$: Observable<Account[]>;

  constructor(private accountService: AccountService, private dialog: MatDialog, private translateService: TranslateService) { }

  ngOnInit(): void {
    this.accounts$ = this.accountService.getAccounts().pipe(takeUntil(this.destroy$));
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  removeAccount(account: Account): void {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent,
      {
        width: '400px',
        data: {
          title: this.translateService.instant('Remove account?', { name: account.name }),
          icon: 'delete'
        }
      });

    dialogRef.afterClosed().subscribe(result => {
      if (!!result) {
        this.accountService.removeAccount(account.id).subscribe(
          () => {},
          error => {
            console.error(error);
          });
      }
    });
  }

  addAccount(): void {
    const dialogRef = this.dialog.open(AccountDialogComponent,
      {
        width: '400px',
        data: { }
      });

    dialogRef.afterClosed().subscribe(result => {
      if (!!result) {
        this.accountService.addAccount(result.name, result.type).subscribe(
          () => {},
          error => {
            console.error(error);
          });
      }
    });
  }

  modifyAccount(account: Account): void {
    const dialogRef = this.dialog.open(AccountDialogComponent,
      {
        width: '400px',
        data: {
          id: account.id,
          name: account.name,
          type: account.type
        }
      });

    dialogRef.afterClosed().subscribe(result => {
      if (!!result) {
        this.accountService.renameAccount(result.id, result.name, false)
          .pipe(switchMap(() => this.accountService.changeAccountType(result.id, result.type)))
          .subscribe(
          () => {},
          error => {
            console.error(error);
          });
      }
    });
  }
}

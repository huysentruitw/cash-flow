import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { AccountService } from './../../services/account.service';
import { Account } from './../../models/account';
import { MatDialog } from '@angular/material/dialog';
import { AccountDialogComponent } from './../account-dialog/account-dialog.component';

@Component({
  selector: 'app-account-list',
  templateUrl: './account-list.component.html',
  styleUrls: ['./account-list.component.scss']
})
export class AccountListComponent implements OnInit {
  displayedColumns = ['name', 'type', 'createdAt', 'modifiedAt', 'remove'];
  accounts$: Observable<Account[]>;

  constructor(private accountService: AccountService, private dialog: MatDialog) { }

  ngOnInit(): void {
    this.accounts$ = this.accountService.getAccounts();
  }

  removeAccount(id: string): void {
    this.accountService.removeAccount(id).subscribe(
      () => {},
      error => {
        console.error(error);
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
        this.accountService.renameAccount(result.id, result.name)
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

import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
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

  deleteAccount(id: string): void {
    console.log('delete', id);
  }

  addAccount(id: string): void {
    const dialogRef = this.dialog.open(AccountDialogComponent,
      {
        width: '400px',
        data: { id: id }
      });

    dialogRef.afterClosed().subscribe(result => {
      console.log('dialog closed', result);
    });
  }
}

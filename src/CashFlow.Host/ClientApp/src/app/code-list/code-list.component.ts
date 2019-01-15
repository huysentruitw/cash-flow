import { Component, OnInit, OnDestroy } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { CodeService } from './../../services/code.service';
import { Code } from './../../models/code';
import { MatDialog } from '@angular/material';
import { TranslateService } from '@ngx-translate/core';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';
import { CodeDialogComponent } from '../code-dialog/code-dialog.component';

@Component({
  selector: 'app-code-list',
  templateUrl: './code-list.component.html',
  styleUrls: ['./code-list.component.scss']
})
export class CodeListComponent implements OnInit, OnDestroy {
  private destroy$ = new Subject<void>();
  displayedColumns = ['name', 'createdAt', 'modify', 'remove'];
  codes$: Observable<Code[]>;

  constructor(private codeService: CodeService, private dialog: MatDialog, private translateService: TranslateService) { }

  ngOnInit(): void {
    this.codes$ = this.codeService.getCodes();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  removeCode(code: Code): void {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent,
      {
        width: '400px',
        data: {
          title: this.translateService.instant('Remove code?', { name: code.name }),
          icon: 'delete'
        }
      });

    dialogRef.afterClosed().subscribe(result => {
      if (!!result) {
        this.codeService.removeCode(code.name).subscribe(
          () => { },
          error => {
            console.error(error);
          });
      }
    });
  }

  addCode(): void {
    const dialogRef = this.dialog.open(CodeDialogComponent,
      {
        width: '400px',
        data: {
          isAdd: true
        }
      });

    dialogRef.afterClosed().subscribe(result => {
      if (!!result) {
        this.codeService.addCode(result.name).subscribe(
          () => { },
          error => {
            console.error(error);
          });
      }
    });
  }

  modifyCode(account: Account): void {
    const dialogRef = this.dialog.open(CodeDialogComponent,
      {
        width: '400px',
        data: {
          isAdd: false,
          name: account.name
        }
      });

    dialogRef.afterClosed().subscribe(result => {
      if (!!result) {
        this.codeService.renameCode(account.name, result.name).subscribe(
          () => { },
          error => {
            console.error(error);
          });
      }
    });
  }
}

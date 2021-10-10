import { HttpClient } from "@angular/common/http";
import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { Observable } from "rxjs";
import { take } from "rxjs/operators";
import { Account } from "../../models/account";
import { AccountService } from "../../services/account.service";

export class DialogData {
  financialYearId: string;
}

@Component({
  templateUrl: './import-transactions-dialog.component.html',
  styleUrls: ['./import-transactions-dialog.component.scss']
})
export class ImportTransactionsDialogComponent implements OnInit {
  accounts$: Observable<Account[]>;
  fileToUpload: File = null;
  accountId: string = null;

  get isValid(): boolean {
    return !!this.accountId && !!this.fileToUpload;
  }

  constructor(
    private dialogRef: MatDialogRef<ImportTransactionsDialogComponent>,
    @Inject(MAT_DIALOG_DATA) private data: DialogData,
    private accountService: AccountService,
    private httpClient: HttpClient) { }

  ngOnInit(): void {
    this.accounts$ = this.accountService.getAccounts().pipe(take(1));
  }

  fileSelected(files: FileList): void {
    this.fileToUpload = files.item(0);
  }

  importFile(): void {
    if (!this.isValid) return;

    const url = `/api/import/financial-year/${this.data.financialYearId}/account/${this.accountId}/transactions`;
    const formData = new FormData();
    formData.append('csvFile', this.fileToUpload, this.fileToUpload.name);

    this.httpClient
      .post(url, formData)
      .subscribe(() => {
        this.dialogRef.close('imported');
      });
  }
}

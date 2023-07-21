import { Component, Inject } from '@angular/core';
import { MatLegacyDialogRef as MatDialogRef, MAT_LEGACY_DIALOG_DATA as MAT_DIALOG_DATA } from '@angular/material/legacy-dialog';

export class DialogData {
  id: string;
  name: string;
  type: string;
}

@Component({
  selector: 'app-account-dialog',
  templateUrl: './account-dialog.component.html',
  styleUrls: ['./account-dialog.component.scss']
})
export class AccountDialogComponent {
  readonly types = [
    'CASH_ACCOUNT',
    'CURRENT_ACCOUNT',
    'SAVINGS_ACCOUNT'
  ];

  constructor(
    private dialogRef: MatDialogRef<AccountDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) { }

  get isAdd(): boolean {
    return !this.data.id;
  }

  get isValid(): boolean {
    return !!this.data.name && !!this.data.type;
  }
}

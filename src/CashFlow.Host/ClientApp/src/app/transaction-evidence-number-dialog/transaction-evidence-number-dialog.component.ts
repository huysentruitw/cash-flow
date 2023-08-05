import { Component, OnInit, Inject } from '@angular/core';
import { MAT_LEGACY_DIALOG_DATA as MAT_DIALOG_DATA, MatLegacyDialogRef as MatDialogRef } from '@angular/material/legacy-dialog';

export class DialogData {
  evidenceNumber: string;
}

@Component({
  selector: 'app-transaction-evidence-number-dialog',
  templateUrl: './transaction-evidence-number-dialog.component.html',
  styleUrls: ['./transaction-evidence-number-dialog.component.scss']
})
export class TransactionEvidenceNumberDialogComponent implements OnInit {
  private initialEvidenceNumber: string;

  constructor(
    private dialogRef: MatDialogRef<TransactionEvidenceNumberDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) {
    this.initialEvidenceNumber = data.evidenceNumber;
  }

  ngOnInit(): void {
  }

  evidenceNumberFocused(element: HTMLInputElement): void {
    element.select();
  }

  evidenceNumberKeyPress(event: KeyboardEvent): void {
    if (event.keyCode === 13 && this.isValid) {
      this.dialogRef.close(this.data);
    }
  }

  get isValid(): boolean {
    return !!this.data.evidenceNumber;
  }
}

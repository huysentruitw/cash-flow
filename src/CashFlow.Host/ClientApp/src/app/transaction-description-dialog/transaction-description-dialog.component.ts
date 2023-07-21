import { Component, OnInit, Inject } from '@angular/core';
import { MAT_LEGACY_DIALOG_DATA as MAT_DIALOG_DATA, MatLegacyDialogRef as MatDialogRef } from '@angular/material/legacy-dialog';

export class DialogData {
  description: string;
}

@Component({
  selector: 'app-transaction-description-dialog',
  templateUrl: './transaction-description-dialog.component.html',
  styleUrls: ['./transaction-description-dialog.component.scss']
})
export class TransactionDescriptionDialogComponent implements OnInit {
  private initialDescription: string;

  constructor(
    private dialogRef: MatDialogRef<TransactionDescriptionDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) {
    this.initialDescription = data.description;
  }

  ngOnInit() {
  }

  descriptionFocused(element: HTMLInputElement): void {
    element.select();
  }

  descriptionKeyPress(event: KeyboardEvent): void {
    if (event.keyCode === 13 && this.isValid) {
      this.dialogRef.close(this.data);
    }
  }

  get isValid(): boolean {
    return !!this.data.description && this.data.description !== this.initialDescription;
  }
}

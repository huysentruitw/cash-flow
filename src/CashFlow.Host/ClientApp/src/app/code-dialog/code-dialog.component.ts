import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

export class DialogData {
  isAdd: boolean;
  name: string;
}

@Component({
  selector: 'app-code-dialog',
  templateUrl: './code-dialog.component.html',
  styleUrls: ['./code-dialog.component.scss']
})
export class CodeDialogComponent {
  constructor(
    private dialogRef: MatDialogRef<CodeDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) { }

  get isAdd(): boolean {
    return this.data.isAdd;
  }

  get isValid(): boolean {
    return !!this.data.name;
  }
}

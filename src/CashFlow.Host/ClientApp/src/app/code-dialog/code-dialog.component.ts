import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

export class DialogData {
  isAdd: boolean;
  name: string;
}

@Component({
  selector: 'app-code-dialog',
  templateUrl: './code-dialog.component.html',
  styleUrls: ['./code-dialog.component.scss']
})
export class CodeDialogComponent implements OnInit {
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

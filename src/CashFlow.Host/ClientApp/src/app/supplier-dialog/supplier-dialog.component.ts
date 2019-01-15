import { Component, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

export class DialogData {
  id: string;
  name: string;
  contactInfo: string;
}

@Component({
  selector: 'app-supplier-dialog',
  templateUrl: './supplier-dialog.component.html',
  styleUrls: ['./supplier-dialog.component.scss']
})
export class SupplierDialogComponent {

  constructor(
    private dialogRef: MatDialogRef<SupplierDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) { }

  get isAdd(): boolean {
    return !this.data.id;
  }

  get isValid(): boolean {
    return !!this.data.name;
  }
}

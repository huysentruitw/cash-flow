import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { FinancialYear } from 'src/models/financial-year';

export class DialogData {
  financialYears: FinancialYear[];
  id: string;
  name: string;
  previousFinancialYearId: string;
}

@Component({
  selector: 'app-financial-year-dialog',
  templateUrl: './financial-year-dialog.component.html',
  styleUrls: ['./financial-year-dialog.component.scss']
})
export class FinancialYearDialogComponent {

  constructor(
    private dialogRef: MatDialogRef<FinancialYearDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) { }

  get isValid(): boolean {
    return !!this.data.name;
  }

}

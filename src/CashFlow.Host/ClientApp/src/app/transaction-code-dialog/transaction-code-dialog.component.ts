import { Component, Inject, OnInit, OnDestroy } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Observable, Subject, BehaviorSubject, combineLatest } from 'rxjs';
import { take, map } from 'rxjs/operators';
import { Code } from 'src/models/code';
import { CodeService } from 'src/services/code.service';

export class DialogData {
  codeName: string;
}

@Component({
  selector: 'app-transaction-code-dialog',
  templateUrl: './transaction-code-dialog.component.html',
  styleUrls: ['./transaction-code-dialog.component.scss']
})
export class TransactionCodeDialogComponent implements OnInit, OnDestroy {
  private filter$ = new BehaviorSubject<string>('');
  filteredCodes$: Observable<Code[]>;

  constructor(
    private codeService: CodeService,
    private dialogRef: MatDialogRef<TransactionCodeDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) { }

  ngOnInit(): void {
    const codes$ = this.codeService.getCodes().pipe(take(1));

    this.filteredCodes$ = combineLatest(codes$, this.filter$)
      .pipe(map(([codes, filter]) => codes.filter(code => code.name.toUpperCase().startsWith(filter.toUpperCase()))));
  }

  ngOnDestroy(): void {
    this.filter$.complete();
  }

  filterCodes(partialCodeName: string): void {
    this.filter$.next(partialCodeName);
  }

  codeSelected(): void {
    if (this.isValid) {
      this.dialogRef.close(this.data);
    }
  }

  get isValid(): boolean {
    return !!this.data.codeName;
  }
}

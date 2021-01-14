import { Component, Inject, OnInit, OnDestroy } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Observable, BehaviorSubject, combineLatest } from 'rxjs';
import { take, map } from 'rxjs/operators';
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
  filteredCodeNames$: Observable<string[]>;

  constructor(
    private codeService: CodeService,
    private dialogRef: MatDialogRef<TransactionCodeDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) { }

  ngOnInit(): void {
    const codeNames$ = this.codeService.getActiveCodeNames().pipe(take(1));

    this.filteredCodeNames$ = combineLatest([codeNames$, this.filter$])
      .pipe(map(([codeNames, filter]) => codeNames.filter(codeName => codeName.toUpperCase().startsWith(filter.toUpperCase()))));
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

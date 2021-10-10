import { Component, OnDestroy, OnInit } from '@angular/core';
import {MatDialog} from "@angular/material/dialog";
import { Observable, Subject } from "rxjs";
import { filter, map, take, takeUntil } from "rxjs/operators";
import { BusService } from "../../services/bus.service";
import { ImportTransactionsDialogComponent } from "../import-transactions-dialog/import-transactions-dialog.component";

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss']
})
export class ToolbarComponent implements OnInit, OnDestroy {
  private destroy$ = new Subject<void>();
  private financialYearId$: Observable<string>;

  constructor(private busService: BusService, private dialog: MatDialog) { }

  ngOnInit() {
    this.initFinancialYearStream();
  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }

  private initFinancialYearStream(): void {
    this.financialYearId$ = this.busService.activeFinancialYear$
      .pipe(takeUntil(this.destroy$), filter(financialYear => !!financialYear), map(financialYear => financialYear.id));
  }

  exportFinancialYear(): void {
    this.financialYearId$.pipe(take(1)).subscribe(financialYearId => {
      const url = `/api/export/financial-year/${financialYearId}/transactions`;
      window.open(url, '_blank');
    });
  }

  importTransactions(): void {
    this.financialYearId$.pipe(take(1)).subscribe(financialYearId => {
      this.dialog.open(ImportTransactionsDialogComponent,
        {
          width: '400px',
          data: {
            financialYearId
          }
        });
    });
  }
}

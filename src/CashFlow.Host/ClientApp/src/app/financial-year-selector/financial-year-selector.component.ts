import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { FinancialYear } from 'src/models/financial-year';
import { FinancialYearService } from 'src/services/financial-year.service';
import { takeUntil, tap, switchMap, take } from 'rxjs/operators';
import { FinancialYearDialogComponent } from '../financial-year-dialog/financial-year-dialog.component';
import { MatDialog } from '@angular/material';

@Component({
  selector: 'app-financial-year-selector',
  templateUrl: './financial-year-selector.component.html',
  styleUrls: ['./financial-year-selector.component.scss']
})
export class FinancialYearSelectorComponent implements OnInit, OnDestroy {
  private destroy$ = new Subject<void>();
  financialYears$: Observable<FinancialYear[]>;

  constructor(private financialYearService: FinancialYearService, private dialog: MatDialog) { }

  ngOnInit(): void {
    this.financialYears$ = this.financialYearService.getFinancialYears().pipe(takeUntil(this.destroy$));
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  addFinancialYear(): void {
    this.financialYears$.pipe(take(1)).subscribe(financialYears => {
      console.log(financialYears);
      const dialogRef = this.dialog.open(FinancialYearDialogComponent,
        {
          width: '400px',
          data: {
            financialYears: financialYears
          }
        });

      dialogRef.afterClosed().subscribe(result => {
        if (!!result) {
          this.financialYearService.addFinancialYear(result.name, result.previousFinancialYearId).subscribe(
            () => { },
            error => {
              console.error(error);
            });
        }
      });

    });
  }

  get activeFinancialYearId(): string {
    return "832d3cd0-1128-4151-bde7-8f3baae41822";
  }

  set activeFinancialYearId(value: string) {
    console.log(value);
  }

}

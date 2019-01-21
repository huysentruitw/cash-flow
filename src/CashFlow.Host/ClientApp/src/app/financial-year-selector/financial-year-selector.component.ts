import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { FinancialYear } from 'src/models/financial-year';
import { FinancialYearService } from 'src/services/financial-year.service';
import { takeUntil, take, map, switchMap } from 'rxjs/operators';
import { FinancialYearDialogComponent } from '../financial-year-dialog/financial-year-dialog.component';
import { MatDialog } from '@angular/material';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-financial-year-selector',
  templateUrl: './financial-year-selector.component.html',
  styleUrls: ['./financial-year-selector.component.scss']
})
export class FinancialYearSelectorComponent implements OnInit, OnDestroy {
  private destroy$ = new Subject<void>();
  financialYears$: Observable<FinancialYear[]>;
  activeFinancialYear$: Observable<FinancialYear>;

  constructor(private financialYearService: FinancialYearService, private translate: TranslateService, private dialog: MatDialog) { }

  ngOnInit(): void {
    this.financialYears$ = this.financialYearService.getFinancialYears().pipe(takeUntil(this.destroy$));
    this.activeFinancialYear$ = this.financialYears$.pipe(map(years => years.find(year => year.isActive)));
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  addFinancialYear(): void {
    this.financialYears$.pipe(
      take(1),
      switchMap(financialYears => this.dialog.open(FinancialYearDialogComponent,
        {
          width: '400px',
          data: {
            financialYears: financialYears
          }
        }).afterClosed()
      ))
      .subscribe(result => {
        if (!!result) {
          this.financialYearService.addFinancialYear(result.name, result.previousFinancialYearId).subscribe(
            () => { },
            error => {
              console.error(error);
            });
        }
      });
  }

  updateActiveFinancialYear(financialYearId: any): void {
    this.financialYears$.pipe(
      take(1),
      map(financialYears => financialYears.find(year => year.id === financialYearId)),
      switchMap(newFinancialYear => this.dialog.open(ConfirmationDialogComponent,
        {
          width: '400px',
          data: {
            title: this.translate.instant('Are you sure you want to change the active financial year to {{name}}?', { name: newFinancialYear.name })
          }
        })
        .afterClosed()
      ))
      .subscribe(result => {
        if (!!result) {
          this.financialYearService.activateFinancialYear(financialYearId).subscribe(
            () => { },
            error => {
              console.error(error);
            });
        } else {
          this.activeFinancialYear$ = this.financialYears$.pipe(map(years => years.find(year => year.isActive)));
        }
      });
  }

}

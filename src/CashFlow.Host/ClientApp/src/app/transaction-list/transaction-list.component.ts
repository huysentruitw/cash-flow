import { Component, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { FinancialYear } from 'src/models/financial-year';
import { AppComponent } from '../app.component';

@Component({
  selector: 'app-transaction-list',
  templateUrl: './transaction-list.component.html',
  styleUrls: ['./transaction-list.component.scss']
})
export class TransactionListComponent implements OnInit, OnDestroy {
  private destroy$ = new Subject<void>();
  private financialYear$: Observable<FinancialYear>;

  constructor(app: AppComponent) {
    this.financialYear$ = app.financialYear$.pipe(takeUntil(this.destroy$));
  }

  ngOnInit(): void {
    this.financialYear$.subscribe(x => console.log('a', x));
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

}

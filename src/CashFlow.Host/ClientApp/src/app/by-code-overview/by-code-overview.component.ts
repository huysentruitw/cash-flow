import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subject, Observable, of, BehaviorSubject, combineLatest } from 'rxjs';
import { CodeBalance } from '../../models/code-balance';
import { ByCodeOverviewService } from '../../services/by-code-overview.service';
import { BusService } from '../../services/bus.service';
import { takeUntil, filter, switchMap, map, take } from 'rxjs/operators';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { Transaction } from '../../models/transaction';

@Component({
  selector: 'app-by-code-overview',
  templateUrl: './by-code-overview.component.html',
  styleUrls: ['./by-code-overview.component.scss'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({ height: '0px', minHeight: '0' })),
      state('expanded', style({ height: '*' })),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class ByCodeOverviewComponent implements OnInit, OnDestroy {
  private destroy$ = new Subject<void>();
  private showAll$ = new BehaviorSubject(false);
  private financialYearId$: Observable<string>;
  private codeTransactions$: { [codeName: string]: Observable<Transaction[]>; } = {};
  displayedColumns = ['name', 'totalIncome', 'totalExpense', 'balance', 'actions'];
  codeBalances$: Observable<CodeBalance[]>;
  expandedElement: CodeBalance = null;

  constructor(
    private byCodeOverviewService: ByCodeOverviewService,
    private busService: BusService) {
  }

  ngOnInit() {
    this.initFinancialYearStream();
    this.initCodeBalancesStream();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  expandElement(codeBalance: CodeBalance): void {
    this.expandedElement = this.expandedElement === codeBalance ? null : codeBalance;
  }

  viewAsPdf(event: Event, codeBalance: CodeBalance): void {
    event.stopPropagation();
    combineLatest([this.financialYearId$, this.showAll$])
      .pipe(take(1))
      .subscribe(([financialYearId, showAll]) => {
        const url = showAll
          ? `/api/report/by-code-overview/${codeBalance.name}`
          : `/api/report/financial-year/${financialYearId}/by-code-overview/${codeBalance.name}`;
        window.open(url, '_blank');
      });
  }

  getCodeTransactions(codeName: string): Observable<Transaction[]> {
    if (!this.codeTransactions$[codeName]) {
      this.codeTransactions$[codeName] = this.financialYearId$.pipe(
        switchMap(financialYearId => this.byCodeOverviewService.getTransactions(financialYearId, codeName)));
    }

    return this.codeTransactions$[codeName];
  }

  showCodesForAllFinancialYears(showAll: boolean): void {
    this.showAll$.next(showAll);
  }

  private initFinancialYearStream(): void {
    const financialYear$ = this.busService.activeFinancialYear$
      .pipe(takeUntil(this.destroy$), filter(financialYear => !!financialYear));

    this.financialYearId$ = combineLatest([financialYear$, this.showAll$])
      .pipe(takeUntil(this.destroy$), map(([financialYear, showAll]) => showAll ? null : financialYear.id));
  }

  private initCodeBalancesStream(): void {
    this.codeBalances$ = this.financialYearId$
      .pipe(switchMap(financialYearId => this.byCodeOverviewService.getCodeBalances(financialYearId)));
  }
}

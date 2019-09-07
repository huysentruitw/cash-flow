import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subject, Observable, of } from 'rxjs';
import { CodeBalance } from '../../models/code-balance';
import { ByCodeOverviewService } from '../../services/by-code-overview.service';
import { BusService } from '../../services/bus.service';
import { FinancialYear } from '../../models/financial-year';
import { takeUntil, filter, switchMap, mergeMap, map, tap } from 'rxjs/operators';
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
  private financialYear$: Observable<FinancialYear>;
  private codeTransactions$: { [codeName: string]: Observable<Transaction[]>; } = {};
  displayedColumns = ['name', 'totalIncome', 'totalExpense', 'balance'];
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

  getCodeTransactions(codeName: string): Observable<Transaction[]> {
    if (!this.codeTransactions$[codeName]) {
      this.codeTransactions$[codeName] = this.financialYear$.pipe(
        switchMap(financialYear => this.byCodeOverviewService.getTransactions(financialYear.id, codeName)));
    }

    return this.codeTransactions$[codeName];
  }

  private initFinancialYearStream(): void {
    this.financialYear$ = this.busService.activeFinancialYear$
      .pipe(takeUntil(this.destroy$), filter(financialYear => !!financialYear));
  }

  private initCodeBalancesStream(): void {
    this.codeBalances$ = this.financialYear$.pipe(
      switchMap(financialYear => this.byCodeOverviewService.getCodeBalances(financialYear.id)));
  }
}

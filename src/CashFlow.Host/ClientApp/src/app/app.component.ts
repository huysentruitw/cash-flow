import { registerLocaleData } from '@angular/common';
import localeExtra from '@angular/common/locales/extra/nl-BE';
import locale from '@angular/common/locales/nl-BE';
import { Component, OnDestroy } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ReplaySubject } from 'rxjs';
import { FinancialYear } from 'src/models/financial-year';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnDestroy {
  title = 'CashFlow';
  financialYear$ = new ReplaySubject<FinancialYear>();

  constructor(translateService: TranslateService) {
    translateService.setDefaultLang('nl-BE');
    translateService.use('nl-BE');
    registerLocaleData(locale, localeExtra);
  }

  ngOnDestroy(): void {
    this.financialYear$.next();
    this.financialYear$.complete();
  }
}

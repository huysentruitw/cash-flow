import { registerLocaleData } from '@angular/common';
import localeExtra from '@angular/common/locales/extra/nl-BE';
import locale from '@angular/common/locales/nl-BE';
import { Component, OnDestroy } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnDestroy {
  title = 'CashFlow';

  constructor(translateService: TranslateService) {
    translateService.setDefaultLang('nl-BE');
    translateService.use('nl-BE');
    registerLocaleData(locale, localeExtra);
  }

  ngOnDestroy(): void {
  }
}

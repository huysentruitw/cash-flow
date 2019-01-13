import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { registerLocaleData } from '@angular/common';
import locale from '@angular/common/locales/nl-BE';
import localeExtra from '@angular/common/locales/extra/nl-BE';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'CashFlow';

  constructor(translateService: TranslateService) {
    translateService.setDefaultLang('nl-BE');
    translateService.use('nl-BE');
    registerLocaleData(locale, localeExtra);
  }

}

import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { FinancialYear } from 'src/models/financial-year';

@Injectable({
  providedIn: 'root'
})
export class BusService {
  activeFinancialYear$ = new BehaviorSubject<FinancialYear>(null);

  constructor() { }
}

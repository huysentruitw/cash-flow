import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FinancialYear } from 'src/models/financial-year';

@Component({
  selector: 'app-toolbar',
  templateUrl: './toolbar.component.html',
  styleUrls: ['./toolbar.component.scss']
})
export class ToolbarComponent implements OnInit {

  @Output()
  financialYearChange: EventEmitter<FinancialYear> = new EventEmitter<FinancialYear>();

  constructor() { }

  ngOnInit() {
  }

}

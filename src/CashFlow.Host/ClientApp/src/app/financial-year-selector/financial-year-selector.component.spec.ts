import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { FinancialYearSelectorComponent } from './financial-year-selector.component';

describe('FinancialYearSelectorComponent', () => {
  let component: FinancialYearSelectorComponent;
  let fixture: ComponentFixture<FinancialYearSelectorComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ FinancialYearSelectorComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FinancialYearSelectorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

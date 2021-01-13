import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { FinancialYearDialogComponent } from './financial-year-dialog.component';

describe('FinancialYearDialogComponent', () => {
  let component: FinancialYearDialogComponent;
  let fixture: ComponentFixture<FinancialYearDialogComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ FinancialYearDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(FinancialYearDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { TransactionCodeDialogComponent } from './transaction-code-dialog.component';

describe('TransactionCodeDialogComponent', () => {
  let component: TransactionCodeDialogComponent;
  let fixture: ComponentFixture<TransactionCodeDialogComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ TransactionCodeDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TransactionCodeDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TransactionDialogComponent } from './transaction-dialog.component';

describe('TransactionDialogComponent', () => {
  let component: TransactionDialogComponent;
  let fixture: ComponentFixture<TransactionDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TransactionDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TransactionDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

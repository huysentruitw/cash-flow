import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { TransactionDescriptionDialogComponent } from './transaction-description-dialog.component';

describe('TransactionDescriptionDialogComponent', () => {
  let component: TransactionDescriptionDialogComponent;
  let fixture: ComponentFixture<TransactionDescriptionDialogComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ TransactionDescriptionDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TransactionDescriptionDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

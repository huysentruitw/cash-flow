import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TransactionDescriptionDialogComponent } from './transaction-description-dialog.component';

describe('TransactionDescriptionDialogComponent', () => {
  let component: TransactionDescriptionDialogComponent;
  let fixture: ComponentFixture<TransactionDescriptionDialogComponent>;

  beforeEach(async(() => {
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

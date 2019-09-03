import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TransactionEvidenceNumberDialogComponent } from './transaction-evidence-number-dialog.component';

describe('TransactionDescriptionDialogComponent', () => {
  let component: TransactionEvidenceNumberDialogComponent;
  let fixture: ComponentFixture<TransactionEvidenceNumberDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [TransactionEvidenceNumberDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TransactionEvidenceNumberDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

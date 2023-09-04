import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ImportTransactionsDialogComponent } from './import-transactions-dialog.component';

describe('ImportTransactionsDialogComponent', () => {
  let component: ImportTransactionsDialogComponent;
  let fixture: ComponentFixture<ImportTransactionsDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ImportTransactionsDialogComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ImportTransactionsDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

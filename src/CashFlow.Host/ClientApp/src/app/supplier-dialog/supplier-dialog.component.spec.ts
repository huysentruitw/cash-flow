import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { SupplierDialogComponent } from './supplier-dialog.component';

describe('SupplierDialogComponent', () => {
  let component: SupplierDialogComponent;
  let fixture: ComponentFixture<SupplierDialogComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ SupplierDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SupplierDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

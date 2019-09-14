import { Component, OnInit, OnDestroy } from '@angular/core';
import { Observable, Subject } from 'rxjs';
import { SupplierService } from './../../services/supplier.service';
import { Supplier } from './../../models/supplier';
import { takeUntil, switchMap } from 'rxjs/operators';
import { ConfirmationDialogComponent } from '../confirmation-dialog/confirmation-dialog.component';
import { MatDialog } from '@angular/material/dialog';
import { TranslateService } from '@ngx-translate/core';
import { SupplierDialogComponent } from '../supplier-dialog/supplier-dialog.component';

@Component({
  selector: 'app-supplier-list',
  templateUrl: './supplier-list.component.html',
  styleUrls: ['./supplier-list.component.scss']
})
export class SupplierListComponent implements OnInit, OnDestroy {
  private destroy$ = new Subject<void>();
  displayedColumns = ['name', 'contactInfo', 'createdAt', 'modifiedAt', 'modify', 'remove'];
  suppliers$: Observable<Supplier[]>;

  constructor(private supplierService: SupplierService, private dialog: MatDialog, private translateService: TranslateService) { }

  ngOnInit(): void {
    this.suppliers$ = this.supplierService.getSuppliers().pipe(takeUntil(this.destroy$));
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  removeSupplier(supplier: Supplier): void {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent,
      {
        width: '400px',
        data: {
          title: this.translateService.instant('Remove supplier?', { name: supplier.name }),
          icon: 'delete'
        }
      });

    dialogRef.afterClosed().subscribe(result => {
      if (!!result) {
        this.supplierService.removeSupplier(supplier.id).subscribe(
          () => { },
          error => {
            console.error(error);
          });
      }
    });
  }

  addSupplier(): void {
    const dialogRef = this.dialog.open(SupplierDialogComponent,
      {
        width: '400px',
        data: {}
      });

    dialogRef.afterClosed().subscribe(result => {
      if (!!result) {
        this.supplierService.addSupplier(result.name, result.contactInfo).subscribe(
          () => { },
          error => {
            console.error(error);
          });
      }
    });
  }

  modifySupplier(supplier: Supplier): void {
    const dialogRef = this.dialog.open(SupplierDialogComponent,
      {
        width: '400px',
        data: {
          id: supplier.id,
          name: supplier.name,
          contactInfo: supplier.contactInfo
        }
      });

    dialogRef.afterClosed().subscribe(result => {
      if (!!result) {
        this.supplierService.renameSupplier(result.id, result.name, false)
          .pipe(switchMap(() => this.supplierService.updateContactInfo(result.id, result.contactInfo)))
          .subscribe(
            () => { },
            error => {
              console.error(error);
            });
      }
    });
  }

}

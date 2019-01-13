import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { SupplierService } from './../../services/supplier.service';
import { Supplier } from './../../models/supplier';

@Component({
  selector: 'app-supplier-list',
  templateUrl: './supplier-list.component.html',
  styleUrls: ['./supplier-list.component.scss']
})
export class SupplierListComponent implements OnInit {
  displayedColumns = ['name', 'contactInfo', 'createdAt', 'modifiedAt'];
  suppliers$: Observable<Supplier[]>;

  constructor(private supplierService: SupplierService) { }

  ngOnInit() {
    this.suppliers$ = this.supplierService.getSuppliers();
  }

}

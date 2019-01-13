import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { CodeService } from './../../services/code.service';
import { Code } from './../../models/code';

@Component({
  selector: 'app-code-list',
  templateUrl: './code-list.component.html',
  styleUrls: ['./code-list.component.scss']
})
export class CodeListComponent implements OnInit {
  displayedColumns = ['name', 'createdAt'];
  codes$: Observable<Code[]>;

  constructor(private codeService: CodeService) { }

  ngOnInit() {
    this.codes$ = this.codeService.getCodes();
  }

}

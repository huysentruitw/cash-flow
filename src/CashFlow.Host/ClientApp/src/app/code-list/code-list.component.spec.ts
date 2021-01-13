import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { CodeListComponent } from './code-list.component';

describe('CodeListComponent', () => {
  let component: CodeListComponent;
  let fixture: ComponentFixture<CodeListComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ CodeListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CodeListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

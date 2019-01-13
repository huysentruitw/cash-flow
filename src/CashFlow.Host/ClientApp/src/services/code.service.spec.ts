import { TestBed } from '@angular/core/testing';

import { CodeService } from './code.service';

describe('CodeService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CodeService = TestBed.get(CodeService);
    expect(service).toBeTruthy();
  });
});

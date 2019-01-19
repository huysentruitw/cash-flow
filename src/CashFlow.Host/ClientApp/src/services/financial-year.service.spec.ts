import { TestBed } from '@angular/core/testing';

import { FinancialYearService } from './financial-year.service';

describe('FinancialYearService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FinancialYearService = TestBed.get(FinancialYearService);
    expect(service).toBeTruthy();
  });
});

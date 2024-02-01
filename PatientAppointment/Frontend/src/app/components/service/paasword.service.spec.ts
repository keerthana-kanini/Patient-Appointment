import { TestBed } from '@angular/core/testing';

import { PaaswordService } from './paasword.service';

describe('PaaswordService', () => {
  let service: PaaswordService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PaaswordService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

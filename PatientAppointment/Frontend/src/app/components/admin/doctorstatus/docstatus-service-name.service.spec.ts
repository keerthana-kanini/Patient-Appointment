import { TestBed } from '@angular/core/testing';

import { DocstatusServiceNameService } from './docstatus-service-name.service';

describe('DocstatusServiceNameService', () => {
  let service: DocstatusServiceNameService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DocstatusServiceNameService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

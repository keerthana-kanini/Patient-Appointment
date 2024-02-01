import { TestBed } from '@angular/core/testing';

import { PatstatusServiceNameService } from './patstatus-service-name.service';

describe('PatstatusServiceNameService', () => {
  let service: PatstatusServiceNameService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PatstatusServiceNameService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

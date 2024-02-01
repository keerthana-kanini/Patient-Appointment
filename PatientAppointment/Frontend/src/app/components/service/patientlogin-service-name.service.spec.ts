import { TestBed } from '@angular/core/testing';

import { PatientloginServiceNameService } from './patientlogin-service-name.service';

describe('PatientloginServiceNameService', () => {
  let service: PatientloginServiceNameService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PatientloginServiceNameService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

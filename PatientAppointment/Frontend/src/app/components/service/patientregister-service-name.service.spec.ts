import { TestBed } from '@angular/core/testing';

import { PatientregisterServiceNameService } from './patientregister-service-name.service';

describe('PatientregisterServiceNameService', () => {
  let service: PatientregisterServiceNameService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PatientregisterServiceNameService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

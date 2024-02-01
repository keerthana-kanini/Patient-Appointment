import { TestBed } from '@angular/core/testing';

import { DoctorstatusServiceNameService } from './doctorstatus-service-name.service';

describe('DoctorstatusServiceNameService', () => {
  let service: DoctorstatusServiceNameService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DoctorstatusServiceNameService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

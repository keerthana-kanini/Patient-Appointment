import { TestBed } from '@angular/core/testing';

import { DoctorappointmentService } from './doctorappointment.service';

describe('DoctorappointmentService', () => {
  let service: DoctorappointmentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DoctorappointmentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

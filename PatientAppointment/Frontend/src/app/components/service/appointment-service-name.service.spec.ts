import { TestBed } from '@angular/core/testing';

import { AppointmentServiceNameService } from './appointment-service-name.service';

describe('AppointmentServiceNameService', () => {
  let service: AppointmentServiceNameService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AppointmentServiceNameService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

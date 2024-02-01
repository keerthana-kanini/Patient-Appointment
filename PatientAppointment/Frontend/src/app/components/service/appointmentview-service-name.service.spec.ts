import { TestBed } from '@angular/core/testing';
import { AppointmentViewService } from './appointmentview-service-name.service';

describe('AppointmentviewServiceNameService', () => {
  let service: AppointmentViewService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AppointmentViewService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

import { Component } from '@angular/core';
import { Appointment } from '../../../Appointment';
import { DoctorappointmentService } from '../../service/doctorappointment.service';

@Component({
  selector: 'app-appointmentdecline',
  templateUrl: './appointmentdecline.component.html',
  styleUrl: './appointmentdecline.component.css'
})
export class AppointmentdeclineComponent {
  appointments: Appointment[] = [];

  constructor(private appointmentService: DoctorappointmentService) { }

  ngOnInit(): void {
    this.loadAppointments();
  }

  loadAppointments() {
    this.appointmentService.getdeclineAppointments().subscribe(
      (data: Appointment[]) => {
        this.appointments = data;
      },
      error => {
        console.error('Error fetching appointments:', error);
      }
    );
  }
}


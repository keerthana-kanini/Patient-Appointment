import { Component } from '@angular/core';
import { Appointment } from '../../../Appointment';
import { DoctorappointmentService } from '../../service/doctorappointment.service';

@Component({
  selector: 'app-appointmentapprove',
  templateUrl: './appointmentapprove.component.html',
  styleUrl: './appointmentapprove.component.css'
})
export class AppointmentapproveComponent {
  appointments: Appointment[] = [];

  constructor(private appointmentService: DoctorappointmentService) { }

  ngOnInit(): void {
    this.loadAppointments();
  }

  loadAppointments() {
    this.appointmentService.getapproveAppointments().subscribe(
      (data: Appointment[]) => {
        this.appointments = data;
      },
      error => {
        console.error('Error fetching appointments:', error);
      }
    );
  }
}



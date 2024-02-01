import { Component } from '@angular/core';
import { Appointment } from '../../../Appointment';
import { AppointmentViewService } from '../../service/appointmentview-service-name.service';

@Component({
  selector: 'app-appointmentpending',
  templateUrl: './appointmentpending.component.html',
  styleUrls: ['./appointmentpending.component.css']
})
export class AppointmentpendingComponent {
  appointments: Appointment[] = [];

  constructor(private appointmentService: AppointmentViewService) { }

  ngOnInit() {
    this.appointmentService.getappointmentrequest().subscribe(data => {
      this.appointments = data;
    });
  }

  approveAppointment(appointment: Appointment): void {
    const patientEmail = appointment.patient?.patient_Email || '';

    
    if (window.confirm('Are you sure you want to approve this appointment?')) {
      this.appointmentService.approveAppointment(patientEmail).subscribe(response => {
        console.log(response);
        this.refreshAppointments();
      });
    }
  }

  declineAppointment(appointment: Appointment): void {
    const patientEmail = appointment.patient?.patient_Email || '';

   
    if (window.confirm('Are you sure you want to decline this appointment?')) {
      this.appointmentService.declineAppointment(patientEmail).subscribe(response => {
        console.log(response);
        this.refreshAppointments();
      });
    }
  }

  rescheduleAppointment(appointment: Appointment): void {
    const patientEmail = appointment.patient?.patient_Email || '';

    
    if (window.confirm('Are you sure you want to reschedule this appointment?')) {
      this.appointmentService.rescheduleAppointment(patientEmail).subscribe(response => {
        console.log(response);
        this.refreshAppointments();
      });
    }
  }

  private refreshAppointments(): void {
    this.appointmentService.getappointmentrequest().subscribe(data => {
      this.appointments = data;
    });
  }
}

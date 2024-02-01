import { Component, OnInit } from '@angular/core';
import { DoctorappointmentService } from '../../service/doctorappointment.service';
import { Appointment } from '../../../Appointment';

@Component({
  selector: 'app-doctorpage',
  templateUrl: './doctorpage.component.html',
  styleUrls: ['./doctorpage.component.css']
})
export class DoctorpageComponent implements OnInit {
  appointments: Appointment[] = [];

  constructor(private appointmentService: DoctorappointmentService) { }

  ngOnInit(): void {
    this.loadAppointments();
  }

  loadAppointments() {
    this.appointmentService.getDoctorAppointments().subscribe(
      (data: Appointment[]) => {
        this.appointments = data;
      },
      error => {
        console.error('Error fetching appointments:', error);
      }
    );
  }

  approveAppointment(appointmentId: number) {
    const confirmation = window.confirm('Are you sure you want to approve this appointment?');

    if (confirmation) {
      this.appointmentService.approveAppointment(appointmentId).subscribe(
        () => {
          console.log('Appointment approved successfully');
          this.loadAppointments();
          alert('Appointment approved successfully');
        },
        error => {
          console.error('Error approving appointment:', error);
          alert('Error approving appointment. Please try again.');
        }
      );
    }
  }

  declineAppointment(appointmentId: number) {
    const confirmation = window.confirm('Are you sure you want to decline this appointment?');

    if (confirmation) {
      this.appointmentService.declineAppointment(appointmentId).subscribe(
        () => {
          console.log('Appointment declined successfully');
          this.loadAppointments();
          alert('Appointment declined successfully');
        },
        error => {
          console.error('Error declining appointment:', error);
          alert('Error declining appointment. Please try again.');
        }
      );
    }
  }


}

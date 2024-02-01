import { Component, OnInit } from '@angular/core';
import { Appointment } from '../../../Appointment';
import { AppointmentViewService } from '../../service/appointmentview-service-name.service';

@Component({
  selector: 'app-appointmentdetails',
  templateUrl: './appointmentdetails.component.html',
  styleUrls: ['./appointmentdetails.component.css']
})
export class AppointmentdetailsComponent implements OnInit {
  appointments: Appointment[] = [];

  constructor(private appointmentService: AppointmentViewService) { }

  ngOnInit() {
    this.appointmentService.getAppointments().subscribe(data => {
      this.appointments = data;
    });
  }
  
}



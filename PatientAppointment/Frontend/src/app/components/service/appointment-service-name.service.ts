import { Injectable } from '@angular/core';
import { Appointment } from '../../Appointment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';

@Injectable({
  providedIn: 'root'
})
export class AppointmentServiceNameService {
  private apiUrl = 'https://localhost:7068/api/Appointments/BookAppointment';

  constructor(private http: HttpClient) {}

  bookAppointment(appointmentData: any): Observable<Appointment> {
    return this.http.post<Appointment>(this.apiUrl, appointmentData);
  }
}
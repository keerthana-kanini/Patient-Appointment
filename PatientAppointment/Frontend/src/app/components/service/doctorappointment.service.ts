import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Appointment } from '../../Appointment';

@Injectable({
  providedIn: 'root'
})
export class DoctorappointmentService {
  private apiUrl = 'https://localhost:7068/api/Appointments';

  constructor(private http: HttpClient) { }

  getDoctorAppointments(): Observable<Appointment[]> {
    return this.http.get<Appointment[]>(`${this.apiUrl}/GetDoctorAppointmentsByEmailAndStatus?doctorEmail=${localStorage.getItem('username')}&status=pending`);
  }

  approveAppointment(appointmentId: number): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/ApproveAppointmentRequest/${appointmentId}`, {});
  }

  declineAppointment(appointmentId: number): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}/DeclineAppointmentRequest/${appointmentId}`, {});
  }

  getapproveAppointments(): Observable<Appointment[]> {
    return this.http.get<Appointment[]>(`${this.apiUrl}/GetDoctorAppointmentsByEmailAndStatus?doctorEmail=${localStorage.getItem('username')}&status=approved`);
  }
  getdeclineAppointments(): Observable<Appointment[]> {
    return this.http.get<Appointment[]>(`${this.apiUrl}/GetDoctorAppointmentsByEmailAndStatus?doctorEmail=${localStorage.getItem('username')}&status=declined`);
  }
}

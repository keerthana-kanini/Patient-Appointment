import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError } from 'rxjs';
import { Appointment } from '../../Appointment';

@Injectable({
  providedIn: 'root'
})
export class AppointmentViewService {
  private getUrl = 'https://localhost:7068/api/Appointments/GetAllAppointments';
  private requests = 'https://localhost:7068/api/Appointments/AppointmentRequests/Approved';

  constructor(private http: HttpClient) { }

  getAppointments(): Observable<Appointment[]> {
    return this.http.get<Appointment[]>(this.getUrl);
  }
  getappointmentrequest(): Observable<Appointment[]> {
    return this.http.get<Appointment[]>(this.requests);
  }
  approveAppointment(patientEmail: string): Observable<any> {
    const apiUrl = `https://localhost:7068/api/Mail/ApproveEmail?request=${encodeURIComponent(patientEmail)}`;
    return this.http.post(apiUrl, patientEmail).pipe(
      catchError(error => {
        console.error('Error approving appointment:', error);
        throw error;
      })
    );
  }
  
  declineAppointment(patientEmail: string): Observable<any> {
    const apiUrl = `https://localhost:7068/api/Mail/DeclineEmail?request=${encodeURIComponent(patientEmail)}`;
    return this.http.post(apiUrl, patientEmail).pipe(
      catchError(error => {
        console.error('Error declining appointment:', error);
        throw error;
      })
    );
  }
  
  rescheduleAppointment(patientEmail: string): Observable<any> {
    const apiUrl = `https://localhost:7068/api/Mail/SendRescheduleEmail?request=${encodeURIComponent(patientEmail)}`;
    return this.http.post(apiUrl, patientEmail).pipe(
      catchError(error => {
        console.error('Error rescheduling appointment:', error);
        throw error;
      })
    );
  }
  
}

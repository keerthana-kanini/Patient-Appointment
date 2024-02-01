import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DoctorstatusServiceNameService {
  private baseUrl = 'https://localhost:7068/api';

  constructor(private http: HttpClient) {}

  getDoctorRequests(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/FrontEndExecutive/DoctorRequests`);
  }

  approveDoctorRequest(doctorRequestId: number): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/FrontEndExecutive/ApproveDoctorRequest/${doctorRequestId}`, {});
  }

  declineDoctorRequest(doctorRequestId: number): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/FrontEndExecutive/DeclineDoctorRequest/${doctorRequestId}`, {});
  }

  getDoctorDetails(doctorId: number): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/Doctors/${doctorId}`);
  }

  getAllDoctors(): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/Doctors`);
  }
  login(doctor_Email: string, doctor_Password: string) {
    const signInData = { doctor_Email, doctor_Password };
  
    return this.http.post<string>('https://localhost:7068/api/Logins/Doctor', signInData);
  }
  registerdoctor(doctorData: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/Doctors`, doctorData, { responseType: 'text' });
  }
  
}


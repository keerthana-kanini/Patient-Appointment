import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Patient } from '../../Patient';

@Injectable({
  providedIn: 'root'
})
export class PatstatusServiceNameService {
  private apiUrl = 'https://localhost:7068/api/FrontEndExecutive/PatientRequests';
  private executiveApiUrl = 'https://localhost:7068/api/FrontEndExecutive';

  constructor(private http: HttpClient) { }

  getPatients(): Observable<Patient[]> {
    return this.http.get<Patient[]>(this.apiUrl);
  }

  approvePatient(patientId: number): Observable<any> {
    const approveUrl = `${this.executiveApiUrl}/ApprovePatientRequest/${patientId}`;
    return this.http.post(approveUrl, null);
  }

  declinePatient(patientId: number): Observable<any> {
    const declineUrl = `${this.executiveApiUrl}/DeclinePatientRequest/${patientId}`;
    return this.http.post(declineUrl, null);
  }

  patientapproved (): Observable<Patient[]> {
    const approvedUrl = `${this.executiveApiUrl}/PatientRequestsStatus?status=approved`;
    return this.http.get<Patient[]>(approvedUrl);
  }
  patientdeclined (): Observable<Patient[]> {
    const approvedUrl = `${this.executiveApiUrl}/PatientRequestsStatus?status=declined`;
    return this.http.get<Patient[]>(approvedUrl);
  }
}

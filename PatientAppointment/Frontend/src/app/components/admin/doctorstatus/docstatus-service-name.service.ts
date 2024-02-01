import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Doctor } from '../../../Doctor';

@Injectable({
  providedIn: 'root'
})
export class DocstatusServiceNameService {
  private apiUrl = 'https://localhost:7068/api/FrontEndExecutive/DoctorRequests';

  constructor(private http: HttpClient) { }

  getDoctors(): Observable<Doctor[]> {
    return this.http.get<Doctor[]>(this.apiUrl);
  }

  approveDoctor(doctorId: number): Observable<any> {
    const approveUrl = `https://localhost:7068/api/FrontEndExecutive/ApproveDoctorRequest/${doctorId}`;
    return this.http.post(approveUrl, null);
  }

  declineDoctor(doctorId: number): Observable<any> {
    const declineUrl = `https://localhost:7068/api/FrontEndExecutive/DeclineDoctorRequest/${doctorId}`;
    return this.http.post(declineUrl, null);
  }

  approveddoctors(): Observable<Doctor[]> {
    const approvedUrl = `https://localhost:7068/api/FrontEndExecutive/DoctorRequests/approved`;
    return this.http.get<Doctor[]>(approvedUrl);
  }
  declineddoctors(): Observable<Doctor[]> {
    const approvedUrl = `https://localhost:7068/api/FrontEndExecutive/DoctorRequests/declined`;
    return this.http.get<Doctor[]>(approvedUrl);
  }
}

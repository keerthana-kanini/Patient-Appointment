import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PatientregisterServiceNameService {
  private apiUrl = 'https://localhost:7068/api/Patients';

  constructor(private http: HttpClient) {}

 registerPatient(patientData: any): Observable<any> {
    return this.http.post(this.apiUrl, patientData, { responseType: 'text' });
  } 
  
}

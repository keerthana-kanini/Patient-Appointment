import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Patient } from '../../Patient';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PatientloginServiceNameService {
  constructor(private http: HttpClient) { }

  login(patient_Email: string, patient_Password: string) {
    const signInData = { patient_Email, patient_Password };
   

    return this.http.post<Res>('https://localhost:7068/api/Logins/Patient', signInData);
  }
}
interface Res{
  status:string,
  is_fstlogin:string
}


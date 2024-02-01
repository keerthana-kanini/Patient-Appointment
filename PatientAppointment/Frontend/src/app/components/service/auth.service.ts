import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  // signInPatient: any;
  constructor(private http: HttpClient) { }

  signInExecutive(executive_Email: string, executive_Password: string) {
    const signInData = { executive_Email, executive_Password };
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
      responseType: 'text' as 'json'
    };

    return this.http.post<string>('https://localhost:7068/api/Logins/Admin', signInData, httpOptions);
  }
}

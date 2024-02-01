import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PaaswordService {

  private apiUrl = 'https://localhost:7068/api/Patients';

  constructor(private http: HttpClient) { }

  updatePassword( newPassword: string): Observable<any> {
    const url = `${this.apiUrl}/updatePassword/${localStorage.getItem('useremail')}?newPassword=${newPassword}`;
    console.log(url)
    return this.http.put(url,newPassword); 
  }
}

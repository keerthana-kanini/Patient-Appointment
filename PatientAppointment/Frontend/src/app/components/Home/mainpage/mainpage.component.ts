import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-mainpage',
  templateUrl: './mainpage.component.html',
  styleUrls: ['./mainpage.component.css']
})
export class MainpageComponent {
  constructor(private router: Router) {}

  onMakeAppointment(): void {
   
    const userEmail = localStorage.getItem('useremail');

   
    if (userEmail) {
      this.router.navigate(['/bookappointment']);
    } else {
      this.router.navigate(['/patientlogin']);
    }
  }
}

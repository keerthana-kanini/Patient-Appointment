import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../service/auth.service';

@Component({
  selector: 'app-adminlogin',
  templateUrl: './adminlogin.component.html',
  styleUrls: ['./adminlogin.component.css']
})
export class AdminLoginComponent {
  executive_Email: string = '';
  executive_Password: string = '';
  submitted: boolean = false;

  constructor(private authService: AuthService, private router: Router) {}

  login(): void {
    this.submitted = true;

    if (this.executive_Email && this.executive_Password) {
      this.authService.signInExecutive(this.executive_Email, this.executive_Password).subscribe(
        response => {
          localStorage.setItem('username', this.executive_Email);
          alert("Logged in successfully!");
          this.router.navigateByUrl('/docsts');
        },
        error => {
          alert("Incorrect Email/Password");
          console.log(error);
        }
      );
    }
  }
}
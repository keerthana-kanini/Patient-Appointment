import { Component } from '@angular/core';
import { DoctorstatusServiceNameService } from '../../service/doctorstatus-service-name.service';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';


@Component({
  selector: 'app-doctorlogin',
  templateUrl: './doctorlogin.component.html',
  styleUrl: './doctorlogin.component.css'
})
export class DoctorloginComponent {
  doctorLoginForm: FormGroup;
  submitted: boolean = false;

  constructor(
    private doctorService: DoctorstatusServiceNameService,
    private router: Router,
    private toast: NgToastService,
    private formBuilder: FormBuilder
  ) {
    // Initialize the form with validators
    this.doctorLoginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }

  // Accessors for form controls
  get formControls() {
    return this.doctorLoginForm.controls;
  }

  onSubmit(): void {
    this.submitted = true;

    if (this.doctorLoginForm.valid) {
      const { email, password } = this.doctorLoginForm.value;

      this.doctorService.login(email, password).subscribe(
        (response: any) => {
          const status=response.status
          console.log(status)
          localStorage.setItem('username', email);
          if(status==='Approved')
          {
            this.toast.success({ detail: 'SUCCESS', summary: 'Logged in successfully!' });
            this.router.navigateByUrl('/docapp');
            
          }
          else if(status==='Declined')
          {
            this.toast.error({ detail: 'ERROR', summary: 'Your account has been deactivated' });
          }
          else{
            this.toast.error({ detail: 'ERROR', summary: 'Your account has not verified' });
          }
        },
        (error: any) => {
          this.toast.error({ detail: 'ERROR', summary: 'Incorrect Email/Password' });
          console.log(error);
        }
      );
    }
  }
}  

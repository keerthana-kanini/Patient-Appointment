import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';  
import { Router } from '@angular/router';
import { PatientloginServiceNameService } from '../../service/patientlogin-service-name.service';
import { NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'app-patient-login',
  templateUrl: './patientlogin.component.html',
  styleUrls: ['./patientlogin.component.css'],
})
export class PatientloginComponent {
  loginForm: FormGroup;  
  submitted: boolean = false;

  constructor(
    private patientService: PatientloginServiceNameService,
    private router: Router,
    private toast: NgToastService,
    private formBuilder: FormBuilder,  
  ) {
   
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  
  get formControls() {
    return this.loginForm.controls;
  }

  onSubmit(): void {
    this.submitted = true;
  
    if (this.loginForm.valid) { 
      const email = this.loginForm.get('email')?.value;
      const password = this.loginForm.get('password')?.value;
  
      this.patientService.login(email, password).subscribe(
        (response: any) => {
          const status=response.status
          const fst=response.is_fstlogin
          console.log(status,fst)
          localStorage.setItem('useremail', email);
          if(status==='Approved')
          {
            this.toast.success({ detail: 'SUCCESS', summary: 'Logged in successfully!' });
            if(fst===true)
            this.router.navigateByUrl('/main');
            else this.router.navigateByUrl('/password');
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

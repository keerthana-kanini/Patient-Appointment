import { Component } from '@angular/core';
import { PatientregisterServiceNameService } from '../service/patientregister-service-name.service';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'app-patientregister',
  templateUrl: './patientregister.component.html',
  styleUrls: ['./patientregister.component.css']
})
export class PatientregisterComponent {
  patientModel: any = {};
  registrationResponse: any;
  submitted: boolean = false;
  maxDate: string;

  constructor(
    private patientRegisterService: PatientregisterServiceNameService,
    private router: Router,
    private toast: NgToastService
  )  {
    const today = new Date();
    const year = today.getFullYear();
    const month = ('0' + (today.getMonth() + 1)).slice(-2);
    const day = ('0' + today.getDate()).slice(-2);
    this.maxDate = `${year}-${month}-${day}`;
  }

  onSubmit(): void {
    this.submitted = true;

    this.patientRegisterService.registerPatient(this.patientModel)
      .subscribe(
        (response) => {
          console.log('Registration successful', response);
          this.toast.success({ detail: 'SUCCESS', summary: 'Registration successful!' });
          this.router.navigateByUrl('/patientlogin');
        },
        (error: any) => {

          this.toast.error({ detail: "Error", summary: "Email already exists"});

          console.log(error);
        }
      );

  }
}

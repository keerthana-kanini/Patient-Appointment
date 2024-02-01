import { Component } from '@angular/core';
import { DoctorstatusServiceNameService } from '../../service/doctorstatus-service-name.service';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'app-doctorregister',
  templateUrl: './doctorregister.component.html',
  styleUrls: ['./doctorregister.component.css'] 
})
export class DoctorregisterComponent {
  doctorModel: any = {};
  registrationResponse: any;
  submitted: boolean = false;
  maxDate: string; // Variable to store the max date

  constructor(
    private doctorRegisterService: DoctorstatusServiceNameService, 
    private router: Router,
    private toast: NgToastService,
  ) {
   
    const today = new Date();
    const year = today.getFullYear();
    const month = ('0' + (today.getMonth() + 1)).slice(-2);
    const day = ('0' + today.getDate()).slice(-2);
    this.maxDate = `${year}-${month}-${day}`;
  }

  onSubmit(): void {
    this.submitted = true;

    
    if (this.isFormValid()) {
      this.doctorRegisterService.registerdoctor(this.doctorModel)
        .subscribe(
          (response: any) => {
            console.log('Doctor registration successful', response);
            this.toast.success({ detail: 'SUCCESS', summary: 'Doctor registration successful!' });
            this.router.navigateByUrl('/doctorlogin');
          },
          (error: any) => {
            console.error('Doctor registration failed', error);
            this.toast.error({ detail: 'ERROR', summary: 'Doctor registration failed. Please try again.' });
          }
        );
    } else {
      this.toast.error({ detail: 'ERROR', summary: 'Please fill in all required fields.' });
    }
  }

  private isFormValid(): boolean {
   
    return (
      this.doctorModel.doctor_Name &&
      this.doctorModel.doctor_Email &&
      this.doctorModel.doctor_Gender &&
      this.doctorModel.doctor_DateOfBirth &&
      this.doctorModel.doctor_Phone &&
      this.doctorModel.doctor_Location &&
      this.doctorModel.doctor_Specialization &&
      this.doctorModel.doctor_Password
    );
  }
}

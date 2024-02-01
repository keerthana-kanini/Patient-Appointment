import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Patient } from '../../../Patient';
import { Doctor } from '../../../Doctor';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { NgToastService } from 'ng-angular-popup';


@Component({
  selector: 'app-appointmentpage',
  templateUrl: './appointmentpage.component.html',
  styleUrls: ['./appointmentpage.component.css']
})
export class AppointmentpageComponent {
  minDate: string;
  patient: Patient | null;
  doctor: Doctor | null | undefined;
  appointmentForm: FormGroup;
 

  availableTimes: string[] = ['10:00', '10:30', '11:00', '11:30', '12:00','15:00','15:30',];
  bookedSlots: string[] = [];

  constructor(private fb: FormBuilder, private http: HttpClient, private router: Router,private toast: NgToastService) {
    const today = new Date();
    this.minDate = this.formatDate(today);
    

    const patientId = localStorage.getItem('selectedPatientId');
    

    this.appointmentForm = this.fb.group({
      appointmentDate: ['', Validators.required],
      appointmentTime: ['', Validators.required],
      symptoms: ['', Validators.required],
    });
    const timeoutDuration = 10 * 60 * 1000;

setTimeout(() => {

  localStorage.clear();
  console.log('All items removed from localStorage.');
}, timeoutDuration);

    if (patientId !== null) {
      const patientName = localStorage.getItem('selectedPatientName') || '';
      const patientEmail = localStorage.getItem('selectedPatientEmail') || '';
      const patientPhone = localStorage.getItem('selectedPatientPhone') || '';
      const patientLocation = localStorage.getItem('selectedPatientLocation') || '';

      this.patient = {
        patient_ID: +patientId,
        patient_Name: patientName,
        patient_Email: patientEmail,
        patient_Phone: patientPhone,
        patient_Location: patientLocation,
        patient_Gender: '',
        patient_DateOfBirth: new Date(),
        patient_Password: '',
        patient_Status: 'pending'
      };
    } else {
      this.patient = null;
    }
  }

  private formatDate(date: Date): string {
    const newDate = new Date(date);
    newDate.setDate(newDate.getDate() + 3);
    const year = newDate.getFullYear();
    const month = (newDate.getMonth() + 1).toString().padStart(2, '0');
    const day = newDate.getDate().toString().padStart(2, '0');

    return `${year}-${month}-${day}`;
  }

  showConfirmation(): void {
    console.log('Confirmation shown');
  }

  onConfirm() {
    const id = localStorage.getItem('selectedPatientId');
    const doctid = localStorage.getItem('selectedDoctorId');
    const appointmentDate = this.appointmentForm.get('appointmentDate')?.value;
    const appointmentTime = this.appointmentForm.get('appointmentTime')?.value;
  
    const selectedTime = appointmentTime.toString();
  
    if (this.bookedSlots.includes(selectedTime)) {
      console.error('Error booking appointment. Time slot already booked.');
      alert('Time slot already booked. Please choose another time or date.');
      return;
    }
  
    const allvalues = {
      patient_ID: id,
      doctor_ID: doctid,
      appointmentDate: appointmentDate,
      appointmentTime: selectedTime
    };
  
    this.http.post('https://localhost:7068/api/Appointments/BookAppointment', allvalues)
      .subscribe(
        (response: any) => {
          console.log('Appointment booked successfully', response);
          this.showConfirmation();
          this.router.navigate(['/success']);
        
          this.bookedSlots.push(selectedTime);
        },
        (error: any) => {
          console.error('Error booking appointment', error);
  
          if (error instanceof HttpErrorResponse) {
            console.error('Server error details:', error.error);
  
            if (error.error && error.error.errors) {
              console.error('Validation errors:', error.error.errors);
              alert('Error booking appointment. Please check the provided details.');
            } else {
              alert('Time slot already booked. Please choose another time or date.');
            }
          }
        }
      );
  }
  

  private showToast(message: string, type: string): void {
  
  }
}
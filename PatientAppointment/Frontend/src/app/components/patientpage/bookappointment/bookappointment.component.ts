import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Doctor } from '../../../Doctor';
import { Patient } from '../../../Patient';
import { Router } from '@angular/router';
 
 
@Component({
  selector: 'app-bookappointment',
  templateUrl: './bookappointment.component.html',
  styleUrls: ['./bookappointment.component.css']
})
export class BookappointmentComponent implements OnInit {
 
  originalDoctors: Doctor[] = [];
  doctors: Doctor[] = [];
  doctorPhotoIndices: { [id: number]: number } = {};
  selectedSpecialization: string | null = null;
 
  patient: Patient | null = null; 
 
  constructor(
    private http: HttpClient,
    private router: Router
  ) {}
 
  ngOnInit() {
    this.http.get<Doctor[]>('https://localhost:7068/api/Doctors')
      .subscribe(doctors => {
        this.originalDoctors = doctors;
        this.doctors = this.originalDoctors.slice();
        this.filterDoctors();
        this.generateDoctorPhotoIndices();
      });
 
 
    this.http.get<Patient>(`https://localhost:7068/api/Patients/email?email=${localStorage.getItem('useremail')}`)
      .subscribe(patient => {
        this.patient = patient;
        localStorage.setItem('selectedPatientId', patient.patient_ID.toString());
        localStorage.setItem('selectedPatientName', patient.patient_Name);
        localStorage.setItem('selectedPatientEmail', patient.patient_Email);
        localStorage.setItem('selectedPatientLocation', patient.patient_Location);
        localStorage.setItem('selectedPatientPhone', patient.patient_Phone);
      });
  }
 
  filterDoctors(): void {
    if (this.selectedSpecialization) {
      this.doctors = this.originalDoctors.filter(doctor => doctor.doctor_Specialization === this.selectedSpecialization);
    } else {
      this.doctors = this.originalDoctors.slice();
    }
  }
 
  generateDoctorPhotoIndices(): void {
    this.doctors.forEach(doctor => {
      this.doctorPhotoIndices[doctor.doctor_ID] = Math.floor(Math.random() * this.doctorPhotos.length);
    });
  }
 
  bookAppointment(doctor: Doctor) {
    const selectedPatientId = localStorage.getItem('selectedPatientId');
    const selectedPatientName = localStorage.getItem('selectedPatientName');
    const selectedPatientEmail = localStorage.getItem('selectedPatientEmail');
    const selectedPatientLocation = localStorage.getItem('selectedPatientLocation');
    const selectedPatientPhone = localStorage.getItem('selectedPatientPhone');
    localStorage.setItem('selectedDoctorId', doctor.doctor_ID.toString());
    localStorage.setItem('selectedDoctorName', doctor.doctor_Name);
    console.log(`Booking appointment with ${doctor.doctor_Name}`);
    this.router.navigate(['/appointment', doctor.doctor_ID]);
 
    console.log(`Booking appointment with ${doctor.doctor_Name} for patient ${selectedPatientName} (ID: ${selectedPatientId})`);
    console.log(`Patient Email: ${selectedPatientEmail}`);
    console.log(`Patient Location: ${selectedPatientLocation}`);
    console.log(`Patient Phone: ${selectedPatientPhone}`);
 
  
 
    this.router.navigate(['/appointment', doctor.doctor_ID]);
  }
 
  getDoctorPhotoUrl(doctor: Doctor): string {
    const randomIndex = this.doctorPhotoIndices[doctor.doctor_ID];
    return this.doctorPhotos[randomIndex] || '';
  }
 
  private doctorPhotos: string[] = [
    'https://www.kailashhealthcare.com/Content/images/Doctor/8G523KEIDr.%20Anuj%20JainKailash%20Hospital%20%20Neuro%20Institute.jpg.jpg',
    'https://www.kailashhealthcare.com/Content/images/Doctor/I7H0MODADr.%20Neha%20GuptaKailash%20Hospital,%20Noida.jpg.jpg',
    'https://www.kailashhealthcare.com/Content/images/Doctor/8ZN3503QDr.%20Tajinder%20KaurKailash%20Hospital,%20Noida.jpg.jpg',
    'https://www.logintohealth.com/assets/uploads/doctors/65dc6f651684c0ae8d0d33c38c126a84.jpg',
    'https://i.pinimg.com/736x/cd/25/e3/cd25e3fa59d82820b10b7ffb71f058de.jpg',
    'https://www.kimshealth.org/media/filer_public/3b/40/3b406f7c-db0e-4d99-bd05-74fe82d47e0e/dr_premkumar.jpg',
    'https://img.freepik.com/premium-photo/confidence-is-everything-my-career-studio-portrait-confident-mature-doctor-against-gray-background_590464-55938.jpg'
  ];
 
  getUniqueSpecializations(): string[] {
    return Array.from(new Set(this.originalDoctors.map(doctor => doctor.doctor_Specialization)));
  }
}
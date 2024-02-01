import { Component, OnInit } from '@angular/core';
import { DocstatusServiceNameService } from './docstatus-service-name.service';
import { Doctor } from '../../../Doctor';

@Component({
  selector: 'app-doctorstatus',
  templateUrl: './doctorstatus.component.html',
  styleUrls: ['./doctorstatus.component.css']
})
export class DoctorstatusComponent implements OnInit {
  doctors: Doctor[] = [];
  filteredDoctors: Doctor[] = []; // New array for filtered doctors
  searchTerm: string = ''; // Variable to store the search term

  constructor(private doctorService: DocstatusServiceNameService) { }

  ngOnInit(): void {
    this.loadDoctors();
  }

  loadDoctors(): void {
    this.doctorService.getDoctors().subscribe(
      (data) => {
        this.doctors = data;
        this.filteredDoctors = [...this.doctors]; 
      },
      (error) => {
        console.error('Error fetching doctors', error);
      }
    );
  }

  approveDoctor(doctor: Doctor): void {
    this.doctorService.approveDoctor(doctor.doctor_ID).subscribe(
      () => {
        this.loadDoctors();
        alert("Approved successfully!");
      },
      (error) => {
        console.error('Error approving doctor', error);
      }
    );
  }

  declineDoctor(doctor: Doctor): void {
    this.doctorService.declineDoctor(doctor.doctor_ID).subscribe(
      () => {
        this.loadDoctors();
        alert("Declined successfully!");
      },
      (error) => {
        console.error('Error declining doctor', error);
      }
    );
  }


  filterDoctors(): void {
    this.filteredDoctors = this.doctors.filter(doctor =>
      doctor.doctor_Name.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
  }
}

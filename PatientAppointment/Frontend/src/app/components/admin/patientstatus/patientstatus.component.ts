import { Component, OnInit } from '@angular/core';
import { PatstatusServiceNameService } from '../../service/patstatus-service-name.service';
import { Patient } from '../../../Patient';

@Component({
  selector: 'app-patientstatus',
  templateUrl: './patientstatus.component.html',
  styleUrls: ['./patientstatus.component.css']
})
export class PatientstatusComponent implements OnInit {
  patients: Patient[] = [];
  filteredPatients: Patient[] = []; 
  searchTerm: string = ''; 

  constructor(private patientService: PatstatusServiceNameService) { }

  ngOnInit(): void {
    this.loadPatients();
  }

  loadPatients(): void {
    this.patientService.getPatients().subscribe(
      (data) => {
        this.patients = data;
        this.filteredPatients = [...this.patients];
      },
      (error) => {
        console.error('Error fetching patients', error);
      }
    );
  }

  approvePatient(patient: Patient): void {
    this.patientService.approvePatient(patient.patient_ID).subscribe(
      () => {
        patient.patient_Status = 'Approved';
        alert("Approved successfully!");
      },
      (error) => {
        console.error('Error approving patient', error);
      }
    );
  }

  declinePatient(patient: Patient): void {
    this.patientService.declinePatient(patient.patient_ID).subscribe(
      () => {
        patient.patient_Status = 'Declined';
        alert("Declined successfully!");
      },
      (error) => {
        console.error('Error declining patient', error);
      }
    );
  }

  filterPatients(): void {
    this.filteredPatients = this.patients.filter(patient =>
      patient.patient_Name.toLowerCase().includes(this.searchTerm.toLowerCase())
    );
  }
}

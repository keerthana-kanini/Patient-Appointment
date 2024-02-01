import { Component } from '@angular/core';
import { Patient } from '../../../Patient';
import { PatstatusServiceNameService } from '../../service/patstatus-service-name.service';

@Component({
  selector: 'app-patientapprove',
  templateUrl: './patientapprove.component.html',
  styleUrl: './patientapprove.component.css'
})
export class PatientapproveComponent {
  patients: Patient[] = [];

  constructor(private patientService: PatstatusServiceNameService) { }

  ngOnInit(): void {
    this.loadPatients();
  }

  loadPatients(): void {
    this.patientService.patientapproved().subscribe(
      (data) => {
        this.patients = data;
      },
      (error) => {
        console.error('Error fetching patients', error);
      }
    );
  }

}

import { Component } from '@angular/core';
import { PatstatusServiceNameService } from '../../service/patstatus-service-name.service';
import { Patient } from '../../../Patient';

@Component({
  selector: 'app-patientdecline',
  templateUrl: './patientdecline.component.html',
  styleUrl: './patientdecline.component.css'
})
export class PatientdeclineComponent {
  patients: Patient[] = [];

  constructor(private patientService: PatstatusServiceNameService) { }

  ngOnInit(): void {
    this.loadPatients();
  }

  loadPatients(): void {
    this.patientService.patientdeclined().subscribe(
      (data) => {
        this.patients = data;
      },
      (error) => {
        console.error('Error fetching patients', error);
      }
    );
  }

}



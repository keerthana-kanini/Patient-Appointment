import { Component } from '@angular/core';
import { DocstatusServiceNameService } from '../doctorstatus/docstatus-service-name.service';
import { Doctor } from '../../../Doctor';

@Component({
  selector: 'app-doctorrequests',
  templateUrl: './doctorrequests.component.html',
  styleUrl: './doctorrequests.component.css'
})
export class DoctorrequestsComponent {
  doctors: Doctor[] = [];
  

  constructor(private doctorService: DocstatusServiceNameService) { }
  

  ngOnInit(): void {
    this.loadDoctors();
  }

  loadDoctors(): void {
    this.doctorService.approveddoctors().subscribe(
      (data) => {
        this.doctors = data;
      },
      (error) => {
        console.error('Error fetching doctors', error);
      }
    );
  }

}

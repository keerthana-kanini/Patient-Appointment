import { Component } from '@angular/core';
import { Doctor } from '../../../Doctor';
import { DocstatusServiceNameService } from '../doctorstatus/docstatus-service-name.service';

@Component({
  selector: 'app-doctordecline',
  templateUrl: './doctordecline.component.html',
  styleUrl: './doctordecline.component.css'
})
export class DoctordeclineComponent {
  doctors: Doctor[] = [];
  

  constructor(private doctorService: DocstatusServiceNameService) { }
  

  ngOnInit(): void {
    this.loadDoctors();
  }

  loadDoctors(): void {
    this.doctorService.declineddoctors().subscribe(
      (data) => {
        this.doctors = data;
      },
      (error) => {
        console.error('Error fetching doctors', error);
      }
    );
  }

}

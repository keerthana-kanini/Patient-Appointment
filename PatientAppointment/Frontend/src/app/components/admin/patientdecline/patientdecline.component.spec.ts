import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientdeclineComponent } from './patientdecline.component';

describe('PatientdeclineComponent', () => {
  let component: PatientdeclineComponent;
  let fixture: ComponentFixture<PatientdeclineComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PatientdeclineComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PatientdeclineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

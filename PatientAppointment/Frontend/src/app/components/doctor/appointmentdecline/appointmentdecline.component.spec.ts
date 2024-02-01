import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppointmentdeclineComponent } from './appointmentdecline.component';

describe('AppointmentdeclineComponent', () => {
  let component: AppointmentdeclineComponent;
  let fixture: ComponentFixture<AppointmentdeclineComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AppointmentdeclineComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AppointmentdeclineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

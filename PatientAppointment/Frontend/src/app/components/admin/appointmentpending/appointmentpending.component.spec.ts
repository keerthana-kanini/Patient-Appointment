import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppointmentpendingComponent } from './appointmentpending.component';

describe('AppointmentpendingComponent', () => {
  let component: AppointmentpendingComponent;
  let fixture: ComponentFixture<AppointmentpendingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AppointmentpendingComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AppointmentpendingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

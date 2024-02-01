import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppointmentapproveComponent } from './appointmentapprove.component';

describe('AppointmentapproveComponent', () => {
  let component: AppointmentapproveComponent;
  let fixture: ComponentFixture<AppointmentapproveComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AppointmentapproveComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AppointmentapproveComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AppointmentrequestComponent } from './appointmentrequest.component';

describe('AppointmentrequestComponent', () => {
  let component: AppointmentrequestComponent;
  let fixture: ComponentFixture<AppointmentrequestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AppointmentrequestComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AppointmentrequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PatientstatusComponent } from './patientstatus.component';

describe('PatientstatusComponent', () => {
  let component: PatientstatusComponent;
  let fixture: ComponentFixture<PatientstatusComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PatientstatusComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(PatientstatusComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

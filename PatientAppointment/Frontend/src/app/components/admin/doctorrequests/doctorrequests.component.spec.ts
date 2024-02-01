import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DoctorrequestsComponent } from './doctorrequests.component';

describe('DoctorrequestsComponent', () => {
  let component: DoctorrequestsComponent;
  let fixture: ComponentFixture<DoctorrequestsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DoctorrequestsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DoctorrequestsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

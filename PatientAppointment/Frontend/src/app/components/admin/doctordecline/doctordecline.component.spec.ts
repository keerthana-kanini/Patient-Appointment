import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DoctordeclineComponent } from './doctordecline.component';

describe('DoctordeclineComponent', () => {
  let component: DoctordeclineComponent;
  let fixture: ComponentFixture<DoctordeclineComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DoctordeclineComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(DoctordeclineComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

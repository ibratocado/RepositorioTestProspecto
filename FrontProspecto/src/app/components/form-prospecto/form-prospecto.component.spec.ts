import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormProspectoComponent } from './form-prospecto.component';

describe('FormProspectoComponent', () => {
  let component: FormProspectoComponent;
  let fixture: ComponentFixture<FormProspectoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FormProspectoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FormProspectoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

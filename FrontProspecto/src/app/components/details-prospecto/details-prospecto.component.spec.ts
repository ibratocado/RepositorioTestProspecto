import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetailsProspectoComponent } from './details-prospecto.component';

describe('DetailsProspectoComponent', () => {
  let component: DetailsProspectoComponent;
  let fixture: ComponentFixture<DetailsProspectoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DetailsProspectoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DetailsProspectoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

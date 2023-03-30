import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PageAddProspectoComponent } from './page-add-prospecto.component';

describe('PageAddProspectoComponent', () => {
  let component: PageAddProspectoComponent;
  let fixture: ComponentFixture<PageAddProspectoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PageAddProspectoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PageAddProspectoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

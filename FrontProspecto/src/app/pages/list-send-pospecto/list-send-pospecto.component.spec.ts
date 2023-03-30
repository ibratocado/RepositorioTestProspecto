import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListSendPospectoComponent } from './list-send-pospecto.component';

describe('ListSendPospectoComponent', () => {
  let component: ListSendPospectoComponent;
  let fixture: ComponentFixture<ListSendPospectoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListSendPospectoComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ListSendPospectoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

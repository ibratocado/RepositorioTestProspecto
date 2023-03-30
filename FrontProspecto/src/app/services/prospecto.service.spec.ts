import { TestBed } from '@angular/core/testing';

import { ProspectoService } from './prospecto.service';

describe('ProspectoService', () => {
  let service: ProspectoService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProspectoService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

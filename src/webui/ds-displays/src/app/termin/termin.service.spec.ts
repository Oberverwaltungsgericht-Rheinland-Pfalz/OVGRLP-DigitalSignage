import { TestBed, inject } from '@angular/core/testing';

import { TerminService } from './termin.service';

describe('TerminService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TerminService]
    });
  });

  it('should ...', inject([TerminService], (service: TerminService) => {
    expect(service).toBeTruthy();
  }));
});

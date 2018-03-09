import { TestBed, inject } from '@angular/core/testing';

import { TerminService } from './termin.service';

describe('TerminService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TerminService]
    });
  });

  it(
    'should be created',
    inject([TerminService], (service: TerminService) => {
      expect(service).toBeTruthy();
    })
  );
});
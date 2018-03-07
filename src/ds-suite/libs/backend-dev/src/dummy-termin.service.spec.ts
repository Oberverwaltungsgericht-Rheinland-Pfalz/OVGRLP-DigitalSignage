import { TestBed, inject } from '@angular/core/testing';

import { DummyTerminService } from './dummy-termin.service';

describe('DummyTerminService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DummyTerminService]
    });
  });

  it(
    'should be created',
    inject([DummyTerminService], (service: DummyTerminService) => {
      expect(service).toBeTruthy();
    })
  );
});

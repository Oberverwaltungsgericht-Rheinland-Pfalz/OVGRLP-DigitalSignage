import { TestBed, inject } from '@angular/core/testing';

import { DummyDisplayService } from './dummy-display.service';

describe('DummyDisplayService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DummyDisplayService]
    });
  });

  it(
    'should be created',
    inject([DummyDisplayService], (service: DummyDisplayService) => {
      expect(service).toBeTruthy();
    })
  );
});

import { TestBed, inject } from '@angular/core/testing';

import { DisplaysService } from './displays.service';

describe('DisplaysService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DisplaysService]
    });
  });

  it('should ...', inject([DisplaysService], (service: DisplaysService) => {
    expect(service).toBeTruthy();
  }));
});

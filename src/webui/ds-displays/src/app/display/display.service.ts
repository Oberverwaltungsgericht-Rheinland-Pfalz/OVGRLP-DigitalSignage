import { Injectable } from '@angular/core';
import { Display } from './display'

@Injectable()
export class DisplayService {

  constructor() { }

  getDisplays(): Promise<Display[]> {
    return Promise.resolve([
      { id: 1, name: 'Test1' },
      { id: 2, name: 'Test2' }
    ]);
  }
}
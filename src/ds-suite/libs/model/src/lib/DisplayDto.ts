import { Display } from './display';
import { DisplayStatus } from './display-status';

export class DisplayDto extends Display {
    Status: DisplayStatus;
    ScreenshotUrl: string;
  }
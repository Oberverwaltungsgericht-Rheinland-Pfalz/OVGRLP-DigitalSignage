import { Restriction } from './restriction';

export class BasicPermissions {
    allowDisplays?: boolean = false;
    allowDisplaysControl?: boolean = false;
    allowTermine?: Restriction = Restriction.forbidden;
    allowNotes?: Restriction = Restriction.forbidden;
  }
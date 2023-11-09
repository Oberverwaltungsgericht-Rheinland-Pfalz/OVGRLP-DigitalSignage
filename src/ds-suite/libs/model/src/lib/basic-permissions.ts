// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { Restriction } from './restriction';

export class BasicPermissions {
    allowDisplays?: boolean = false;
    allowDisplaysControl?: boolean = false;
    allowTermine?: Restriction = Restriction.forbidden;
    allowNotes?: Restriction = Restriction.forbidden;

    public static isAdmin(perm: BasicPermissions) {
      return (perm.allowNotes == Restriction.write);
    }
  }
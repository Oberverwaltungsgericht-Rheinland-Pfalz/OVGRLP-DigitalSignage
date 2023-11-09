// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { Display } from './display';
import { DisplayStatus } from './display-status';

export class DisplayDto extends Display {
    Status: DisplayStatus;
    ScreenshotUrl: string;
  }
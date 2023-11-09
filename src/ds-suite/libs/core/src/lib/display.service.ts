// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs/Observable';
import { Display, DisplayDto, DisplayStatus, Note } from '@ds-suite/model';

@Injectable()
export abstract class DisplayService {
  abstract getDisplays(): Observable<Display[]>;

  abstract getDisplaysDto(): Observable<DisplayDto[]>;

  abstract getDisplay(name: string): Observable<Display>;

  abstract getDisplayDto(name: string): Observable<DisplayDto>;

  abstract getDisplayNotes(name: string): Observable<Note[]>;

  abstract getDisplayStatus(display: Display): Observable<DisplayStatus>;

  abstract getScreenshotUrl(display: Display): Observable<string>;

  abstract startDisplay(display: Display): Observable<void>;

  abstract restartDisplay(display: Display): Observable<void>;

  abstract stopDisplay(display: Display): Observable<void>;
}
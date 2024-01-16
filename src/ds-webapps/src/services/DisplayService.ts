// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import Note from '../models/Note'
import Display from '../models/Display'
import DisplayDto from '../models/DisplayDto'
import { DisplayStatus } from '../models/display-status'

export default abstract class DisplayService {
  abstract getDisplays (): Promise<Display[]>

  abstract getDisplaysDto (): Promise<DisplayDto[]>

  abstract getDisplay (name: string): Promise<Display>

  abstract getDisplayDto (name: string): Promise<DisplayDto>

  abstract getDisplayNotes (name: string): Promise<Note[]>

  abstract getDisplayStatus (display: Display): Promise<DisplayStatus>

  abstract getScreenshotUrl (display: Display): Promise<string>

  abstract startDisplay (display: Display): Promise<void>

  abstract restartDisplay (display: Display): Promise<void>

  abstract stopDisplay (display: Display): Promise<void>
}

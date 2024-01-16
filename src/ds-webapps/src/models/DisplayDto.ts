// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import Display from './Display'
import { DisplayStatus } from './display-status'

export default interface DisplayDto extends Display {
  Status: DisplayStatus
  ScreenshotUrl: string
}

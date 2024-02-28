// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
export interface AppSettings {
  webApiUrl: string
  useWindowsAuthentication: boolean
}

export interface AppSettingsRoomcontrol extends AppSettings {
  showBesetzung: false
}

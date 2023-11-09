// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
export class AppConfig {
  webApiUrl: string;
  useWindowsAuthentication: boolean;
}

export class AppConfigRoomcontrol extends AppConfig {
  showBesetzung: false
}

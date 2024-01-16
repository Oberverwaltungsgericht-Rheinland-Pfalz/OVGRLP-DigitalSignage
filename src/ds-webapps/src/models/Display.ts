// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
export default interface Display {
  id: number
  name: string
  title: string
  template: string
  styles: string
  filter: string
  group: string
  controlUrl: string
  netAddress: string
  wolIpAddress: string
  wolMacAddress: string
  wolUdpPort: number
  description: string
  dummy: boolean
}

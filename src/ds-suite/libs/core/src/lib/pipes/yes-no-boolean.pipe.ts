// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { Pipe, PipeTransform } from '@angular/core'

@Pipe({
  name: 'yesnoBoolean'
})
export class YesNoBooleanPipe implements PipeTransform {
  transform (value: string, args?: any): any {
    return !!((value.toLowerCase() == 'ja' || value.toLowerCase() == 'yes'))
  }
}

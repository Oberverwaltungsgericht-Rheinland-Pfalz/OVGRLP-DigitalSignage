import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'yesnoBoolean'
})
export class YesNoBooleanPipe implements PipeTransform {
  transform(value: string, args?: any): any {
    return (value.toLowerCase() == 'ja' || value.toLowerCase()=='yes') ? true : false
  }
}
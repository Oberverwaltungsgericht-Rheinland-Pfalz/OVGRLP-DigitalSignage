// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { Component, OnInit, Input } from '@angular/core'
import { ObjectProperty } from '@ds-suite/model'

@Component({
  selector: 'object-properties-dialog',
  templateUrl: './object-properties-dialog.component.html',
  styleUrls: ['./object-properties-dialog.component.css']
})
export class ObjectPropertiesDialogComponent implements OnInit {
  public show: boolean = false
  listOfPropertys: ObjectProperty[]
  _objectData: Object
  _title: string

  @Input()
  set Title (title: string) { this._title = title }

  get Title (): string { return this._title }

  @Input()
  set ObjectData (objectData: Object) { this._objectData = objectData }

  get ObjectData (): Object { return this._objectData }

  constructor () { }

  ngOnInit () {
  }

  open (objectData: Object, title: string) {
    this.ObjectData = objectData
    this.Title = title
    this.GetJsonPropertys(this.ObjectData)
    this.show = true
  }

  close () {
    this.show = false
  }

  private GetJsonPropertys (objectData: Object) {
    this.listOfPropertys = new Array<ObjectProperty>()
    let prop: ObjectProperty
    if (objectData != null && objectData != undefined) {
      // var jObj = JSON.parse(objectData) as Object;
      for (const propertyName in objectData) {
        prop = new ObjectProperty()
        prop.Name = propertyName
        prop.Value = objectData[propertyName]
        this.listOfPropertys.push(prop)
      }
    }
  }
}

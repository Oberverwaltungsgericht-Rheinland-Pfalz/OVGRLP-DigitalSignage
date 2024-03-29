// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { Component, OnInit, Input } from '@angular/core'

import { Alert, AlertType } from '@ds-suite/model'
import { AlertService } from '@ds-suite/core'

@Component({

  selector: 'alert',
  templateUrl: './alert.component.html',
  styleUrls: ['./alert.component.css']
})

export class AlertComponent {
  @Input() id: string

  alerts: Alert[] = []

  constructor (private readonly alertService: AlertService) { }

  ngOnInit () {
    this.alertService.getAlert(this.id).subscribe((alert: Alert) => {
      if (!alert.message) {
        // clear alerts when an empty alert is received
        this.alerts = []
        return
      }

      // add alert to array
      this.alerts.push(alert)
    })
  }

  removeAlert (alert: Alert) {
    this.alerts = this.alerts.filter(x => x !== alert)
  }

  cssClass (alert: Alert) {
    if (!alert) {
      return
    }

    // return css class based on alert type
    switch (alert.type) {
      case AlertType.Success:
        return 'alert alert-success'
      case AlertType.Error:
        return 'alert alert-danger'
      case AlertType.Info:
        return 'alert alert-info'
      case AlertType.Warning:
        return 'alert alert-warning'
    }
  }
}

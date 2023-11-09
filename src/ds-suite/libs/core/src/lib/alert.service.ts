// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { Injectable } from '@angular/core'
import { Router, NavigationStart } from '@angular/router'
import { Observable, Subject } from 'rxjs'

import { filter } from 'rxjs/operators'

import { Alert, AlertType } from '@ds-suite/model'

// https://github.com/cornflourblue/angular2-alert-notifications

@Injectable()
export class AlertService {
  private readonly subject = new Subject<Alert>()
  private keepAfterRouteChange = false

  constructor (private readonly router: Router) {
    // clear alert messages on route change unless 'keepAfterRouteChange' flag is true
    router.events.subscribe(event => {
      if (event instanceof NavigationStart) {
        if (this.keepAfterRouteChange) {
          // only keep for a single route change
          this.keepAfterRouteChange = false
        } else {
          // clear alert messages
          this.clear()
        }
      }
    })
  }

  // subscribe to alerts
  getAlert (alertId?: string): Observable<any> {
    return this.subject.asObservable().pipe(filter((x: Alert) => x && x.alertId === alertId))
  }

  // convenience methods
  success (message: string) {
    this.alert(new Alert({ message, type: AlertType.Success }))
  }

  error (message: string) {
    this.alert(new Alert({ message, type: AlertType.Error }))
  }

  info (message: string) {
    this.alert(new Alert({ message, type: AlertType.Info }))
  }

  warn (message: string) {
    this.alert(new Alert({ message, type: AlertType.Warning }))
  }

  // main alert method
  alert (alert: Alert) {
    this.keepAfterRouteChange = alert.keepAfterRouteChange
    this.subject.next(alert)
  }

  // clear alerts
  clear (alertId?: string) {
    this.subject.next(new Alert({ alertId }))
  }
}

// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpHandler, HttpRequest, HttpEvent, HttpResponse, HttpErrorResponse }  from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import { tap } from 'rxjs/operators';

import { AppConfig } from '@ds-suite/model';
import { ConfigService } from '../config.service';
import { AlertService } from '../alert.service';

@Injectable()
export class CredentialsInterceptor implements HttpInterceptor {

    _config: AppConfig;
    get config(): AppConfig { 
        if (this._config==undefined){
            this._config=this.configService.getConfig();
        }
        return this._config; 
    }

    constructor(private configService: ConfigService, private alertService: AlertService){}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (this.config!=undefined && this.config.useWindowsAuthentication==true){
            req = req.clone({
                withCredentials: true
            });
        }
        return next.handle(req).pipe(tap((event: HttpEvent<any>) => {}, (err: any) => {
            if (err instanceof HttpErrorResponse) {
                if(err.status==403) {
                    this.alertService.error("Zugriff nicht erlaubt: " + err.statusText)
                }                
            }
        }));
    }
}
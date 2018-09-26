import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpHandler, HttpRequest, HttpEvent, HttpResponse }  from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/do';

import { AppConfig } from '@ds-suite/model';
import { ConfigService } from '../config.service';

@Injectable()
export class CredentialsInterceptor implements HttpInterceptor {

    _config: AppConfig;
    get config(): AppConfig { 
        if (this._config==undefined){
            this._config=this.configService.getConfig();
        }
        return this._config; 
    }

    constructor(private configService: ConfigService){}

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (this.config!=undefined && this.config.useWindowsAuthentication==true){
            req = req.clone({
                withCredentials: true
            });
        }
        return next.handle(req);
    }
}
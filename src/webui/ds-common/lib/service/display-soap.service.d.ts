import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import { DisplayService } from './display.service';
import { Display, DisplayStatus } from '../model';
export declare class DisplaySoapService implements DisplayService {
    private http;
    constructor(http: HttpClient);
    getDisplays(): Observable<Display[]>;
    getDisplay(name: string): Observable<Display>;
    getDisplayStatus(display: Display): Observable<DisplayStatus>;
    private extractJson(res);
    private extractDisplayStatus(res);
}

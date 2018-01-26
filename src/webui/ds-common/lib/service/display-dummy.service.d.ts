import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/catch";
import "rxjs/add/operator/map";
import { DisplayService } from "./display.service";
import { Display, DisplayStatus } from "../model";
export declare class DisplayDummyService implements DisplayService {
    private apiUrl;
    constructor();
    getDisplays(): Observable<Display[]>;
    getDisplay(name: string): Observable<Display>;
    getDisplayStatus(display: Display): Observable<DisplayStatus>;
}

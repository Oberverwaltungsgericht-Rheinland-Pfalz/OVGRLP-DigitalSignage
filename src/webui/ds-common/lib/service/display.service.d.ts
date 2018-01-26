import { Observable } from "rxjs/Observable";
import { Display } from "../model/display";
import { DisplayStatus } from "../model/displayStatus";
export declare abstract class DisplayService {
    abstract getDisplays(): Observable<Display[]>;
    abstract getDisplay(name: string): Observable<Display>;
    abstract getDisplayStatus(display: Display): Observable<DisplayStatus>;
}

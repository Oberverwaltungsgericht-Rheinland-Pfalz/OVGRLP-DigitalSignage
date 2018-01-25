"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var of_1 = require("rxjs/observable/of");
require("rxjs/add/operator/catch");
require("rxjs/add/operator/map");
var TerminDummyService = /** @class */ (function () {
    function TerminDummyService() {
    }
    TerminDummyService.prototype.getTermine = function (displayName) {
        var termine = [
            {
                id: 1,
                az: "1 K 1111/18.XY",
                lfdnr: 1,
                kammer: 1,
                sitzungssaal: "Sitzungssaal I",
                sitzungssaalNr: 1,
                uhrzeitPlan: "10:00",
                uhrzeitAktuell: "10:00",
                status: "",
                oeffentlich: "ja",
                art: "mündliche Verhandlung",
                gegenstand: "Rundfunkbeitrags",
                bemerkung1: "",
                bemerkung2: "",
                parteienAktiv: ["Frank Ferdinand\r"],
                prozBevAktiv: ["Proz.-Bev.: Rechtsanwalt Volkmar Stern\r"],
                parteienPassiv: [
                    "Südwestrundfunk, vertreten durch den Intendanten\r"
                ],
                prozBevPassiv: [],
                parteienBeigeladen: [],
                prozBevBeigeladen: [],
                parteienZeugen: [],
                parteienSv: [],
                parteienAktivKurz: "Frank Ferdinand\r",
                parteienPassivKurz: "Südwestrundfunk, vertreten durch den Intendanten\r",
                stammdatenId: 1,
                gericht: "Testgericht Koblenz",
                datum: "09.01.2018",
                besetzung: ["Vorsitzender Richter am Testgericht Heinzmann"]
            },
            {
                id: 2,
                az: "1 K 2222/18.XY",
                lfdnr: 2,
                kammer: 1,
                sitzungssaal: "Sitzungssaal I",
                sitzungssaalNr: 1,
                uhrzeitPlan: "11:00",
                uhrzeitAktuell: "11:00",
                status: "",
                oeffentlich: "ja",
                art: "mündliche Verhandlung",
                gegenstand: "Rundfunkbeitrags",
                bemerkung1: "",
                bemerkung2: "",
                parteienAktiv: ["Simone Sterntaler\r"],
                prozBevAktiv: ["Proz.-Bev.: Rechtsanwalt Volkmar Stern\r"],
                parteienPassiv: [
                    "Südwestrundfunk, vertreten durch den Intendanten\r"
                ],
                prozBevPassiv: [],
                parteienBeigeladen: [],
                prozBevBeigeladen: [],
                parteienZeugen: [],
                parteienSv: [],
                parteienAktivKurz: "Simone Sterntaler\r",
                parteienPassivKurz: "Südwestrundfunk, vertreten durch den Intendanten\r",
                stammdatenId: 1,
                gericht: "Testgericht Koblenz",
                datum: "09.01.2018",
                besetzung: ["Vorsitzender Richter am Testgericht Heinzmann"]
            },
            {
                id: 3,
                az: "1 K 3333/18.XY",
                lfdnr: 3,
                kammer: 1,
                sitzungssaal: "Sitzungssaal I",
                sitzungssaalNr: 1,
                uhrzeitPlan: "13:00",
                uhrzeitAktuell: "13:00",
                status: "",
                oeffentlich: "ja",
                art: "mündliche Verhandlung",
                gegenstand: "Rundfunkbeitrags",
                bemerkung1: "",
                bemerkung2: "",
                parteienAktiv: ["Peter Peterson\r"],
                prozBevAktiv: ["Proz.-Bev.: Rechtsanwalt Volkmar Stern\r"],
                parteienPassiv: [
                    "Südwestrundfunk, vertreten durch den Intendanten\r"
                ],
                prozBevPassiv: [],
                parteienBeigeladen: [],
                prozBevBeigeladen: [],
                parteienZeugen: [],
                parteienSv: [],
                parteienAktivKurz: "Peter Peterson\r",
                parteienPassivKurz: "Südwestrundfunk, vertreten durch den Intendanten\r",
                stammdatenId: 1,
                gericht: "Testgericht Koblenz",
                datum: "09.01.2018",
                besetzung: ["Vorsitzender Richter am Testgericht Heinzmann"]
            },
        ];
        console.log("TerminDummyService.getTermine()");
        return of_1.of(termine);
    };
    TerminDummyService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [])
    ], TerminDummyService);
    return TerminDummyService;
}());
exports.TerminDummyService = TerminDummyService;
//# sourceMappingURL=termin-dummy.service.js.map
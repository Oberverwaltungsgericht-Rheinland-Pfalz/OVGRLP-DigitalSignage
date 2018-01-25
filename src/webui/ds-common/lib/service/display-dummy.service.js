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
var model_1 = require("../model");
var DisplayDummyService = /** @class */ (function () {
    function DisplayDummyService() {
    }
    DisplayDummyService.prototype.getDisplays = function () {
        var displays = [
            {
                id: 1,
                name: "saal1",
                title: "Sitzungssaal I",
                template: "NjzSaalTemplateComponent",
                styles: "",
                filter: "SitzungssaalNr = 1",
                group: "Testanzeigen",
                controlUrl: "",
                netAddress: "",
                wolIpAddress: "",
                wolMacAddress: "",
                wolUdpPort: 9,
                description: null,
                notes: null,
                dummy: false
            },
            {
                id: 2,
                name: "saal2",
                title: "Sitzungssaal II",
                template: "NjzSaalTemplateComponent",
                styles: "",
                filter: "SitzungssaalNr = 2",
                group: "Testanzeigen",
                controlUrl: "",
                netAddress: "",
                wolIpAddress: "",
                wolMacAddress: "",
                wolUdpPort: 9,
                description: null,
                notes: null,
                dummy: false
            },
            {
                id: 3,
                name: "saal3",
                title: "Sitzungssaal III",
                template: "NjzKhFoyerTemplateComponentUnten",
                styles: "",
                filter: "SitzungssaalNr = 5",
                group: "Testanzeigen",
                controlUrl: "",
                netAddress: "",
                wolIpAddress: "",
                wolMacAddress: "",
                wolUdpPort: 9,
                description: null,
                notes: null,
                dummy: false
            }
        ];
        console.log("DisplayDummyService.getDisplays()");
        return of_1.of(displays);
    };
    DisplayDummyService.prototype.getDisplay = function (name) {
        var display = new model_1.Display();
        console.log("DisplayDummyService.getDisplay(name: '" + name + "')");
        this.getDisplays().subscribe(function (displays) {
            display = displays.find(function (display) { return display.name === name; });
        });
        return of_1.of(display);
    };
    DisplayDummyService.prototype.getDisplayStatus = function (display) {
        var displayStatus = model_1.DisplayStatus.Active;
        console.log("DisplayDummyService.getDisplayStatus(display: '" + display.name + "')");
        return of_1.of(displayStatus);
    };
    DisplayDummyService = __decorate([
        core_1.Injectable(),
        __metadata("design:paramtypes", [])
    ], DisplayDummyService);
    return DisplayDummyService;
}());
exports.DisplayDummyService = DisplayDummyService;
//# sourceMappingURL=display-dummy.service.js.map
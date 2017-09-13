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
var router_1 = require("@angular/router");
var Rx_1 = require("rxjs/Rx");
require("rxjs/add/operator/switchMap");
var ds_core_1 = require("ds-core");
var DisplayComponent = /** @class */ (function () {
    function DisplayComponent(displayService, terminService, route) {
        this.displayService = displayService;
        this.terminService = terminService;
        this.route = route;
    }
    DisplayComponent.prototype.loadDisplay = function () {
        var _this = this;
        this.route.params
            .switchMap(function (params) { return _this.displayService.getDisplay(params['name']); })
            .subscribe(function (display) {
            _this.display = display;
            _this.loadTermine(display.name);
        });
    };
    DisplayComponent.prototype.loadTermine = function (name) {
        var _this = this;
        this.terminService.getTermine(name)
            .subscribe(function (termine) {
            _this.alleTermine = termine.filter(function (termin) { return termin.uhrzeitAktuell != 'omV'; });
            _this.aktiverTermin = _this.alleTermine.find(function (termin) { return termin.status === 'Läuft'; });
            _this.offeneTermine = _this.alleTermine.filter(function (termin) { return !(termin.status === 'Abgeschlossen' || termin.status === 'Aufgehoben'); });
            _this.naechsterTermin = _this.aktiverTermin ? null : _this.offeneTermine[0];
        });
    };
    DisplayComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.updateTimer = Rx_1.Observable.timer(2000, 5000);
        this.updateSub = this.updateTimer.subscribe(function (t) {
            _this.datum = new Date();
            _this.loadDisplay();
        });
    };
    DisplayComponent.prototype.ngOnDestroy = function () {
        this.updateSub.unsubscribe();
    };
    DisplayComponent = __decorate([
        core_1.Component({
            selector: 'app-display',
            templateUrl: './display.component.html',
            styleUrls: ['./display.component.css'],
            providers: [ds_core_1.DisplayService, ds_core_1.TerminService]
        }),
        __metadata("design:paramtypes", [ds_core_1.DisplayService,
            ds_core_1.TerminService,
            router_1.ActivatedRoute])
    ], DisplayComponent);
    return DisplayComponent;
}());
exports.DisplayComponent = DisplayComponent;
//# sourceMappingURL=display.component.js.map
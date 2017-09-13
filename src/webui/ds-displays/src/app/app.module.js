"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var platform_browser_1 = require("@angular/platform-browser");
var core_1 = require("@angular/core");
var forms_1 = require("@angular/forms");
var http_1 = require("@angular/http");
var router_1 = require("@angular/router");
var flex_layout_1 = require("@angular/flex-layout");
var environment_1 = require("../environments/environment");
var ds_core_1 = require("ds-core");
var app_component_1 = require("./app.component");
var display_component_1 = require("./display/display.component");
var home_component_1 = require("./home/home.component");
var capitalize_pipe_1 = require("./capitalize.pipe");
var termin_component_1 = require("./termin/termin.component");
var appRoutes = [
    { path: ':name', component: display_component_1.DisplayComponent },
    { path: '', component: home_component_1.HomeComponent }
];
function ConfigLoader(configService) {
    return function () { return configService.load(environment_1.environment.configFile); };
}
exports.ConfigLoader = ConfigLoader;
var AppModule = /** @class */ (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        core_1.NgModule({
            declarations: [
                app_component_1.AppComponent,
                display_component_1.DisplayComponent,
                home_component_1.HomeComponent,
                capitalize_pipe_1.CapitalizePipe,
                termin_component_1.TerminComponent
            ],
            imports: [
                router_1.RouterModule.forRoot(appRoutes, { useHash: true }),
                platform_browser_1.BrowserModule,
                forms_1.FormsModule,
                http_1.HttpModule,
                flex_layout_1.FlexLayoutModule
            ],
            providers: [
                ds_core_1.ConfigService,
                {
                    provide: core_1.APP_INITIALIZER,
                    useFactory: ConfigLoader,
                    deps: [ds_core_1.ConfigService],
                    multi: true
                }
            ],
            bootstrap: [app_component_1.AppComponent]
        })
    ], AppModule);
    return AppModule;
}());
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map
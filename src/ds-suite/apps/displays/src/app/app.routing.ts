import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { DisplayComponent } from './display/display.component';

const routes: Routes = [
  { path: "", component: HomeComponent },
  {
    path: ":name", component: DisplayComponent,
    children: [
      {
        path: "njzko",
        loadChildren: './templates/njz-ko/njz-ko.module#NjzKoModule'
      },
      {
        path: "njzkh",
        loadChildren: './templates/njz-kh/njz-kh.module#NjzKhModule'
      },
      {
        path: "demo",
        loadChildren: './templates/demo/demo.module#DemoModule'
      },
      {
        path: "jztr",
        loadChildren: './templates/jz-tr/jz-tr.module#JzTrModule'
      },
      {
        path: "pfolg",
        loadChildren: './templates/jz-pfolg/jz-pfolg.module#PfolgModule'
      },
      {
        path: "jzzw",
        loadChildren: './templates/jz-zw/jz-zw.module#JzZwModule'
      },
      {
        path: "jzkl",
        loadChildren: './templates/jz-kl/jz-kl.module#JzKlModule'
      }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true })],
  exports: [RouterModule],
})
export class AppRoutingModule { }

export const routingComponents = [HomeComponent, DisplayComponent];
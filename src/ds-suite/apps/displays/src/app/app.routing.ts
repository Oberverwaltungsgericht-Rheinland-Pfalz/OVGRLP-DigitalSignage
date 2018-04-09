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
      }
    ]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }

export const routingComponents = [HomeComponent, DisplayComponent];
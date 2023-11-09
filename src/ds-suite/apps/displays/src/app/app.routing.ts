// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
import { NgModule } from '@angular/core'
import { Routes, RouterModule } from '@angular/router'

import { HomeComponent } from './home/home.component'
import { DisplayComponent } from './display/display.component'

const routes: Routes = [
  { path: '', component: HomeComponent },
  {
    path: ':name',
    component: DisplayComponent,
    children: [
      {
        path: 'njzko',
        loadChildren: async () => await import('./templates/njz-ko/njz-ko.module').then(m => m.NjzKoModule)
      },
      {
        path: 'njzkh',
        loadChildren: async () => await import('./templates/njz-kh/njz-kh.module').then(m => m.NjzKhModule)
      },
      {
        path: 'demo',
        loadChildren: async () => await import('./templates/demo/demo.module').then(m => m.DemoModule)
      },
      {
        path: 'jztr',
        loadChildren: async () => await import('./templates/jz-tr/jz-tr.module').then(m => m.JzTrModule)
      },
      {
        path: 'pfolg',
        loadChildren: async () => await import('./templates/jz-pfolg/jz-pfolg.module').then(m => m.PfolgModule)
      },
      {
        path: 'jzzw',
        loadChildren: async () => await import('./templates/jz-zw/jz-zw.module').then(m => m.JzZwModule)
      },
      {
        path: 'jzkl',
        loadChildren: async () => await import('./templates/jz-kl/jz-kl.module').then(m => m.JzKlModule)
      }
    ]
  }
]

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true, relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }

export const routingComponents = [HomeComponent, DisplayComponent]

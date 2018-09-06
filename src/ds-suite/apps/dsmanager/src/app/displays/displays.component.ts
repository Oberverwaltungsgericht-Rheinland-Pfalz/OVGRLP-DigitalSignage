import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';

import { Termin } from '@ds-suite/model';
import { TerminService } from '@ds-suite/core';
import { DisplayDto, DisplayStatus } from '@ds-suite/model';
import { DisplayService } from '@ds-suite/core';
import { DisplayDialogComponent } from '@ds-suite/ui';

@Component({
  selector: 'displays',
  templateUrl: './displays.component.html',
  styleUrls: ['./displays.component.css']
})
export class DisplaysComponent implements OnInit {
  displayDto: DisplayDto[];
  termine: Termin[];
  displayGroups: string[];
  public isLoading: boolean = false;

  @ViewChild(DisplayDialogComponent) modal: DisplayDialogComponent;

  constructor(private displayService: DisplayService,
    private terminService: TerminService,
    private router: Router) { 
  }

  getDisplays() {
    this.displayService.getDisplaysDto()
      .subscribe(
        displays => {
          this.displayDto = displays.sort((d1, d2) => d1.title > d2.title ? 1 : -1)
          this.DetermineDisplayGroups(this.displayDto);
        },
        err => {
          console.error("Displays konnten nicht geladen werden: ",err);
        },
        ()=> this.isLoading = false);
  }

  GetDisplaysFromGroup(group: string): DisplayDto[] {
    return this.displayDto.filter(t => t.group == group);
  }

  DetermineDisplayGroups(displays: DisplayDto[]): void {
    this.displayGroups = Array.from(new Set(displays.map(t => t.group)));
  }

  loadTermine() {
    this.terminService.getAllTermine()
      .subscribe(termine => {
        this.termine = termine;
      },
      err => {
        console.error("Termine konnten nicht geladen werden: ",err);
      });
  }

  ngOnInit() {
    this.isLoading = true;
    this.loadTermine();
    this.getDisplays();
  }

  AnzeigeTermineClick(display: DisplayDto) {
    this.router.navigate(['/termine', display.title]);
  }

  updateGroupClick(group: string){
    var disp: DisplayDto[] = this.GetDisplaysFromGroup(group)
    disp.forEach(display => {
      this. updateDisplayClick(display);
    });
  }

  updateDisplayClick(display: DisplayDto){
    this.displayService.getDisplayDto(display.name)
      .subscribe(
        disp => {
          var index=this.displayDto.findIndex(t => t.name == display.name)
          this.displayDto[index]=disp
        },
        err => {
          console.error("Display " + display.name + " konnte nicht aktualisiert werden: ",err);
        });
  }

  startGroupClick(group: string){
    var disp: DisplayDto[] = this.GetDisplaysFromGroup(group)
    disp.forEach(display => {
      this. startDisplayClick(display);
    });
  }

  startDisplayClick(display: DisplayDto) {
    this.displayService.startDisplay(display)
      .subscribe(response => { },
        err => {
          console.error("Display " + display.name + " konnte nicht gestartet werden: ",err);
        });
  }

  restartGroupClick(group: string){
    var disp: DisplayDto[] = this.GetDisplaysFromGroup(group)
    disp.forEach(display => {
      this. restartDisplayClick(display);
    });
  }

  restartDisplayClick(display: DisplayDto) {
    this.displayService.restartDisplay(display)
      .subscribe(response => { },
        err => {
          console.error("Display " + display.name + " konnte nicht neu gestartet werden: ",err);
        });
  }

  stopGroupClick(group: string){
    var disp: DisplayDto[] = this.GetDisplaysFromGroup(group)
    disp.forEach(display => {
      this. stopDisplayClick(display);
    });
  }

  stopDisplayClick(display: DisplayDto) {
    this.displayService.stopDisplay(display)
      .subscribe(response => { },
        err => {
          console.error("Display " + display.name + " konnte nicht heruntergefahren werden: ",err);
        });
  }

  TermineExist(display: DisplayDto) {
    return this.termine!=undefined && this.termine.find(t=> t.sitzungssaal==display.title || t.gericht==display.title )
  }

}

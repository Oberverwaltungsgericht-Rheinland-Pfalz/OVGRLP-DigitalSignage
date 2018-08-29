import { Component, OnInit } from '@angular/core';

import { DisplayDto, DisplayStatus } from '@ds-suite/model';
import { DisplayService } from '@ds-suite/core';

@Component({
  selector: 'displays',
  templateUrl: './displays.component.html',
  styleUrls: ['./displays.component.css']
})
export class DisplaysComponent implements OnInit {
  displayDto: DisplayDto[];
  displayGroups: string[];

  constructor(private displayService: DisplayService) { }

  getDisplays() {
    this.displayService.getDisplaysDto()
      .subscribe(
        displays => {
          this.displayDto = displays.sort((d1, d2) => d1.title > d2.title ? 1 : -1)
          this.DetermineDisplayGroups(this.displayDto);
        }
      );
  }

  GetDisplaysFromGroup(group: string): DisplayDto[] {
    return this.displayDto.filter(t => t.group == group);
  }

  DetermineDisplayGroups(displays: DisplayDto[]): void {
    this.displayGroups = Array.from(new Set(displays.map(t => t.group)));
  }

  ngOnInit() {
    this.getDisplays();
  }
}

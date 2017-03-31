import { Injectable } from '@angular/core';
import { Display } from './display'

const DISPLAYS: Display[] = [
  {
    "id": 2,
    "name": "5540DS-008",
    "title": "Sitzungssaal I",
    "template": "aushang-standard",
    "styles": "aushang-1280x1024",
    "filter": "Sitzungssaal = 'Sitzungssaal I'",
    "group": "VGTR",
    "controlUrl": "http://5540DS-008:9000",
    "netAddress": "5540DS-008",
    "wolIpAddress": "10.10.138.255",
    "wolMacAddress": "00-04-5F-83-80-5B",
    "wolUdpPort": 9,
    "description": null,
    "notes": null,
    "dummy": false
  },
  {
    "id": 3,
    "name": "5540DS-004",
    "title": "Sitzungssaal II",
    "template": "aushang-standard",
    "styles": "aushang-1280x1024",
    "filter": "Sitzungssaal = 'Sitzungssaal II'",
    "group": "VGTR",
    "controlUrl": "http://5540DS-004:9000",
    "netAddress": "5540DS-004",
    "wolIpAddress": "10.10.138.255",
    "wolMacAddress": "00-04-5F-89-7E-A4",
    "wolUdpPort": 9,
    "description": null,
    "notes": null,
    "dummy": false
  },
  {
    "id": 7,
    "name": "5540DS-002",
    "title": "Sitzungssaal III",
    "template": "aushang-standard",
    "styles": "aushang-1280x1024",
    "filter": "Sitzungssaal = 'Sitzungssaal III'",
    "group": "VGTR",
    "controlUrl": "http://5540DS-002:9000",
    "netAddress": "5540DS-002",
    "wolIpAddress": "10.10.138.255",
    "wolMacAddress": "00-30-18-00-B4-4C",
    "wolUdpPort": 9,
    "description": null,
    "notes": null,
    "dummy": false
  }
];


@Injectable()
export class DisplayService {

  constructor() { }

  getDisplays(): Promise<Display[]> {
    return Promise.resolve(DISPLAYS);
  }

  getDisplay(name: string): Promise<Display> {
    return this.getDisplays()
      .then(displays => displays.find(display => display.name === name));
  }
}
import { MockHandler } from 'vite-plugin-mock-simple'
// @ts-expect-error
import Termin from './src/models/Termin'

const terminMock: Termin = {
  id: 199637,
  az: 'S 1 VG 11/24',
  lfdnr: 1,
  kammer: 14,
  sitzungssaal: 'Sitzungssaal A008',
  sitzungssaalNr: null,
  uhrzeitPlan: '09:00',
  uhrzeitAktuell: '09:00',
  status: '',
  oeffentlich: 'ja',
  art: 'mündliche Verhandlung',
  gegenstand: 'Soziales Entschädigungsrecht',
  bemerkung1: '',
  bemerkung2: '',
  parteienAktiv: [
    'Michael Müller'
  ],
  prozBevAktiv: [
    'Proz.-Bev.: Sozialverband VdK e.V. Rheinland-Pfalz Rechtsschutzaußenstelle Koblenz'
  ],
  parteienPassiv: [
    'das Land Rheinland-Pfalz vertreten durch den Präsidenten des Landesamtes'
  ],
  prozBevPassiv: [],
  parteienBeigeladen: [],
  prozBevBeigeladen: [],
  parteienZeugen: [],
  parteienSv: [],
  parteienAktivKurz: 'MM',
  parteienPassivKurz: 'das Land Rheinland-Pfalz vertreten durch den Präsidenten des Landesamtes',
  stammdatenId: 24006,
  gericht: 'Sozialgericht Koblenz',
  datum: new Date().toLocaleString().substring(0, 9),
  besetzung: [
    'Präsident des Sozialgerichts Dr. Richter',
    'ehrenamtlicher Richter Eins',
    'ehrenamtlicher Richter Zwei'
  ],
  parteienBeteiligt: [],
  objekte: []
}

const routes: MockHandler[] = [
  {
    pattern: '/settings/displays/DisplaysEx',
    jsonBody: [
      {
          "status": 0,
          "screenshotUrl": "http://127.0.0.1:8000/api/screenshot?123",
          "id": 1,
          "name": "Anzeige 1",
          "title": "Sitzungssaal E2",
          "template": "njz/saal",
          "styles": null,
          "filter": "",
          "group": "NJZ Säle",
          "controlUrl": "http://127.0.0.1:8000",
          "netAddress": "127.0.0.1",
          "wolIpAddress": "255.255.255.255",
          "wolMacAddress": "00-90-11-02-AB-23",
          "wolUdpPort": 1,
          "description": "",
          "notesAssignments": [],
          "dummy": false
      },{
        "status": 0,
        "screenshotUrl": "http://127.0.0.1:8001/api/screenshot?123",
        "id": 2,
        "name": "Anzeige 5",
        "title": "Sitzungssaal E3",
        "template": "njz/saal",
        "styles": null,
        "filter": "",
        "group": "NJZ Tafeln",
        "controlUrl": "http://http://127.0.0.1:9001",
        "netAddress": "127.0.0.1",
        "wolIpAddress": "255.255.255.255",
        "wolMacAddress": "00-00-11-02-13-92",
        "wolUdpPort": 2,
        "description": "",
        "notesAssignments": [],
        "dummy": false
    },]
  },
  {
    pattern: '/daten/verfahren/{termin_id}',
    method: 'PUT',
    handle: (req, res) => {
      res.setHeader('Content-Type', 'application/json')
      // @ts-expect-error
      req.on('data', (bodyString: string) => {
        const body: Termin = JSON.parse(bodyString)
        terminMock.status = body.status
        terminMock.oeffentlich = body.oeffentlich
        res.end('')
      })
    }
  },
  {
    pattern: '/settings/displays/*/ScreenshotUrl',
    handle: (req, res) => {
      res.end('http://localhost:5173/api/screenshot.jpg')
    }
  },
  {
    pattern: '/api/screenshot',
    handle: (req, res) => {
      res.setHeader('Content-Type', 'application/json')
      res.end('data:image/gif;base64,R0lGODlhAQABAIAAAMLCwgAAACH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==')
    }
  },
  {
    pattern: '/settings/displays/*/status',
    method: 'GET',
    handle: (req, res) => {
      res.setHeader('Content-Type', 'application/json')
      res.end(JSON.stringify(Math.floor(Math.random() * 2)))
    }
  },
  {
    pattern: '/settings/displays/*/termine',
    handle: (req, res) => {
      res.setHeader('Content-Type', 'application/json')
      res.end(JSON.stringify([terminMock]))
    }
  },
  {
    pattern: '/settings/displays',
    jsonBody: [
      {
        id: 1,
        name: 'Foyer-Gesamt',
        title: 'Justizzentrum Koblenz',
        template: 'njzko/foyer',
        styles: null,
        filter: null,
        group: 'NJZ Foyer',
        controlUrl: null,
        netAddress: '127.0.0.1',
        wolIpAddress: '127.0.0.1',
        wolMacAddress: '00-00-00-00-00-00',
        wolUdpPort: 9,
        description: '',
        notesAssignments: null,
        dummy: false
      }]
  }
]

export default routes

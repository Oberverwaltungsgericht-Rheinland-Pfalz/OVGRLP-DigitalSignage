import { MockHandler } from "vite-plugin-mock-simple"
import Termin from './src/models/Termin'

const terminMock: Termin = {
  "id": 199637,
  "az": "S 1 VG 11/24",
  "lfdnr": 1,
  "kammer": 14,
  "sitzungssaal": "Sitzungssaal A008",
  "sitzungssaalNr": null,
  "uhrzeitPlan": "09:00",
  "uhrzeitAktuell": "09:00",
  "status": "",
  "oeffentlich": "ja",
  "art": "mündliche Verhandlung",
  "gegenstand": "Soziales Entschädigungsrecht",
  "bemerkung1": "",
  "bemerkung2": "",
  "parteienAktiv": [
      "Michael Müller"
  ],
  "prozBevAktiv": [
      "Proz.-Bev.: Sozialverband VdK e.V. Rheinland-Pfalz Rechtsschutzaußenstelle Koblenz"
  ],
  "parteienPassiv": [
      "das Land Rheinland-Pfalz vertreten durch den Präsidenten des Landesamtes"
  ],
  "prozBevPassiv": [],
  "parteienBeigeladen": [],
  "prozBevBeigeladen": [],
  "parteienZeugen": [],
  "parteienSv": [],
  "parteienAktivKurz": "MM",
  "parteienPassivKurz": "das Land Rheinland-Pfalz vertreten durch den Präsidenten des Landesamtes",
  "stammdatenId": 24006,
  "gericht": "Sozialgericht Koblenz",
  "datum": new Date().toLocaleString().substring(0, 9),
  "besetzung": [
      "Präsident des Sozialgerichts Dr. Richter",
      "ehrenamtlicher Richter Eins",
      "ehrenamtlicher Richter Zwei"
  ],
  "parteienBeteiligt": [],
  "objekte": []
}

const routes: MockHandler[] = [
  {
    pattern: '/daten/verfahren/{termin_id}',
    method: 'PUT',
    handle: (req, res) => {
      res.setHeader('Content-Type', 'application/json')
      // @ts-expect-error
      req.on('data', (bodyString: string) => { 
        let body: Termin = JSON.parse(bodyString)
        terminMock.status =  body.status
        terminMock.oeffentlich =  body.oeffentlich
        res.end('')
      })
    }
  },
  {
    pattern: '/settings/displays/*/ScreenshotUrl',
    handle: (req, res) => {
      res.end('http://localhost:5173/api/screenshot')
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
    pattern: `/settings/displays/*/status`,
    method: 'GET',
    handle: (req, res) => {
        res.setHeader('Content-Type', 'application/json')
        res.end(JSON.stringify(1))
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
        "id": 1,
        "name": "Foyer-Gesamt",
        "title": "Justizzentrum Koblenz",
        "template": "njzko/foyer",
        "styles": null,
        "filter": null,
        "group": "NJZ Foyer",
        "controlUrl": null,
        "netAddress": "10.10.10.255",
        "wolIpAddress": "10.10.10.255",
        "wolMacAddress": "00-00-00-00-00-00",
        "wolUdpPort": 9,
        "description": "",
        "notesAssignments": null,
        "dummy": false
    }]
}
]



export default routes
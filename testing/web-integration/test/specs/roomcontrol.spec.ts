import { actorCalled, TakeNotes } from '@serenity-js/core'
import { IsAppRunning } from './screenplay/OpenTheApp'
import { RoomShowsValid } from './screenplay/ViewARoom'
import { BrowseTheWebWithWebdriverIO } from '@serenity-js/webdriverio'
import { browser } from '@wdio/globals'

// do here things that needs to be executed before the tests start
if (process.env.NODE_ENV && process.env.NODE_ENV.includes('testlab')) {
  var settings = require('./../../settings_labor.json')
  //    var instance = axios.create({baseURL: settings.url, proxy: false})
  delete process.env.https_proxy
  delete process.env.http_proxy
}

describe('Roomcontrol', () => {
  it(`${process.env.NODE_ENV}: test ob das Justizportal richtig funktioniert`, async () =>
    await actorCalled('Nutzer')
      .whoCan(
        BrowseTheWebWithWebdriverIO.using(browser),
        TakeNotes.usingAnEmptyNotepad()
      )
      .attemptsTo(
        IsAppRunning(settings.url),
        RoomShowsValid()
      )
  )
})

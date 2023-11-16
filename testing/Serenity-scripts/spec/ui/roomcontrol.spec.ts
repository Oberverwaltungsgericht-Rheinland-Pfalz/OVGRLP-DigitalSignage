import 'jasmine'
import { actorCalled, TakeNotes } from '@serenity-js/core'
import { BrowseTheWeb } from '@serenity-js/protractor'
import { protractor } from 'protractor'
import { IsAppRunning } from './screenplay/ui/OpenTheApp'
import { RoomShowsValid } from './screenplay/ui/ViewARoom'

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
        BrowseTheWeb.using(protractor.browser),
        TakeNotes.usingAnEmptyNotepad()
      )
      .attemptsTo(
        IsAppRunning(settings.url),
        RoomShowsValid()
      ).then(() => {
        protractor.browser.driver.manage().logs().get('browser').then(s => {
          console.log('Browser-Log:')
          console.dir(s)
        })
      })
  )
})

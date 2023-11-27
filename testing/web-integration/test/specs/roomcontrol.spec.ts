import { actorCalled, TakeNotes } from '@serenity-js/core'
import { IsAppRunning } from './screenplay/OpenTheApp'
import { RoomShowsValid } from './screenplay/ViewARoom'
import { BrowseTheWebWithWebdriverIO } from '@serenity-js/webdriverio'
import { browser } from '@wdio/globals'
import settings from './../../settings_labor.json'

describe('Roomcontrol', () => {
  it('Test ob die Roomcontrol der Saalanzeige richtig funktioniert', async () =>
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

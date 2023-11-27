import { endsWith, Ensure, equals, isGreaterThan, not, startsWith, isPresent, includes } from '@serenity-js/assertions'
import { Roomcontrol } from './RoomcontrolApp'
import { Text, isVisible, Click, PageElement, By, isClickable } from '@serenity-js/web'
import { Check, Task, Wait } from '@serenity-js/core'

export const RoomShowsValid: () => Task = () =>
  Task.where('#actor checks if a room shows properly',
    Click.on(Roomcontrol.DisplayItems.first()),
    Wait.until(PageElement.located(By.id('ScreenshotImage')), isPresent()),
    Ensure.that(Text.of(Roomcontrol.displayStatusCard), not(equals(''))),
    Ensure.that(Text.of(Roomcontrol.displayStatusCard), startsWith('Anzeigegerät ')),
    Ensure.that(Text.of(Roomcontrol.displayStatusCard), not(endsWith('Anzeigegerät '))),

    Check.whether(Roomcontrol.emptyList, isVisible())
      .andIfSo(Ensure.that(Roomcontrol.clockTimeTableElement, not(isPresent())))
      .otherwise(
        Ensure.that(Roomcontrol.AppointmentListItems.count(), isGreaterThan(1)),
        Ensure.that(Roomcontrol.AppointmentListItems.first(), isVisible()),
        Ensure.that(Roomcontrol.clockTimeTableElement, isPresent()),
        Ensure.that(Text.of(Roomcontrol.clockTimeTableElement), equals('Uhrzeit'))
      ),
    Ensure.that(Roomcontrol.turnOnCard, isClickable()),
    Ensure.that(Text.of(Roomcontrol.turnOnCard), includes('ANSCHALTEN'))
  )

import { endsWith, Ensure, equals, isGreaterThan, not, startsWith } from '@serenity-js/assertions'
import { Task } from '@serenity-js/core'
import { isPresent, Wait, Text, isVisible, Click } from '@serenity-js/protractor'
import { Roomcontrol } from './RoomcontrolApp'

export const RoomShowsValid = () =>
  Task.where('#actor checks if a room shows properly',
    Click.on(Roomcontrol.DisplayItems.first()),
    Wait.until(Roomcontrol.clockTimeTableElement, isPresent()),
    Ensure.that(Roomcontrol.clockTimeTableElement, isPresent()),
    Ensure.that(Text.of(Roomcontrol.clockTimeTableElement), equals('Uhrzeit')),

    Ensure.that(Roomcontrol.AppointmentListItems.count(), isGreaterThan(1)),
    Ensure.that(Roomcontrol.AppointmentListItems.first(), isVisible()),

    Ensure.that(Text.of(Roomcontrol.displayStatusCard), startsWith('Anzeigegerät ')),
    Ensure.that(Text.of(Roomcontrol.displayStatusCard), not(endsWith('Anzeigegerät ')))
  )

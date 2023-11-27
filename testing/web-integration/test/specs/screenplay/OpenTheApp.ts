import { containItemsWhereEachItem, Ensure, isPresent, equals, isGreaterThan, not, property } from '@serenity-js/assertions'
import { Task, Wait } from '@serenity-js/core'

import { GetRequest, LastResponse, Send } from '@serenity-js/rest'
import { Roomcontrol } from './RoomcontrolApp'
import { Attribute, Navigate, Text, isClickable, isVisible } from '@serenity-js/web'

export const IsAppRunning: (baseUrl: string) => Task = (baseUrl: string) =>
  Task.where('#actor checks if the app is running',
    Navigate.to(baseUrl + 'roomcontrol/#'),
    //        Wait.for(Duration.ofMilliseconds(100)),
    Wait.until(Roomcontrol.FirstDisplayLi, isPresent()),
    Ensure.that(Roomcontrol.versionElement, isPresent()),
    Ensure.that(Roomcontrol.titleNavField, isPresent()),
    Ensure.that(Roomcontrol.titleNavField, isVisible()),
    Ensure.that(Roomcontrol.titleNavField, isClickable()),
    Ensure.that(Text.of(Roomcontrol.titleNavField), equals('Saalsteuerung')),
    Ensure.that(Roomcontrol.DisplayItems.first(), isPresent()),
    Ensure.that(Roomcontrol.DisplayItems.first(), isVisible()),
    Ensure.that(Attribute.called('href').of(Roomcontrol.DisplayItems.first()), not(equals(''))),
    Ensure.that(Roomcontrol.DisplayItems.count(), isGreaterThan(1)),
    Ensure.that(Text.ofAll(Roomcontrol.DisplayItems), containItemsWhereEachItem(not(equals('')))),

    // with GetResponse
    Send.a(GetRequest.to(baseUrl + 'roomcontrol/assets/config.json')),
    //  Ensure.that(LastResponse.status(), equals(200)),
    Ensure.that(LastResponse.body<{ webApiUrl: string, useWindowsAuthentication: boolean }>(), property('webApiUrl', not(equals('')))),
    Ensure.that(LastResponse.body<{ webApiUrl: string, useWindowsAuthentication: string }>(), property('useWindowsAuthentication', not(equals('')))),
    //        Ensure.that(LastResponse.body<{assemblyVersion: string, isDbConnected: boolean}>(), property('isDbConnected', isTrue())),

    Send.a(GetRequest.to(baseUrl + 'webapi/settings/displays')),
    Ensure.that(LastResponse.status(), equals(404))
    // Ensure.that(LastResponse.body<string>(), includes('[{id')),
  )

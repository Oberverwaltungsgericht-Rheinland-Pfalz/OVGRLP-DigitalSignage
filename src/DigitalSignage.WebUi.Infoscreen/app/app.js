(function () {
    'use strict';

    var app = angular.module('infoscreen.app', [
        'ngMaterial'
    ]);

    app.controller('InfoscreenMainController', function ($scope) {
        $scope.title = 'Digital Signage - Infoscreen';

        $scope.terms = [
            {
                uhrzeit: '09:30 Uhr',
                aktenzeichen: 'S 16 R 666/13',
                gericht: 'Sozialgericht Koblenz',
                kurzrubrum: 'Michael Juwig ./. Deutsche Rentenversicherung Rheinland-Pfalz Zweigstelle Andernach',
                saal: 'Sitzungssaal A007'
            },
            {
                uhrzeit: '09:50 Uhr',
                aktenzeichen: 'S 13 KR 296/13',
                gericht: 'Sozialgericht Koblenz',
                kurzrubrum: 'Wilfried Baltes ./. Betriebskrankenkasse ZF & Partner',
                saal: 'Sitzungssaal A008'
            },
            {
                uhrzeit: '10:20 Uhr',
                aktenzeichen: 'S 13 AS 1008/13',
                gericht: 'Sozialgericht Koblenz',
                kurzrubrum: 'Heinz Emons ./. Jobcenter Westerwald',
                saal: 'Sitzungssaal A008'
            },
            {
                uhrzeit: '10:30 Uhr',
                aktenzeichen: 'S 16 R 557/13',
                gericht: 'Sozialgericht Koblenz',
                kurzrubrum: 'Andreas Will ./. Deutsche Rentenversicherung Bund',
                saal: 'Sitzungssaal A007'
            },
            {
                uhrzeit: '10:40 Uhr',
                aktenzeichen: 'S 13 AS 587/14',
                gericht: 'Sozialgericht Koblenz',
                kurzrubrum: 'Fedor Dondaj ./. Jobcenter Westerwald',
                saal: 'Sitzungssaal A008'
            },
            {
                uhrzeit: '11:00 Uhr',
                aktenzeichen: '9 Ca 6/15',
                gericht: 'Arbeitsgericht Koblenz',
                kurzrubrum: 'Chantal Gerlach ./. Taijo Intensivpflege UG',
                saal: 'Sitzungssaal A026'
            },
            {
                uhrzeit: '11:00 Uhr',
                aktenzeichen: '8 Ca 1462/14',
                gericht: 'Arbeitsgericht Koblenz',
                kurzrubrum: 'Anna Malgorzata Marusa ./. Abdelmagid Messaoudi',
                saal: 'Sitzungssaal A022'
            },
            {
                uhrzeit: '11:00 Uhr',
                aktenzeichen: 'S 16 R 528/13',
                gericht: 'Sozialgericht Koblenz',
                kurzrubrum: 'Andrej Eliseev ./. Deutsche Rentenversicherung Rheinland-Pfalz',
                saal: 'Sitzungssaal A007'
            },
            {
                uhrzeit: '11:00 Uhr',
                aktenzeichen: 'S 13 AS 737/14',
                gericht: 'Sozialgericht Koblenz',
                kurzrubrum: 'Gabriele Ascheid ./. Jobcenter Westerwald',
                saal: 'Sitzungssaal A008'
            },
            {
                uhrzeit: '11:30 Uhr',
                aktenzeichen: '9 Ca 3354/14',
                gericht: 'Arbeitsgericht Koblenz',
                kurzrubrum: 'Dirk Lichtenberg ./. SGS-Security GmbH',
                saal: 'Sitzungssaal A026'
            },
            {
                uhrzeit: '12:00 Uhr',
                aktenzeichen: '9 Ca 2755/14',
                gericht: 'Arbeitsgericht Koblenz',
                kurzrubrum: 'Marion Leygraf ./. Leo-Jonas IPB guG (haftungsbeschränkt)',
                saal: 'Sitzungssaal A026'
            },
            {
                uhrzeit: '13:00 Uhr',
                aktenzeichen: '8 Ca 2924/14',
                gericht: 'Arbeitsgericht Koblenz',
                kurzrubrum: 'Brigitta Pallag ./. Alfred Banyai',
                saal: 'Sitzungssaal A022'
            }
        ];
    });
})();
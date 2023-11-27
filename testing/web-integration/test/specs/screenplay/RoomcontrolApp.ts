import { By, PageElement, PageElements } from '@serenity-js/web'

// removal of Target.
// https://serenity-js.org/handbook/getting-started/upgrading-to-serenity-js-3/#portable-pageelements

export class Roomcontrol {
  error = 0
  static versionElement = PageElement.located(By.css('body > app-root > div > div.alert.alert-app-level')).describedAs('version element')

  static titleNavField = PageElement.located(By.css('body > app-root > div > header > div.branding > a > span')).describedAs('title element')

  static DisplayItems = PageElements.located(By.css('body > app-root > div > div.content-container > div > app-home > li > a')).describedAs('display items')

  static FirstDisplayLi = PageElement.located(By.css('body > app-root > div > div.content-container > div > app-home > li')).describedAs('display items')

  static clockTimeTableElement = PageElement.located(By.css('app-root clr-dg-column.ds-termin-column-uhrzeit.datagrid-column.datagrid-fixed-width > div > span')).describedAs('clock time header')
  static emptyList = PageElement.located(By.css('.datagrid div.datagrid-placeholder-image')).describedAs('no rows message')

  static AppointmentListItems = PageElements.located(By.css('app-root app-display app-termine clr-datagrid clr-dg-table-wrapper > div.datagrid-body > clr-dg-row')).describedAs('appointment entries')

  static displayStatusCard = PageElement.located(By.css('app-root app-display app-display-control > div > div.card-header')).describedAs('display card')
  static turnOnCard = PageElement.located(By.css('.display-control .card-footer > button.btn'))
}

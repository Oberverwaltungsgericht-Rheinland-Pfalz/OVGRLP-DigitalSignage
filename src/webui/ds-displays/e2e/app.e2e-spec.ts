import { DsDisplaysPage } from './app.po';

describe('ds-displays App', () => {
  let page: DsDisplaysPage;

  beforeEach(() => {
    page = new DsDisplaysPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});

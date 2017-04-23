import { DsRoomcontrolPage } from './app.po';

describe('ds-roomcontrol App', () => {
  let page: DsRoomcontrolPage;

  beforeEach(() => {
    page = new DsRoomcontrolPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});

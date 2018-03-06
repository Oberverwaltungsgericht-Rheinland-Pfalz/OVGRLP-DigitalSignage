import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NjzFoyerComponent } from './njz-foyer.component';

describe('NjzFoyerComponent', () => {
  let component: NjzFoyerComponent;
  let fixture: ComponentFixture<NjzFoyerComponent>;

  beforeEach(
    async(() => {
      TestBed.configureTestingModule({
        declarations: [NjzFoyerComponent]
      }).compileComponents();
    })
  );

  beforeEach(() => {
    fixture = TestBed.createComponent(NjzFoyerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

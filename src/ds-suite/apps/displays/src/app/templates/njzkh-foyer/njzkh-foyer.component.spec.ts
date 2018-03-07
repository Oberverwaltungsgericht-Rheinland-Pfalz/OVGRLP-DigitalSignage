import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NjzkhFoyerComponent } from './njzkh-foyer.component';

describe('NjzkhFoyerComponent', () => {
  let component: NjzkhFoyerComponent;
  let fixture: ComponentFixture<NjzkhFoyerComponent>;

  beforeEach(
    async(() => {
      TestBed.configureTestingModule({
        declarations: [NjzkhFoyerComponent]
      }).compileComponents();
    })
  );

  beforeEach(() => {
    fixture = TestBed.createComponent(NjzkhFoyerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

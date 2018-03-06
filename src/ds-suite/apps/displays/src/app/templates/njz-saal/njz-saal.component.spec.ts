import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NjzSaalComponent } from './njz-saal.component';

describe('NjzSaalComponent', () => {
  let component: NjzSaalComponent;
  let fixture: ComponentFixture<NjzSaalComponent>;

  beforeEach(
    async(() => {
      TestBed.configureTestingModule({
        declarations: [NjzSaalComponent]
      }).compileComponents();
    })
  );

  beforeEach(() => {
    fixture = TestBed.createComponent(NjzSaalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

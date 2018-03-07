import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EdvgtSteleComponent } from './edvgt-stele.component';

describe('EdvgtSteleComponent', () => {
  let component: EdvgtSteleComponent;
  let fixture: ComponentFixture<EdvgtSteleComponent>;

  beforeEach(
    async(() => {
      TestBed.configureTestingModule({
        declarations: [EdvgtSteleComponent]
      }).compileComponents();
    })
  );

  beforeEach(() => {
    fixture = TestBed.createComponent(EdvgtSteleComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

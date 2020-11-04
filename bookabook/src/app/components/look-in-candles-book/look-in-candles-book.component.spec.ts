import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LookInCandlesBookComponent } from './look-in-candles-book.component';

describe('LookInCandlesBookComponent', () => {
  let component: LookInCandlesBookComponent;
  let fixture: ComponentFixture<LookInCandlesBookComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LookInCandlesBookComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LookInCandlesBookComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { MyBooksThatBorroedComponent } from './my-books-that-borroed.component';

describe('MyBooksThatBorroedComponent', () => {
  let component: MyBooksThatBorroedComponent;
  let fixture: ComponentFixture<MyBooksThatBorroedComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ MyBooksThatBorroedComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(MyBooksThatBorroedComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

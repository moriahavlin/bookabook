import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UsersBasketBooksComponent } from './users-basket-books.component';

describe('UsersBooksComponent', () => {
  let component: UsersBasketBooksComponent;
  let fixture: ComponentFixture<UsersBasketBooksComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UsersBasketBooksComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UsersBasketBooksComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

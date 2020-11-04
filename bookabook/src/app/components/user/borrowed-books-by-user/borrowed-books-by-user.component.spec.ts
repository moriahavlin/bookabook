import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BorrowedBooksByUserComponent } from './borrowed-books-by-user.component';

describe('BorrowedBooksByUserComponent', () => {
  let component: BorrowedBooksByUserComponent;
  let fixture: ComponentFixture<BorrowedBooksByUserComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BorrowedBooksByUserComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BorrowedBooksByUserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

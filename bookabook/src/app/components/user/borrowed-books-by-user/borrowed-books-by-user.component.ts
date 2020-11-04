import { Component, OnInit } from '@angular/core';
import { BookService } from '../../../services/book.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Books } from 'src/app/models/books';
import { LendsService } from 'src/app/services/lends.service';
import { lends } from 'src/app/models/lends';
import { BaseService } from 'src/app/services/base.service';

@Component({
  selector: 'app-borrowed-books-by-user',
  templateUrl: './borrowed-books-by-user.component.html',
  styleUrls: ['./borrowed-books-by-user.component.css']
})
export class BorrowedBooksByUserComponent implements OnInit {
  borrowedBook: Array<lends>=[];
  displayedColumns: string[] = ['Book Name','Book Auther','Book Publishing','Book Buttons'];
  constructor(private lendsService:LendsService,
    private baseService:BaseService ,private router: ActivatedRoute ,private route:Router){}

  ngOnInit() {
 this.loadBooks();
  }

loadBooks()
{
  if (this.baseService.isLogin==false) { 
      
    this.route.navigate(['/login']);

  }
  else{
  this.lendsService.getBorrowedBookByUser(this.baseService.activeUser.id).subscribe((res) => {
    this.borrowedBook = res;
    console.log(res);
  });
}
}

  returnBook(bookToReturn: number){
    this.lendsService.returnBook(bookToReturn)
    .subscribe((res) => {
      if (res == false)
      confirm("אירעה שגיאה")
      else
      confirm("נשלחה הודעה לך ולבעלי הספר עם פרטי יצירת קשר")
      this.loadBooks();
    });
  }
 

}

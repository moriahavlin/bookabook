import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { MatDialog } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { BookService } from 'src/app/services/book.service';
import { BaseService } from 'src/app/services/base.service';
import { LendsService } from 'src/app/services/lends.service';
import { lends } from 'src/app/models/lends';

@Component({
  selector: 'app-my-books-that-borroed',
  templateUrl: './my-books-that-borroed.component.html',
  styleUrls: ['./my-books-that-borroed.component.css']
})
export class MyBooksThatBorroedComponent implements OnInit {

  constructor(
    private userService:UserService, 
    public dialog: MatDialog,private router: ActivatedRoute
     ,private route:Router,private BookService:BookService,private baseServise:BaseService,private lendsService:LendsService
  ) { }

  ngOnInit() {
   this.loadBooks();
  }
 
  borrowedBook: Array<lends>=[];
  displayedColumns: string[] = ['Book Name','Book Auther','Book Publishing','borrower name','borrower email','borrower phone','Book Buttons'];
  loadBooks()
  {
    if (this.baseServise.isLogin==false) { 
        
      this.route.navigate(['/login']);
  
    }
    else{
    this.lendsService.getAllmyBooksThatBorrowed(this.baseServise.activeUser.id).subscribe((res) => {
      this.borrowedBook = res;
      console.log(res);
    });
  }}

  
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

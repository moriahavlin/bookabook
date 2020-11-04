import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { MatDialog } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { NewBookComponent } from 'src/app/dialogs/new-book/new-book.component';
import { BookService } from 'src/app/services/book.service';
import { Books } from 'src/app/models/books';
import { BaseService } from 'src/app/services/base.service';

@Component({
  selector: 'app-my-books',
  templateUrl: './my-books.component.html',
  styleUrls: ['./my-books.component.css']
})
export class MyBooksComponent implements OnInit {
  displayedColumns: string[] = ['Book ID','Book Name', 'Book Description','Book Buttons'];
  constructor(private userService:UserService, 
    public dialog: MatDialog,private router: ActivatedRoute
     ,private route:Router,private BookService:BookService,private baseServise:BaseService
    ) { }
//לשנות !!!!

myBooks:Books[];
  ngOnInit() {
    if (this.baseServise.isLogin==false) { 
      
      this.route.navigate(['/login']);

    }
    else{
    this.BookService.getAllUsersBooksByIDU(this.baseServise.activeUser.id).subscribe(result => {
      this.myBooks=result;
    });
  }
  }
  openDialog( idB): void {
    
    const dialogRef = this.dialog.open(NewBookComponent, {
      width: '750px',
      data: {Id:idB}
    });
    dialogRef.afterClosed().subscribe(result => {
    });
  }
}

import { Component, OnInit } from '@angular/core';
import { Books } from '../../../models/books';
import { BookService } from '../../../services/book.service';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA, MatSnackBar } from '@angular/material';
import { NewBookComponent } from '../../../dialogs/new-book/new-book.component';
import { LendsService } from 'src/app/services/lends.service';
import { BaseService } from 'src/app/services/base.service';


@Component({
  selector: 'users-basket-books',
  templateUrl: './users-basket-books.component.html',
  styleUrls: ['./users-basket-books.component.css']
})

export class UsersBasketBooksComponent implements OnInit {
  booksArry: Array<Books> = [];
  bookToUpdate: Books;
  myLoveBooks: Array<Books> = [];
  displayedColumns: string[] = ['Book ID', 'Book Name', 'Book Description', 'Book Buttons'];
  constructor(private booksService: BookService,
    private lendService: LendsService,
    public dialog: MatDialog,
    private baseService: BaseService,
    private snackBar: MatSnackBar,
    private router: ActivatedRoute,
    private route: Router) { }



  ngOnInit() {
    this.lodeBuskate()

  }

  lodeBuskate() {
    if (this.baseService.isLogin == false) {

      this.route.navigate(['/login']);

    }
    else {
      this.booksService.getAllMyBasket(this.baseService.activeUser.id).subscribe((res) => {
        this.myLoveBooks = res;
      });
    }
  }

  openDialog(idBookToUpdate: number): void {
    const dialogRef = this.dialog.open(NewBookComponent, {
      width: '500px',
      data: { Id: idBookToUpdate }
    });
    dialogRef.afterClosed().subscribe(result => {

      // this.myNewEmptyBook = result.NewEmptyBook;
      // this.booksArry.push(this.myNewEmptyBook);


    });
  }
  deleteBookFromMyBuskate(deleteBook: Books): void {
    if (this.baseService.isLogin == false) {

      this.route.navigate(['/login']);

    }
    else {
      if (confirm("האם אתה בטוח שברצונך למחוק ספר זה מסל הספרים האהובים שלי?")) {
        this.snackBar.open(" הספר נמחק בהצלחה ", "", {
          duration: 5000,
        });

        this.booksService.deleteBookFromMyBuskate(deleteBook.id,this.baseService.activeUser.id).subscribe(result => {
          this.lodeBuskate();
        });


      }
    }

  }


  borrowBook(id: number) {
    if (this.baseService.isLogin == false) {

      this.route.navigate(['/login']);

    }
    else {

      this.snackBar.open(" הבקשה שלך להשאלת הספר נשלחה תוכל להתעדכן בדף ספרים הממתינים לאישור עדכונים ישלחו במייל ", "", {
        duration: 5000,
      });

      this.lendService.borrowBook(id, this.baseService.activeUser.id)
        .subscribe((res) => {
        });
    }
  }

  updateWhenBookIsAvailable(id: number) {
    if (this.baseService.isLogin == false) {

      this.route.navigate(['/login']);

    }
    else {

      this.snackBar.open("הבקשה התקבלה בהצלחה ברגע שהספר יתפנה תקבל מייל ", "", {
        duration: 5000,
      });
      this.lendService.updateWhenBookIsAvailable(id, this.baseService.activeUser.id)
        .subscribe((res) => {
        });
    }
  }

}

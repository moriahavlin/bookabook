import { Component, OnInit } from '@angular/core';
import { BookService } from '../../../services/book.service';
import { Books } from '../../../models/books';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA, MatSnackBar } from '@angular/material';
import { ActivatedRoute, Router, NavigationExtras } from '@angular/router'
import { MyBasketOfBooks } from 'src/app/models/MyBasketOfBooks';
import { LendsService } from 'src/app/services/lends.service';
import { BookToList } from 'src/app/models/BookToList';
import { BaseService } from 'src/app/services/base.service';
import { LookupService } from 'src/app/services/lookup.service';
import { Lookup } from 'src/app/models/lookup';
@Component({
  selector: 'app-books',
  templateUrl: './books-list.component.html',
  styleUrls: ['./books-list.component.css']
})

export class BooksListComponent implements OnInit {
  categoryList: Lookup[];
  massageOfSerch = "";
  nameBookToSearch: string = "";
  categoryIdSearch = 0;
  cityToSearch: string = "";
  publishingToSerch: String="";

  autherToSerch: String="";
  ShowBorrowedBooks: boolean = false;
  booksArry: Array<BookToList> = [];
  userLoveBooks: Array<Books> = [];
  constructor(private booksService: BookService,
    private lendService: LendsService,
    private lookupService: LookupService,
    public dialog: MatDialog,
    private router: ActivatedRoute,
    private baseService: BaseService,
    private snackBar: MatSnackBar,
    private route: Router) { }
  ngOnInit() {
    this.loadeBookList();
    this.lookupService.getAllCategory().subscribe((res) => {
      this.categoryList = res;
    });
    // קליטה של כל שמות הספרים אזור המיקום (ארץ) ואולי עוד פרטים 
  }

  loadeBookList() {

    this.booksService.getAllBooks().subscribe((res) => {
      this.massageOfSerch = "";
      this.booksArry = res;
      for (let item of this.booksArry) {
        if (item.picNAme != null)
          item.picNAme = "http://localhost:56996/Images/" + item.picNAme;
        else item.picNAme = "http://localhost:56996/Images/noPic.jpg";

      }
    });
  }
  search() {
    if (this.nameBookToSearch == "" && this.categoryIdSearch == 0) {

      this.booksService.getAllBooks().subscribe((res) => {
        this.massageOfSerch = "";
        this.booksArry = res;
        for (let item of this.booksArry) {
          if (item.picNAme != null)
            item.picNAme = "http://localhost:56996/Images/" + item.picNAme;
          else item.picNAme = "http://localhost:56996/Images/noPic.jpg";
        }
      });
    } else {
      this.booksService.Serch(this.nameBookToSearch, this.categoryIdSearch).subscribe((res) => {
        this.massageOfSerch = "הנתונים הם לפי סינון של :";
        if (this.nameBookToSearch != "")
          this.massageOfSerch += " שם ספר: " + this.nameBookToSearch;
        if (this.categoryIdSearch != 0)
          this.massageOfSerch += " קטגוריה: " + this.categoryIdSearch;

        this.booksArry = res;
        for (let item of this.booksArry) {
          if (item.picNAme != null)
            item.picNAme = "http://localhost:56996/Images/" + item.picNAme;
          else item.picNAme = "http://localhost:56996/Images/noPic.jpg";
        }
      });
    }
  }
  openOneBook(id: number) {
    let navigationExtras: NavigationExtras = {
      queryParams: {
        "idB": JSON.stringify(id)
      }
    };
    this.route.navigate(['/oneBook'], navigationExtras);
  }


  addBookToMybasket(b: Books) {
    if (this.baseService.isLogin == false) {
      this.route.navigate(['/login']);
    }
    else {
      this.snackBar.open("הספר נוסף בהצלחה לספרים האהובים שלך", "", {
        duration: 2000,
      });
      this.booksService.getAllMyBasket(this.baseService.activeUser.id).subscribe((res) => {
        this.userLoveBooks = res;
        for (let el of this.userLoveBooks) {
          if (el.id == b.id) {
            return;
          }
        }
        let myBasketOfBooks: MyBasketOfBooks = {
          idBook: b.id,
          idUser: this.baseService.activeUser.id,
          id: 0
        }
        this.booksService.addBookToMybasket(myBasketOfBooks)
          .subscribe((res) => {
          });
      });
    }
  }

  borrowBook(id: number) {
    if (this.baseService.isLogin == false) {
      this.route.navigate(['/login']);
    }
    else {
      this.snackBar.open(" הבקשה שלך להשאלת הספר נשלחה תוכל להתעדכן בדף ספרים הממתינים לאישור,6 עדכונים ישלחו במייל ", "", {
        duration: 5000,
      });

      this.lendService.borrowBook(id, this.baseService.activeUser.id)
        .subscribe((res) => {
          if (res == false)
            confirm("אופס! הספר בדיוק הושאל. באפשרותך להיכנס לרשימת המתנה ואנו נשלח לך מייל כאשר הספר יהיה זמין שוב.")
          else
            this.loadeBookList();
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

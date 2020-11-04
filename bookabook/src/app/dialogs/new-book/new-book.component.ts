import { Component, OnInit, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { FormsModule, FormControl, FormGroup, Validators } from '@angular/forms'
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { Lookup } from '../../models/lookup';
import { Books } from '../../models/books';
import { ReactiveFormsModule } from '@angular/forms';
import { bookName } from '../../models/booksName';
import { BookService } from 'src/app/services/book.service';
import { LookupService } from 'src/app/services/lookup.service';
import { Observable } from 'rxjs';
import { map, startWith } from 'rxjs/operators';
// import {MatFormFieldModule} from '@angular/material/form-field';
// import {MatInputModule} from '@angular/material/input';
@Component({
  selector: 'app-new-book',
  templateUrl: './new-book.component.html',
  styleUrls: ['./new-book.component.css']
})
export class NewBookComponent implements OnInit {
  imageBook: File = null;
  imageUrl: string = "";
  categoryList: Lookup[];
  singleBook: Books;
  isEdit: boolean;//אם זה true זה אומר שהגעתי לכאן עם עריכה כלשהי
  //Authers
  myAuthersControl = new FormControl();
  filteredAuthersList: Observable<Lookup[]>;
  AuthersList: Lookup[];
  addBookForm: FormGroup;
  // publishing
  myPublishinControl = new FormControl();
  publishingsList: Lookup[];
  filteredpublishingsList: Observable<Lookup[]>;


  // BookName
  myBookNameControl = new FormControl();
  BookNameList: bookName[];
  filteredBookNameList: Observable<bookName[]>;

  ngOnInit() {
    this.lodeLookup();
    this.addBookForm = new FormGroup({
      'myBookNameControl': new FormControl("", [Validators.required]),
      'categoryId': new FormControl("", [Validators.required]),
      'publishingId': new FormControl("", [Validators.required]),
      'description': new FormControl("", [Validators.required]),
      'autherId': new FormControl("", [Validators.required]),
      'numOfPages': new FormControl("", [Validators.required]),
      'picNAme': new FormControl("", [Validators.required]),
    });
    if (this.data.Id != 0) {
      //נשלח id של ספר לעריכה - יש לשלוף אותו 
      this.bookService.getBookById(this.data.Id).subscribe((res) => {
        if (res != undefined) {
          // this.singleBook = res;
          this.addBookForm.patchValue({
          'nameId': res.nameId,
          'categoryId':res.categoryId,
          'publishingId': res.publishingId,
          'description': res.description,
          'autherId': res.autherId,
          'numOfPages': res.numOfPages,
          'picNAme': res.picNAme,
        })
          // console.log(res);
        }
        else {
          //לא הצליח לשלוף מהDB
          console.log("שליפה של הספר נכשלה")
          return;
        }
      });
      //     this.singleBook = dbStatic.booksArry.find(b => b.id === data.Id);
      this.isEdit = true;
    }
    else {
      this.isEdit = false;
      this.singleBook = new Books();
      this.singleBook.autherId = new Lookup();
      this.singleBook.categoryId = new Lookup();
      this.singleBook.nameId = new bookName();
      this.singleBook.publishingId = new Lookup();
    }
  }
  constructor(private bookService: BookService,
    private lookupService: LookupService,
    public dialogRef: MatDialogRef<NewBookComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) {

  }
  lodeLookup() {
    // category

    this.lookupService.getAllCategory().subscribe((res) => {
      this.categoryList = res;
    });
    // publishing
    this.lookupService.getAllPublishing().subscribe((res) => {
      this.publishingsList = res;
      this.filteredpublishingsList = this.myPublishinControl.valueChanges
        .pipe(
          startWith<string | Lookup>(''),
          map(value => typeof value === 'string' ? value : value.Desc),
          map(Desc => Desc ? this._filterpublishing(Desc) : this.publishingsList.slice())
        );
    });
    //Authers
    this.lookupService.getAllAuthers().subscribe((res) => {
      this.AuthersList = res;
      this.filteredAuthersList = this.myAuthersControl.valueChanges
        .pipe(
          startWith<string | Lookup>(''),
          map(value => typeof value === 'string' ? value : value.Desc),
          map(Desc => Desc ? this._filterAuthers(Desc) : this.AuthersList.slice())
        );
    });
    // BookName
    this.lookupService.getAllBookName().subscribe((res) => {
      this.BookNameList = res;
      this.filteredBookNameList = this.myBookNameControl.valueChanges
        .pipe(
          startWith<string | bookName>(''),
          map(value => typeof value === 'string' ? value : value.name),
          map(name => name ? this._filterBookName(name) : this.BookNameList.slice())
        );
    });
  }
  //Authers  publishing
  displayFn(lookup?: Lookup): string | undefined {
    return lookup ? lookup.Desc : undefined;
  }
  // BookName
  displayFnBookName(BookName?: bookName): string | undefined {
    return BookName ? BookName.name : undefined;
  }
  //Authers
  private _filterAuthers(Desc: string): Lookup[] {
    const filterValue = Desc.toLowerCase();
    return this.AuthersList.filter(option => option.Desc.toLowerCase().indexOf(filterValue) === 0);
  }
  // publishing
  private _filterpublishing(Desc: string): Lookup[] {
    const filterValue = Desc.toLowerCase();
    return this.publishingsList.filter(option => option.Desc.toLowerCase().indexOf(filterValue) === 0);
  }
  // BookName
  private _filterBookName(name: string): bookName[] {
    const filterValue = name.toLowerCase();
    return this.BookNameList.filter(option => option.name.toLowerCase().indexOf(filterValue) === 0);
  }
  onNoClick(): void {
    this.dialogRef.close();
  }
  onAddClick(): void {
    console.log(this.singleBook);
    console.log(this.myBookNameControl.value);
    this.singleBook.nameId = this.myBookNameControl.value;
    debugger;
    this.bookService.insertBook(this.singleBook, this.imageBook).subscribe((res) => {
      this.dialogRef.close();
    });
  }
  onEditClick() {
    this.bookService.editBook(this.singleBook).subscribe((res) => {
      this.dialogRef.close();
    });
  }
  selectBookName() {
    if (this.myBookNameControl) {
      if (this.myBookNameControl.value) {
        if (typeof (this.myBookNameControl.value) != "object") {
          if (this.BookNameList.find((book) => book.name == this.myBookNameControl.value)) {
            this.myBookNameControl.setValue((this.BookNameList.find((book) => book.name == this.myBookNameControl.value)));
          }

        } else {
          let newBookName: bookName = new bookName();
          newBookName.id = -1;
          newBookName.name = this.myBookNameControl.value;

          this.myBookNameControl.setValue(newBookName);
        }

      }
    }
  }

  // onSelectFile(event) {
  //   if (event.target.files && event.target.files[0]) {
  //     this.imageBook = event.target.files[0];
  //     const reader = new FileReader();
  //     reader.onload = e => this.selectedImage = reader.result.toString();
  //     this.fileName = event.target.files[0].name;
  //     reader.readAsDataURL(this.imageToUpload);
  //   }
  // }
  onFileChanged(file: FileList) {
    this.imageBook = file[0];
    var render = new FileReader;
    render.onload = (event: any) => {
      this.imageUrl = event.target.result;
    }
    render.readAsDataURL(this.imageBook);
  }

  getRequiredErrorMessage(fieldName) {
    return `שדה ${fieldName} הנו חובה`
  }
  // myUploader(event) {
  //   console.log("myUploader");
  // }
}

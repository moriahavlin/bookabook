//@anglar...
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AgmCoreModule } from '@agm/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
// import Chart from 'chart.js'
import { ReactiveFormsModule } from '@angular/forms';

//classes
import {PrimeNgModule}from'./primeNg';
import { MatirialModule } from './matearial';
import { router } from './app.routing';
import { CarouselModule, WavesModule } from 'angular-bootstrap-md'
import { FormControl, FormGroupDirective, NgForm, Validators } from '@angular/forms';
import { ErrorStateMatcher } from '@angular/material';

// export class MyErrorStateMatcher implements ErrorStateMatcher {
//   isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
//     const isSubmitted = form && form.submitted;
//     return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
//   }
// }


//service
import { BaseService } from './services/base.service';
import { UserService } from './services/user.service';
import { BookService } from './services/book.service';
import { LookupService } from './services/lookup.service'
import { LendsService } from './services/lends.service'
//component
import { HomeComponent } from './components/home/home.component';
import { EditUserDetailsComponent } from './dialogs/edit-user-details/edit-user-details.component';
import { BookDetailsComponent } from './components/books/book-details/book-details.component';
import { NewBookComponent } from './dialogs/new-book/new-book.component';
import { LoginComponent } from './components/user/login/login.component';
import { MapsComponent } from './components/maps/maps.component';
import { UsersBasketBooksComponent } from './components/user/users-basket-books/users-basket-books.component';
import { BorrowedBooksByUserComponent } from './components/user/borrowed-books-by-user/borrowed-books-by-user.component';
import { BooksListComponent } from './components/books/books-list/books-list.component';

import { AppComponent } from './app.component';
import { NuvComponent } from './components/nuv/nuv.component';
import { UserAccountComponent } from './components/user/user-account/user-account.component';
import { AboutTheSiteComponent } from './components/about-the-site/about-the-site.component';
import { LookInCandlesBookComponent } from './components/look-in-candles-book/look-in-candles-book.component';
import { UserDetailsComponent } from './components/user/user-details/user-details.component';
import { MyBooksComponent } from './components/user/my-books/my-books.component';
import { WaitingForApprovalComponent } from './components/user/waiting-for-approval/waiting-for-approval.component';
import { MyBooksThatBorroedComponent } from './components/user/my-books-that-borroed/my-books-that-borroed.component';





@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    NuvComponent,
    BooksListComponent,
    BookDetailsComponent,
    NewBookComponent,
    LoginComponent,
    AppComponent,
    MapsComponent,   
    BorrowedBooksByUserComponent,
    UsersBasketBooksComponent,
    EditUserDetailsComponent,
    UserAccountComponent,
    AboutTheSiteComponent,
    LookInCandlesBookComponent,
    UserDetailsComponent,
    MyBooksComponent,
    WaitingForApprovalComponent,
    MyBooksThatBorroedComponent,
    
   
  ],
  imports: [
    BrowserModule,
    PrimeNgModule,
    HttpClientModule,
    CarouselModule.forRoot(),
    WavesModule.forRoot(),
    FormsModule,
    RouterModule.forRoot(router, { useHash: false }),
    BrowserAnimationsModule,
    MatirialModule,
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    
    AgmCoreModule.forRoot({


      apiKey: 'AIzaSyAwkqjyL91gb8amZim2EaZd7SmE0aO04Os',
      language: 'he'
    })

  ],
  providers: [BaseService, UserService,LookupService,
    BookService,LendsService],
  bootstrap: [AppComponent],
  // exports:[NewBookComponent],
  entryComponents: [
    NewBookComponent,
    EditUserDetailsComponent
  ],

})
export class AppModule { }




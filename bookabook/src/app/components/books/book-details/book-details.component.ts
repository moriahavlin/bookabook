import { Component, OnInit } from '@angular/core';
import { Books } from '../../../models/books';

import { Lookup } from '../../../models/lookup';
import { bookName } from '../../../models/booksName';
import { ActivatedRoute, Router } from '@angular/router';
import { BookService } from 'src/app/services/book.service';
import { LookupService } from 'src/app/services/lookup.service';
import { RatingModule } from 'primeng/rating';
import { BaseService } from 'src/app/services/base.service';
import { LendsService } from 'src/app/services/lends.service';
import { MatSnackBar } from '@angular/material';
import { rating } from 'src/app/models/rating';
// import { runInThisContext } from 'vm';

@Component({
  selector: 'app-book',
  templateUrl: './book-details.component.html',
  styleUrls: ['./book-details.component.css']
})
export class BookDetailsComponent implements OnInit {





  chartdata: any;

  signalBook: Books;
  idB: number;
  kehalYaadSelect: Array<Lookup>;
  selectedKehalYaad: Array<Lookup>;
  rating: RatingModule;
  newRating:rating=new rating();

  constructor(private route: ActivatedRoute,
    private booksServic: BookService,
    private lookupService: LookupService,
    private snackBar: MatSnackBar,

    private baseService: BaseService,
    private router: Router,
    private lendService:LendsService) { }

  ngOnInit() {


    this.route.queryParams.subscribe(params => {
      this.idB = JSON.parse(params["idB"]);
    });
    this.booksServic.getBookById(this.idB).subscribe((res) => {
      this.signalBook = res;
      this.chartdata = {
        labels: ['בנים', 'בנות'],
        datasets: [
          {
            data: [this.signalBook.Statistics.numberOfMen, this.signalBook.Statistics.numberOfwomen],
            backgroundColor: [
              "#36A2EB",
              "#FF6384",
            ],
            hoverBackgroundColor: [
              "#36A2EB",
              "#FF6384",
            ]
          }]
      };
      if (this.signalBook.picNAme != null)
        this.signalBook.picNAme = "http://localhost:56996/Images/" + this.signalBook.picNAme;
      else this.signalBook.picNAme = "http://localhost:56996/Images/noPic.jpg";
    },
      (err) => console.log(err)
    );


    this.booksServic.PromoteNumberOfViewers(this.idB).subscribe((res) => {
    })

    this.lookupService.getAllKehalYaad().subscribe((res) => {
      this.kehalYaadSelect = res;
    })


  }
  updateWhenBookIsAvailable(id: number) {
    if (this.baseService.isLogin == false) {
      this.router.navigate(['/login']);
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
  rateBook() {
// confirm(this.newRating.desk+" "+this.newRating.rating )
this.booksServic.rateBook(this.idB,this.newRating.desk,this.newRating.rating)
        .subscribe((res) => {
        });
  }
}

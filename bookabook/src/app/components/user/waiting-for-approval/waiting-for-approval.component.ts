import { Component, OnInit } from '@angular/core';
import { Books } from 'src/app/models/books';
import { UserService } from 'src/app/services/user.service';
import { LendsService } from 'src/app/services/lends.service';
import { lends } from 'src/app/models/lends';
import { BaseService } from 'src/app/services/base.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-waiting-for-approval',
  templateUrl: './waiting-for-approval.component.html',
  styleUrls: ['./waiting-for-approval.component.css']
})
export class WaitingForApprovalComponent implements OnInit {
  // booksArry: Array<Books> = [];
  //ספרים שאני ממתין שיאשרו לי 
  booksUserWantedArr: Array<lends> = [];
  //displayedColumnsWanted: string[] = ['Lender Id', 'Book Id'];
    displayedColumnsWanted: string[] = ['Lender Id', 'Book Id','Book Name' ,'Book Buttons'];

  //ספרים שניא צריך לאשר או לדחות
  LendersBooksUserWantedToBorrow: Array<lends> = [];
  displayedColumns: string[] = ['Lender Id', 'Book Id', 'Book Name', 'Book Auther', 'borrower name', 'Book Buttons'];
  
  constructor(private LendsService: LendsService,
    private baseService: BaseService,
    private route: Router) { }

  ngOnInit() {
   
   this.loadDedeils();
  }
  loadDedeils(){
    if (this.baseService.isLogin==false) { 
      
      this.route.navigate(['/login']);

    }
    else{

    this.LendsService.getAllWaitingForApprovalByUser(this.baseService.activeUser.id).subscribe((res) => {
     
      this.LendersBooksUserWantedToBorrow = res;
    });
    //  לטפל!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    this.LendsService.getBooksUserWantedToBorrow(this.baseService.activeUser.id).subscribe((ress) => {
      console.log(ress);
      this.booksUserWantedArr = ress;
    });
  }
  }
  confirmBorrow(idBorrow: number) {
    this.LendsService.confirmBorrow(idBorrow)
      .subscribe((res) => {
       
        this.loadDedeils();
      })
  }
  rejectBorrow(idBorrow: number) {
    this.LendsService.rejectBorrow(idBorrow)
      .subscribe((res) => {
        this.loadDedeils();
      })
  }
}

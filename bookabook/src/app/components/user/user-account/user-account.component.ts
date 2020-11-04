import { Component, OnInit } from '@angular/core';
import { BookService } from 'src/app/services/book.service';
import { MatDialog } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { NewBookComponent } from 'src/app/dialogs/new-book/new-book.component';
import { user } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';
import { EditUserDetailsComponent } from 'src/app/dialogs/edit-user-details/edit-user-details.component';
import { BaseService } from 'src/app/services/base.service';

@Component({
  selector: 'app-user-account',
  templateUrl: './user-account.component.html',
  styleUrls: ['./user-account.component.css']
})
export class UserAccountComponent implements OnInit {

  constructor(private userService: UserService,
    public dialog: MatDialog, private router: ActivatedRoute
    , private route: Router,private baseService: BaseService) { }

  ngOnInit() {
    if (this.baseService.isLogin==false) { 
      
      this.route.navigate(['/login']);

    }

  }

}

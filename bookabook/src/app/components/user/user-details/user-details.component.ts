import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/services/user.service';
import { MatDialog } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { user } from 'src/app/models/user';
import { EditUserDetailsComponent } from 'src/app/dialogs/edit-user-details/edit-user-details.component';
import { NewBookComponent } from 'src/app/dialogs/new-book/new-book.component';
import { router } from 'src/app/app.routing';
import { BaseService } from 'src/app/services/base.service';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-details.component.html',
  styleUrls: ['./user-details.component.css']
})
export class UserDetailsComponent implements OnInit {

  constructor(private userService: UserService,
     public dialog: MatDialog, private router: ActivatedRoute, 
     private route: Router,private baseService:BaseService ) { }
  signedUser: user;
  idU: number;

  ngOnInit() {
    this.loadUserDetails();

  }

  loadUserDetails() {
    if (this.baseService.isLogin==false) { 
      
      this.route.navigate(['/login']);

    }
    else{
    this.userService.getUserById(this.baseService.activeUser.id).subscribe((res) => {
      this.signedUser = res;
      console.log(res);
    });
  }

  }


  openUserDialog(): void {
    if (this.baseService.isLogin==false) { 
      
      this.route.navigate(['/login']);

    }else{
    const dialogRef = this.dialog.open(EditUserDetailsComponent, {
      width: '500px',
      data: { Id: this.baseService.activeUser.id, }
    });
    dialogRef.afterClosed().subscribe(result => {
      this.loadUserDetails();
    });
  }
  }
}

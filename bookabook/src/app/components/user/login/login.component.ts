import { Component, OnInit } from '@angular/core';
import { Validators, FormControl, FormBuilder } from '@angular/forms';
import { user } from '../../../models/user';
import { UserService } from '../../../services/user.service';
import { EditUserDetailsComponent } from 'src/app/dialogs/edit-user-details/edit-user-details.component';
import { MatDialog } from '@angular/material';
import { ActivatedRoute, Router } from '@angular/router';
import { Login } from 'src/app/models/login';
import { BaseService } from 'src/app/services/base.service';
// import { MyErrorStateMatcher } from '../app.module';
import { Location } from '@angular/common';
import { LendsService } from 'src/app/services/lends.service';
import { lends } from 'src/app/models/lends';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(public UserService: UserService, public baseService: BaseService, public dialog: MatDialog,
    private router: ActivatedRoute, private route: Router, private fb: FormBuilder,
    private location: Location, private lendsService: LendsService) { }
  message = "";
  names = ""
  borrowedBook: Array<lends> = [];
  loginModel: Login = new Login();
  login() {
    if (this.loginModel.email == undefined || this.loginModel.password == undefined) {
      this.message = "יש להכניס את מייל וסיסמה";
    }
    else {
      this.baseService.login(this.loginModel).subscribe(result => {
        if (result == 0) {
          this.message = "המשתמש לא קיים במערכת";
        }
        else {
          if (result == -1) {
            this.message = "אחד מהנתונים לא תקין";
          }
          else {
            this.UserService.getUserById(result).subscribe(res => {
              this.baseService.setActiveUser(res);
              //this.router.snapshot.param
              this.getBooksUserDidntReturn(res);
              this.route.navigate(['/books'])
            })
          }
        }
      });
    }
  }
  getBooksUserDidntReturn(u: user) {
    this.lendsService.getBooksUserDidntReturn(u.id).subscribe((res) => {
      if (res != null) {
        this.borrowedBook = res;
        this.borrowedBook.forEach(element => {
          this.names += element.book.nameId.name.toString() + " ";
        });
        confirm("שים לב! ישנם מספר ספרים המושאלים אצלך למעלה מחודש:" + this.names)
      }
    });
  }
  logOut() {
    this.baseService.logOut();
    this.loginModel.email = null;
    this.loginModel.password = null;

  }
  register() {
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(EditUserDetailsComponent, {
      width: '750px',
      data: { Id: 0, }
    });
    dialogRef.afterClosed().subscribe(result => {

    });
  }
  ngOnInit() {
  }
  emailFormControl = new FormControl('', [
    Validators.required,
    Validators.email,
  ]);
  user: user;
  hide = true;
  // matcher = new MyErrorStateMatcher();
}

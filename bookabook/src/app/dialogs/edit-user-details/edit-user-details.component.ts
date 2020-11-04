import { Component, OnInit, Inject } from '@angular/core';
import { user } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';
import { FormControl, Validators, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { Lookup } from '../../models/lookup';
import { LookupService } from 'src/app/services/lookup.service';
import { BaseService } from 'src/app/services/base.service';
import { Login } from 'src/app/models/login';



@Component({
  selector: 'app-edit-user-details',
  templateUrl: './edit-user-details.component.html',
  styleUrls: ['./edit-user-details.component.css']
})
export class EditUserDetailsComponent implements OnInit {
  hide1 = true;
  hide2 = true;
  date = new FormControl();
  confirmPassword: string;
  genderList: Lookup[];
  userLogin: user;
  isEdit: boolean;
  date1: string = "XX-XX-XXXX";
  registerForm: FormGroup;
  message = "";
  constructor(private UserService: UserService,
    private lookupService: LookupService, private baseService: BaseService,
    public dialogRef: MatDialogRef<EditUserDetailsComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) {


  }
  ngOnInit() {
    this.lookupService.getGenderList().subscribe((res) => {
      this.genderList = res;
    });
    this.registerForm = new FormGroup({
      'firstName': new FormControl("", [Validators.required]),
      'lastName': new FormControl("", [Validators.required]),
      'email': new FormControl("", [Validators.email, Validators.required]),
      'phone': new FormControl("", [Validators.required, Validators.pattern("^[0-9]*$")]),
      'cityCode': new FormControl(new Lookup(), [Validators.required]),
      'address': new FormControl("", [Validators.required]),
      'houseNumber': new FormControl("", [Validators.required, Validators.pattern("^[0-9]*$")]),
      'neighborhood': new FormControl("", [Validators.required]),
      'genderId': new FormControl(new Lookup(), Validators.required),
      'birthDate': new FormControl('', [Validators.required]),
      'password': new FormControl(),
    });
    if (this.data.Id != 0) {
      this.UserService.getUserById(this.data.Id).subscribe((res) => {
        this.userLogin = res;
        this.isEdit = true;
        this.confirmPassword = res.password;
        if (res) {
          this.registerForm.patchValue({
            'firstName': res.firstName,
            'lastName': res.lastName,
            'email': res.email,
            'phone': res.phone,
            'cityCode': res.cityCode,
            'address': res.address,
            'houseNumber': res.houseNumber,
            'neighborhood': res.neighborhood,
            'genderId': res.genderId,
            'birthDate': res.birthDate,
            'password': res.password
          })
        }
      })
    }
    else {
      this.isEdit = false;
      this.userLogin = new user();
      this.userLogin.cityCode = new Lookup();
      this.userLogin.genderId = new Lookup();
    }


  }
  emailFormControl = new FormControl('', [
    Validators.required,
    Validators.email,
  ]);
  register() {
    if (this.registerForm.invalid) { return; }
    this.UserService.insertUser(this.registerForm.getRawValue()).subscribe((res) => {
      if (res != null) {
        this.baseService.setActiveUser(res);
        this.dialogRef.close();
      }
      else {
        this.message = "כתובת מייל כבר קיימת במערכת"
      }
    })
  }
  onEditClick() {
    this.userLogin.id = this.data.Id
    this.UserService.editUserDetails(this.userLogin).subscribe((res) => {
      this.dialogRef.close();
    })
  }

  getRequiredErrorMessage(fieldName) {
    return `שדה ${fieldName} הנו חובה`
  }
}

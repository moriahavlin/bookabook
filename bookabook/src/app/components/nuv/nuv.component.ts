import { Component, OnInit } from '@angular/core';
import { BaseService } from 'src/app/services/base.service';

@Component({
  selector: 'app-nuv',
  templateUrl: './nuv.component.html',
  styleUrls: ['./nuv.component.css']
})
export class NuvComponent implements OnInit {

  constructor(public baseService: BaseService) { }

  ngOnInit() {
  }
 public logOut() {
    this.baseService.logOut();
  }
}

import { Component, OnInit } from '@angular/core';
import { AgmCoreModule } from '@agm/core';

@Component({
  selector: 'app-maps',
  templateUrl: './maps.component.html',
  styleUrls: ['./maps.component.css']
})
export class MapsComponent implements OnInit {

  constructor() { }
  lat: number = 31.771959;
  lng: number = 35.217018;
  locationChosen=false;
  ngOnInit() {
  }
  onChoseKocation(event){
console.log(event);
this.lat=event.coords.lat;
this.lng=event.coords.lng;
this.locationChosen=true;
  }
}

// @NgModule({
//  imports: [
//     AgmCoreModule
//   ]
// })
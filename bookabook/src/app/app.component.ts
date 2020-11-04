import { Component,OnInit } from '@angular/core';
import { BookService } from './services/book.service';
import { Books } from './models/books';
import { NewBookComponent } from './dialogs/new-book/new-book.component';
import { HttpRequest } from '@angular/common/http';
import { LendsService } from './services/lends.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  ngOnInit() {
   //this.getFile();
 // this.getImageFromService()
  }

  title = 'books';

  name;
  // lat: number = 51.678418;
  // lng: number = 7.809007;
  // locationChosen=false;
  constructor(private booksService:BookService,private lenderService:LendsService){
  }
   confirmBorrow(idBorrow:number){
    this.lenderService.confirmBorrow(idBorrow)
     .subscribe((res) =>{

    })
  }
  rejectBorrow(idBorrow:number){
    this.lenderService.rejectBorrow(idBorrow)
     .subscribe((res) =>{

    })
  }
  // onChoseKocation(event){
  //   console.log(event);
  //   this.lat=event.coords.lat;
  //   this.lng=event.coords.lng;
  //   this.locationChosen=true;
  //     }
  imag;
  getBooksByName(){
    this.booksService.getBooksByName(this.name)
    .subscribe((res) =>{
      this.title = res['bookDescription'];

    })
  }
 
  imageToShow: any;
  isImageLoading:boolean=false;
createImageFromBlob(image: Blob) {
   let reader = new FileReader();
   reader.addEventListener("load", () => {
      this.imageToShow = reader.result;
   }, false);

   if (image) {
      reader.readAsDataURL(image);
   }
}
getImageFromService() {
  this.isImageLoading = true;
  this.booksService.getImage("miniFox.jpg").subscribe(data => {
    this.createImageFromBlob(data);
    this.isImageLoading = false;
  }, error => {
    this.isImageLoading = false;
    console.log(error);
  });
}
  getFile(){
    this.booksService.getFile("miniFox.jpg")
    .subscribe((res) =>{ 

      this.imag=res;
      console.log(res);

    },
    error => {
      console.log(error);
    })
  }

  uploadedFiles: any[] = [];



  onUpload(event) {
      for(let file of event.files) {
          this.uploadedFiles.push(file);
      }

     // this.messageService.add({severity: 'info', summary: 'File Uploaded', detail: ''});
  }
  myUploader(event) {
   console.log("myUploader");
}
}

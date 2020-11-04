import { Injectable } from "@angular/core";
import { BaseService } from "./base.service";
import { Books } from "../models/books";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable, observable } from "rxjs";
import { MyBasketOfBooks } from "../models/MyBasketOfBooks";
import { Lookup } from "../models/lookup";
import { formatDate } from "@angular/common";
import{BookToList} from'../models/BookToList'
const BookUrl = "http://localhost:56996/api/Book/";
@Injectable({
    providedIn: 'root'
})
export class BookService {

    constructor(private http: HttpClient) { }

    public getBooksByName(name) {
        return this.http.get(`${BookUrl}GetBookByName?name=${name}`);
    }
    public getAllBooks(): Observable<BookToList[]> {

        return this.http.get<BookToList[]>(`${BookUrl}GetAllBook`);
    }
  public Serch(nameToserch?:string,categoryIdSearch?:number): Observable<BookToList[]> {

//    let url=`${BookUrl}Serch`
//    if(nameToserch!="")
//    url+""
        return this.http.get<BookToList[]>(`${BookUrl}Serch?name=${nameToserch}&categoryIdSearch=${categoryIdSearch}`);
    }
    public getBookById(id: number): Observable<Books> {

        return this.http.get<Books>(`${BookUrl}GetBookById?idBook=${id}`);
    }
    public insertBook(obj, file) {
        console.log('Books/InsertBook');

        let formdata: FormData = new FormData();
        formdata.append("book", obj);
        //    formdata.append("image",file);
        return this.http.post(`${BookUrl}InsertBook`, obj);
        //return this.http.post(`${BookUrl}InsertBook`, formdata);
    }
    public addBookToMybasket(obj) {

        return this.http.post(`${BookUrl}addBookToMybasket`, obj);

    }
    public getAllMyBasket(idU): Observable<Books[]> {

        return this.http.get<Books[]>(`${BookUrl}getAllMyBasket?idU=${idU}`);

    }
    public getAllUsersBooksByIDU(idu: number): Observable<Books[]> {
        return this.http.get<Books[]>(`${BookUrl}getAllUsersBooksByIDU?idu=${idu}`);
    }

    public editBook(obj) {
        return this.http.post(`${BookUrl}editBook`, obj);

    }
    public getFile(imageName): Observable<Blob> {
        return this.http.get(`${BookUrl}getFile?imageName=${imageName}`, { responseType: 'blob' });

    }
    getImage2(imageName: string): Observable<Blob> {
        return this.http.get(`${BookUrl}getFile?imageName=${imageName}`, { responseType: 'blob' });
    }
    getImage(imageName: string): Observable<Blob> {
        return this.http.get(`http://localhost:56996/Images/${imageName}`, { responseType: 'blob' });
    }
    deleteBookFromMyBuskate(idB: number, idU: number) {
        let headers = new HttpHeaders({
            'Content-Type': 'application/json'
        });
        let options = { headers: headers };
        return this.http.get(`${BookUrl}deleteBookFromMyBuskate?idB=${idB}&idU=${idU}`)
    }
    public PromoteNumberOfViewers(idB) {
        return this.http.get(`${BookUrl}PromoteNumberOfViewers?idBook=${idB}`);
    }

    public rateBook(idB,desc,rate) {
        return this.http.get(`${BookUrl}rateBook?idB=${idB}&desc=${desc}&rate=${rate}`)
    }


}
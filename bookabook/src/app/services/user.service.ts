import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { user } from '../models/user';
import { Observable } from 'rxjs';
import { Books } from '../models/books';
const userUrl = "http://localhost:56996/api/Users/";
@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private httpService: HttpClient) { }
  //רישום של יוזר חדש
  insertUser(obj) {
    let headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    let options = { headers: headers };
    return this.httpService.post(userUrl + `InsertUser`, obj);
  }
  //הספרים האהובים שלי
  public getAllmyBasketBooks(idU) {
    return this.httpService.get(userUrl + `getAllmyBasketBooks?idu=` + idU);
  }
  //עריכת הפרטים שלי
  public editUserDetails(obj) {
    let headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });
    let options = { headers: headers };
    return this.httpService.post(userUrl + `EditUser`, obj);
  }
  //שליפת הפרטים שלי
  public getUserById(id: number): Observable<user> {
    return this.httpService.get<user>(`${userUrl}GetUserById?idUser=${id}`);
  }
  //הספרים שהעלתי לאתר
  public getAllUsersBooks(id: number): Observable<Books[]> {
    return this.httpService.get<Books[]>(`${userUrl}getAllUsersBooks?idUser=${id}`);
  }

  public getBooksUserDidntReturnById(uId:number)
  {
    return this.httpService.get<Books[]>(`${userUrl}getBooksUserDidntReturnById?idUser=${uId}`);
  }

}

import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from 'rxjs';
import { lends } from '../models/lends';
import { Books } from '../models/books';

const LenderUrl = "http://localhost:56996/api/Lender/";
@Injectable({
  providedIn: 'root'
})


export class LendsService {
//
  constructor(private http: HttpClient) { }
//בקשה להשאלת ספר
  public borrowBook(idB, idU): Observable<boolean> {
    return this.http.get<boolean>(`${LenderUrl}borrowBook?idB=${idB}&idU=${idU}`);
    // return this.http.post(`${LenderUrl}borrowBook`, idB,idU);
  }
//עדכן אותי כשהספר מתפנה 
  public updateWhenBookIsAvailable(idB, idU) {
    return this.http.get(`${LenderUrl}updateWhenBookIsAvailable?idB=${idB}&idU=${idU}`);
    // return this.http.post(`${LenderUrl}borrowBook`, idB,idU);
  }
  //אישור השאלה - ישלחו הפרטים שלי  למבקש 
  public confirmBorrow(idBorrow: number) {
    return this.http.get(`${LenderUrl}confirmBorrow?idBorrow=${idBorrow}`);

  }
  //דחית ספר - אני לא מוכן להשאיל אותו
  public rejectBorrow(idBorrow: number) {
    return this.http.get(`${LenderUrl}rejectBorrow?idBorrow=${idBorrow}`);

  }
  //כל הספרים שמחכים שנאי יאשר את ההשאלה שלהם
  public getAllWaitingForApprovalByUser(id:number):Observable<lends[]>{
    return this.http.get<lends[]>(`${LenderUrl}getAllWaitingForApprovalByUser?idU=${id}`);
  }
//הספרים שאני מחכה שיאשרו לי את ההשאלה
  public getBooksUserWantedToBorrow(id:number):Observable<lends[]>{
    return this.http.get<lends[]>(`${LenderUrl}getBooksUserWantedToBorrow?idU=${id}`);
  }
//ספרים שאני שואל אותם - הם אצלי
  public getBorrowedBookByUser(id:number):Observable<lends[]>{
    return this.http.get<lends[]>(`${LenderUrl}getBorrowedBookByUser?idU=${id}`);
  }
//החזרת ספר
  public returnBook(idB:number):Observable<boolean>{
    return this.http.get<boolean>(`${LenderUrl}returnBook?idB=${idB}`);
  }

  //ספרים שאני שואל אותם - הם אצלי
  public getAllmyBooksThatBorrowed(id:number):Observable<lends[]>{
    return this.http.get<lends[]>(`${LenderUrl}getAllmyBooksThatBorrowed?idU=${id}`);
  }
  //   let headers = new HttpHeaders({
  //     'Content-Type': 'application/json'});
  // let options = { headers: headers };
  //   return this.http.post(`${LenderUrl}borrowBook`, idB,idU);
  // }
  // post(url,obj){
  //   let headers = new HttpHeaders({
  //       'Content-Type': 'application/json'});
  //   let options = { headers: headers };
  //   return this.httpService.post(`http://localhost:56996/api/${url}`,obj,options);   
  // }
  public getBooksUserDidntReturn(id:number):Observable<lends[]>{
    return this.http.get<lends[]>(`${LenderUrl}getBooksUserDidntReturn?idU=${id}`);
  }
}

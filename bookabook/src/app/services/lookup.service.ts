import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Lookup } from '../models/lookup';
import { Observable } from 'rxjs';
import { bookName } from '../models/booksName';
const LookupUrl = "http://localhost:56996/api/Lookup/";
@Injectable({
  providedIn: 'root'
})
export class LookupService {

  constructor(private http: HttpClient) { }

 public getAllAuthers(): Observable<Lookup[]> {
    return this.http.get<Lookup[]>(`${LookupUrl}getAllAuther`);
  }
public  getGenderList():Observable<Lookup[]>{
    return this.http.get<Lookup[]>(`${LookupUrl}getGenderList`);
}
public  getAllPublishing():Observable<Lookup[]>{
    return this.http.get<Lookup[]>(`${LookupUrl}getAllPublishing`);
}
public  getAllCategory():Observable<Lookup[]>{
    return this.http.get<Lookup[]>(`${LookupUrl}getAllCategory`);
}
public  getAllBookName():Observable<bookName[]>{
    return this.http.get<bookName[]>(`${LookupUrl}getAllBookName`);
}
public  getAllKehalYaad():Observable<Lookup[]>{
  return this.http.get<Lookup[]>(`${LookupUrl}getAllKehalYaad`);
}

}

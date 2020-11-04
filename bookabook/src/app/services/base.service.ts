import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { Lookup } from "../models/lookup";
import { Login } from "../models/login";
import { user } from "../models/user";
import { constants } from "../constants";

const LookupUrl = constants.URL_BEGIN+"Lookup/";
const userUrl = constants.URL_BEGIN+"Users/";
@Injectable()
export class BaseService {
    activeUser:user;
    //
    loginFlag:Boolean=false;
    get isLogin() { return this.loginFlag; }

    constructor(private httpService: HttpClient) { }
    login(loginModel: Login):Observable<number> {
        let headers = new HttpHeaders({
            'Content-Type': 'application/json'
        });
        let options = { headers: headers };
        return this.httpService.post<number>(`${userUrl}Login`, loginModel, options)
    }
    logOut() {
        this.activeUser = undefined;
        this.loginFlag=false;
    }
    setActiveUser(login) {
        this.activeUser = login;
        this.loginFlag=true;

    }
    get(url) {
        return this.httpService.get(`http://localhost:56996/api/${url}`);
    }

    post(url, obj) {
        let headers = new HttpHeaders({
            'Content-Type': 'application/json'
        });
        let options = { headers: headers };
        return this.httpService.post(`http://localhost:56996/api/${url}`, obj, options);
    }


}
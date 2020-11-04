import{Lookup}from "../models/lookup"
import { user } from "./user";
import { Books } from "./books";
export class lends {
    id: number;
   // borrowerId: number;
   // bookId: number;
     borrower:user;
     book :Books;

    date: Date;
    bookIsActive: boolean;
    statusCode:number;
}
import { Lookup } from "../models/lookup"
import { bookName } from "./booksName";
export class BookToList {
    nameId: bookName;
    id: number;
   // categoryId: Lookup;
  //  publishingId: Lookup;
 //   description: string;
    isBorrowed: boolean;
  //  lenderId: number;
    autherId: Lookup;
 //   numOfPages: number
    picNAme: string;
    numberOfViewers:number;
}

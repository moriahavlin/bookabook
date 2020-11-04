import { Lookup } from "../models/lookup"
import { bookName } from "./booksName";
import { Statistics } from "./Statistics";
import { rating } from "./rating";
export class Books {
    nameId: bookName;
    id: number;
    categoryId: Lookup;
    publishingId: Lookup;
    description: string;
    isBorrowed: boolean;
    lenderId: number;
    autherId: Lookup;
    numOfPages: number
    picNAme: string;
    numberOfViewers: number;
    Statistics: Statistics;
    ratingList: rating[];

}

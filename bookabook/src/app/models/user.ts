import { Lookup } from "./lookup";

export class user {
    id: number;
    firstName: string;
    lastName: string;
    email: string;
    phone: string;
    cityCode: Lookup;
    address: string;
    houseNumber: number;
    neighborhood: string;
    genderId: Lookup;
    birthDate: Date;
    password: string;
}
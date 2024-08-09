import { RoleType } from "../enums";

export interface User {
    userId: string;
    roleName: RoleType;
    counterNumber: number;
    username: string;
    fullName: string;
    gender: string;
    phoneNumber: string;
    email: string;
    status: boolean;
    counterId: string;
}

export interface Role {
    roleId: string;
    roleName: string;
    users: any[];
}

export interface AddUser {
    roleId: string;
    username: string;
    fullName: string;
    gender: string;
    email: string;
    password: string;
}

export interface Token {
    token: string;
    expiration: string;
}

export interface TokenDecode {
    Id: string;
    nameid: string;
    sub: string;
    email: string;
    role: string;
    jti: string;
    nbf: number;
    exp: number;
    iat: number;
    iss: string;
    aud: string;
}

export interface JewelryType {
    jewelryTypeId: string;
    name: string;
    jewelries: any[];
}

export interface SignInRequest {
    email: string;
    password: string;
    counterId:string;
}

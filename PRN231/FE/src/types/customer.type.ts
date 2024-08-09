export interface Customer {
    customerId: string;
    userName: string;
    fullName: string;
    email: string;
    phone: string;
    gender: string;
    address: string;
    point: number;
}

export interface CreateCustomerRequest {
    userName: string;
    fullName: string;
    email: string;
    phone: string;
    gender: string;
    address: string;
    point: number;
}

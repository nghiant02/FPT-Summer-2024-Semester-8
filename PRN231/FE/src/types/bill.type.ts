import { JewelryID } from './jewelry.type';
import { PromotionBill, PromotionID } from './promotion.type';

export interface Bill {
    id: string;
    billId: string;
    customerName: string;
    staffName: string;
    totalAmount: number;
    totalDiscount: number;
    saleDate: string;
    items: Item[];
    promotions: PromotionBill[];
    additionalDiscount: number;
    pointsUsed: number;
    finalAmount: number;
}

export interface Item {
    jewelryId: string;
    name: string;
    jewelryPrice: number;
    laborCost: number;
    totalPrice: number;
}

export interface CreateBillRequest {
    customerId: string;
    userId: string;
    counterId: string;
    additionalDiscount: number;
    jewelries: JewelryID[];
    promotions: PromotionID[];
}

export interface CheckoutOfflineRequest {
    billId: string;
    cashAmount: number;
}

export interface CheckoutOfflineRespone {
    billId: string;
    customerName: string;
    initialAmount: number;
    cashBack: number;
    finalAmount: number;
    createdAt: string;
    status: string;
}

export interface CheckoutOnlineRespone {
    checkoutUrl: string
    billId: string
    orderCode: number
}
export interface CheckoutOnlineRequest {
    id: string,
    amount: number,
    returnUrl: string
}

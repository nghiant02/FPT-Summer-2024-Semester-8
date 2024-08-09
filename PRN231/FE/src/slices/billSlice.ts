import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { RootState } from '../store';
import { Bill, CheckoutOfflineRespone } from '../types/bill.type';

interface ShowBill {
    bill: Bill;
    isShow: boolean;
}

export interface billState {
    showBill: ShowBill;
    checkoutOffLineData: CheckoutOfflineRespone;
}

const initBill: Bill = {
    additionalDiscount: 0,
    billId: '',
    customerName: '',
    finalAmount: 0,
    id: '',
    items: [],
    pointsUsed: 0,
    promotions: [],
    saleDate: '',
    staffName: '',
    totalAmount: 0,
    totalDiscount: 0,
};

const initialState: billState = {
    showBill: {
        bill: initBill,
        isShow: false,
    },
    checkoutOffLineData: {
        billId: '',
        cashBack: 0,
        customerName: '',
        finalAmount: 0,
        initialAmount: 0,
        status: '',
        createdAt: '',
    },
};

export const billSlice = createSlice({
    name: 'bill',
    initialState,
    reducers: {
        setShowBill: (state, action: PayloadAction<ShowBill>) => {
            state.showBill = action.payload;
        },
        setIsShowBill: (state, action: PayloadAction<boolean>) => {
            state.showBill.isShow = action.payload;
        },
        setCheckoutOffLineData: (state, action: PayloadAction<CheckoutOfflineRespone>) => {
            state.checkoutOffLineData = action.payload;
        },
    },
});

export const selectBill = (state: RootState) => state.bill;

export const { setIsShowBill, setShowBill, setCheckoutOffLineData } = billSlice.actions;

export default billSlice.reducer;

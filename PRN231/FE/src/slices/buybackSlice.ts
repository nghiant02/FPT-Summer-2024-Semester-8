import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { Jewelry } from '../types/jewelry.type';
import { Bill } from '../types/bill.type';

export interface BuyBackState {
    buyBackMethod:number,
    jewelry:Jewelry|null
    showBill:boolean
    bill:Bill|null
    price: number
    billId:string
}


const initialState: BuyBackState = {
  buyBackMethod:0,
    jewelry:null,
    showBill:false,
    bill:null,
    price:0,
    billId:''
};

export const buyBackSlice = createSlice({
    name: 'buyBack',
    initialState,
    reducers: {
        setBuyBackMethod: (state, action: PayloadAction<number>) => {
            state.buyBackMethod = action.payload;
        },
        setJewelryBuyBack: (state, action: PayloadAction<Jewelry>) => {
            state.jewelry = action.payload;
        },
        setShowBuyBackBill:(state, action: PayloadAction<boolean>) => {
            state.showBill = action.payload;
        },
        setPrice:(state, action: PayloadAction<number>) => {
            state.price = action.payload;
        },
        setBuyBackBill:(state, action: PayloadAction<Bill>) => {
            state.bill = action.payload;
        },
        setBuyBackBillId:(state, action: PayloadAction<string>) => {
            state.billId = action.payload;
        },
        setClearBuyBack: (state)=>{
            state.buyBackMethod = 0;
            state.jewelry=null,
            state.showBill=false;
            state.bill=null;
            state.price=0;
            state.billId='';
        }
    },
});


export const {setBuyBackMethod,setJewelryBuyBack,setShowBuyBackBill,setPrice,setBuyBackBill,setBuyBackBillId ,setClearBuyBack} = buyBackSlice.actions;

export default buyBackSlice.reducer;

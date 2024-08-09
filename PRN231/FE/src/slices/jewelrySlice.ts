import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { RootState } from '../store';
import { Jewelry, JewelryID } from '../types/jewelry.type';
import { CartItem } from '../types/cart.type';

interface TempCart {
    totalPrice: number;
    discount: number;
    pay: number;
}

interface TermPromotion {
    discountRate: number;
    promotionId: string;
}

interface TempBill {
    customerId: string;
    userId: string;
    counterId: string;
    additionalDiscount: number;
    jewelries: JewelryID[];
    promotions: TermPromotion[];
}

export interface JewelryState {
    selectedItems: Jewelry[];
    cart: CartItem[];
    bill: TempBill;
    tempCart: TempCart;
    promotionsSelected: TermPromotion[];
    search: string;
    selectedPaymentMethod: number;
}

const initBill: TempBill = {
    additionalDiscount: 0,
    counterId: '',
    customerId: '',
    jewelries: [],
    promotions: [],
    userId: '',
};

const initTempCart: TempCart = {
    discount: 0,
    pay: 0,
    totalPrice: 0,
};

const initialState: JewelryState = {
    selectedItems: [],
    cart: [],
    bill: initBill,
    tempCart: initTempCart,
    promotionsSelected: [],
    search: '',
    selectedPaymentMethod: 0,
};

const callMoney = (state: JewelryState) => {
    let total = 0;
    state.cart.forEach((item) => (total += item.price));
    state.tempCart.totalPrice = total;
    state.tempCart.pay = total - state.tempCart.discount;
    let totalPromotion = 0;
    state.promotionsSelected.forEach((p) => (totalPromotion += p.discountRate));
    state.tempCart.discount = (state.tempCart.totalPrice * totalPromotion) / 100;
};

export const jewelrySlice = createSlice({
    name: 'jewelry',
    initialState,
    reducers: {
        toggleCart(state, action: PayloadAction<Jewelry>) {
            const foundIndex = state.cart.findIndex((i) => i.id == action.payload.jewelryId);
            if (foundIndex != -1) {
                state.cart.splice(foundIndex, 1);
            } else {
                const { name, jewelryId, totalPrice, type } = action.payload;
                const newCartItem: CartItem = {
                    id: jewelryId,
                    name: name,
                    price: totalPrice,
                    typeName: type,
                    sale: 0,
                };
                state.cart.push(newCartItem);
            }
            callMoney(state);
        },
        removeFromCart(state, action: PayloadAction<string>) {
            state.cart = state.cart.filter((item) => item.id !== action.payload);
            callMoney(state);
        },
        clearCart(state) {
            state.cart = [];
            state.tempCart = initTempCart;
        },
        clearBill(state) {
            state.bill = initBill;
            state.promotionsSelected = [];
            state.cart = [];
            state.tempCart = initTempCart;
            state.selectedPaymentMethod = 0;
        },
        callMoney,
        toggelPromotion(state, action: PayloadAction<TermPromotion>) {
            const index = state.promotionsSelected.findIndex(
                (p) => p.promotionId === action.payload.promotionId,
            );
            if (index >= 0) {
                state.promotionsSelected.splice(index, 1);
            } else {
                state.promotionsSelected.push(action.payload);
            }
        },
        clearPromotionSelected(state) {
            state.promotionsSelected = [];
        },
        savePromotionSelected(state) {
            state.bill.promotions = state.promotionsSelected;
            callMoney(state);
        },
        loadPromotionSelected(state) {
            state.promotionsSelected = state.bill.promotions;
        },
        setSearch(state, action: PayloadAction<string>) {
            state.search = action.payload;
        },
        setPaymentMethod(state, action: PayloadAction<number>) {
            state.selectedPaymentMethod = action.payload;
        },
    },
});

export const selectAuth = (state: RootState) => state.jewelry;

export const {
    toggleCart,
    clearCart,
    removeFromCart,
    toggelPromotion,
    clearPromotionSelected,
    savePromotionSelected,
    loadPromotionSelected,
    clearBill,
    setSearch,
    setPaymentMethod,
} = jewelrySlice.actions;

export default jewelrySlice.reducer;

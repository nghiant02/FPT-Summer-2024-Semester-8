import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { RootState } from '../store';
import { Customer } from '../types/customer.type';

export interface CustomerState {
    customer: Customer;
    showCustomerModal: boolean;
    tempPhone: string;
}
const initCustomer: Customer = {
    address: '',
    customerId: '',
    email: '',
    fullName: '',
    gender: '',
    phone: '',
    point: 0,
    userName: '',
};

const initialState: CustomerState = {
    customer: initCustomer,
    showCustomerModal: true,
    tempPhone: '',
};

export const customerState = createSlice({
    name: 'customer',
    initialState,
    reducers: {
        setCustomer: (state, action: PayloadAction<Customer>) => {
            state.customer = action.payload;
        },
        setShowCustomerModal: (state, action: PayloadAction<boolean>) => {
            state.showCustomerModal = action.payload;
        },
        clearCustomer: (state) => {
            state.tempPhone = state.customer.phone;
            state.customer = initCustomer;
        },
    },
});

export const selectCustomer = (state: RootState) => state.customer;

export const { setCustomer, setShowCustomerModal, clearCustomer } = customerState.actions;

export default customerState.reducer;

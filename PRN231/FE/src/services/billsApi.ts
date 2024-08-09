import {
    Bill,
    CheckoutOfflineRequest,
    CheckoutOfflineRespone,
    CheckoutOnlineRequest,
    CheckoutOnlineRespone,
    CreateBillRequest,
} from '../types/bill.type';
import { baseQueryWithReauth } from './baseApi';
import { createApi } from '@reduxjs/toolkit/query/react';

export const billApi = createApi({
    baseQuery: baseQueryWithReauth,
    tagTypes: ['bill', 'jewelry'],
    refetchOnMountOrArgChange: true,
    endpoints: (builder) => ({
        createBill: builder.mutation<Bill, CreateBillRequest>({
            query: (body) => ({
                url: `api/Bill/CreateBill`,
                method: 'POST',
                body,
            }),
        }),
        checkoutOffline: builder.mutation<CheckoutOfflineRespone, CheckoutOfflineRequest>({
            query: (para) => ({
                url: `api/Bill/CheckoutOffline/${para.billId}?cashAmount=${para.cashAmount}`,
                method: 'POST',
            }),
            invalidatesTags: ['jewelry'],
        }),
        checkoutOnline: builder.mutation<CheckoutOnlineRespone, CheckoutOnlineRequest>({
            query: (body) => ({
                url: `api/Bill/CheckoutOnline/${body.id}`,
                method: 'POST',
                body
            }),
        }),
        getBillBuyId: builder.query<Bill, string>({
            query: (para) => ({
                url: `api/Bill/GetBillById/${para}`,
                method: 'GET',
            }),
        }),
    }),
    reducerPath: 'billApi',
});

export default billApi;

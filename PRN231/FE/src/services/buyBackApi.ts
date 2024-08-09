import { BuyBackCountResponse, BuyBackResponse } from '../types/buyBack.type';
import { baseQueryWithReauth } from './baseApi';
import { createApi } from '@reduxjs/toolkit/query/react';

export const buyBackApi = createApi({
    baseQuery: baseQueryWithReauth,
    tagTypes: ['buyBack'],
    refetchOnMountOrArgChange: true,
    endpoints: (builder) => ({
        countBuyBackById: builder.mutation<BuyBackCountResponse, {
            jewelryId: string
          }>({
            query: (body) => ({
                url: `api/Purchase/CountBuyBackById`,
                method: 'POST',
                body
            }),
        }),
        buyBackById: builder.mutation<BuyBackResponse, {
            jewelryId: string
          }>({
            query: (body) => ({
                url: `api/Purchase/BuyBackById`,
                method: 'POST',
                body
            }),
        }),
    }),
    reducerPath: 'buyBackApi',
});

export default buyBackApi;

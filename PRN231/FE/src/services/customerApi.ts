import { CreateCustomerRequest, Customer } from '../types/customer.type';
import { baseQueryWithReauth } from './baseApi';
import { createApi } from '@reduxjs/toolkit/query/react';

export const customerApi = createApi({
    baseQuery: baseQueryWithReauth,
    tagTypes: ['customer'],
    refetchOnMountOrArgChange: true,
    endpoints: (builder) => ({
        createCustomer: builder.mutation<Customer, CreateCustomerRequest>({
            query: (body) => ({
                url: 'api/Customer/CreateCustomer',
                method: 'POST',
                body: body,
            }),
            invalidatesTags: ['customer'],
        }),
        findCustomerByPhone: builder.query<Customer, string>({
            query: (para) => ({
                url: `api/Customer/GetCustomerByPhone/${para}`,
                method: 'GET',
            }),
            providesTags: ['customer'],
        }),
    }),
    reducerPath: 'customerApi',
});

export default customerApi;

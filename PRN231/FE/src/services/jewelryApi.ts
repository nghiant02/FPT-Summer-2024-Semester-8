import { PaggingRequest, PaggingRespone } from '../types/base.type';
import { Jewelry } from '../types/jewelry.type';
import { JewelryType } from '../types/user.type';
import { baseQueryWithReauth } from './baseApi';
import { createApi } from '@reduxjs/toolkit/query/react';

export const jewelryApi = createApi({
    baseQuery: baseQueryWithReauth,
    tagTypes: ['jewelry'],
    refetchOnMountOrArgChange: true,
    endpoints: (builder) => ({
        getJewelries: builder.query<
            PaggingRespone<Jewelry>,
            PaggingRequest<{ jewelryTypeId: string; name: string }>
        >({
            query: (para) => ({
                url: `api/Jewelry/GetJewelries?pageNumber=${para.pageNumber}&pageSize=${para.pageSize}&name=${para.data.name}&typeId=${para.data.jewelryTypeId}`,
                method: 'GET',
            }),
            providesTags: ['jewelry'],
        }),
        getJewelryTypes: builder.query<JewelryType[], void>({
            query: () => ({
                url: `api/JewelryType/GetJewelryTypes`,
                method: 'GET',
            }),
        }),
        getJewelryById: builder.query<Jewelry, string>({
            query: (id) => ({
                url: `api/Jewelry/GetJewelryById/${id}`,
                method: 'GET',
            }),
        }),
    }),
    reducerPath: 'jewelryApi',
});

export default jewelryApi;

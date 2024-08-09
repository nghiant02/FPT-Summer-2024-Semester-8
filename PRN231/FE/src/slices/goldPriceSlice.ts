// slices/goldPriceSlice.ts
import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { RootState } from '../store';
import { GoldPrice } from '../types/goldPrice.type';

export interface GoldPriceState {
    goldPrices: GoldPrice[];
}

const initialState: GoldPriceState = {
    goldPrices: [],
};

const goldPriceSlice = createSlice({
    name: 'goldPrice',
    initialState,
    reducers: {
        setGoldPrices: (state, action: PayloadAction<GoldPrice[]>) => {
            state.goldPrices = action.payload;
        },
    }
});

export const selectGoldPrices = (state: RootState) => state.goldPrice.goldPrices;

export const { setGoldPrices } = goldPriceSlice.actions;

export default goldPriceSlice.reducer;

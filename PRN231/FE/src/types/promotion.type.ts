export interface PromotionBill {
    promotionId: string;
    discount: number;
}

export interface Promotion {
    promotionId: string;
    type: string;
    approveManager: any;
    description: string;
    discountRate: number;
    startDate: string;
    endDate: string;
    billPromotions: any[];
}

export interface PromotionID {
    promotionId: string;
}

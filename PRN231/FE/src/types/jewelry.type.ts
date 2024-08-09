export interface Jewelry {
    jewelryId: string;
    name: string;
    type: string;
    barcode: string;
    laborCost: number;
    jewelryPrice: number;
    imageUrl: any;
    materials: Material[];
    totalPrice: number;
    isSold: boolean;
}

export interface Material {
    gold: Gold;
    gem: Gem;
}

export interface Gold {
    goldType: string;
    goldQuantity: number;
    goldPrice: number;
}

export interface Gem {
    gem: string;
    gemQuantity: number;
    gemPrice: number;
}
export interface JewelryID {
    jewelryId: string;
}

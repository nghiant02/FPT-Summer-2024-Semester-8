import { PromotionType } from '../enums';

export const getPromotionTitle = (type: PromotionType | string) => {
    switch (type) {
        case 'Giam gia':
            return 'Giảm giá';
        default:
            return type;
    }
};

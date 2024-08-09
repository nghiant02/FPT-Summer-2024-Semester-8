import { useDispatch, useSelector } from 'react-redux';
import { Promotion } from '../types/promotion.type';
import { getPromotionTitle } from '../utils/enumsHelper';
import { RootState } from '../store';
import { FaCheckCircle } from 'react-icons/fa';
import { toggelPromotion } from '../slices/jewelrySlice';
interface PromotionItemProps {
    item: Promotion;
}

const PromotionItem = ({ item }: PromotionItemProps) => {
    const selectedPromotion = useSelector((state: RootState) => state.jewelry.promotionsSelected);
    const dispatch = useDispatch();
    return (
        <div
            onClick={() =>
                dispatch(
                    toggelPromotion({
                        discountRate: item.discountRate,
                        promotionId: item.promotionId,
                    }),
                )
            }
            className="relative flex cursor-pointer select-none flex-col justify-center rounded-sm border-[1px] border-[#B292EA] px-4 py-2 text-[#333]"
        >
            {selectedPromotion &&
                selectedPromotion.find((p) => p.promotionId === item.promotionId) && (
                    <div className="absolute right-0 top-0 rounded-full bg-white">
                        <FaCheckCircle className="text-primary" size={18} />
                    </div>
                )}
            <p className="text-center font-medium uppercase">{getPromotionTitle(item.type)}</p>
            <p className="text-center font-medium text-red-400">{item.discountRate}%</p>
        </div>
    );
};

export default PromotionItem;

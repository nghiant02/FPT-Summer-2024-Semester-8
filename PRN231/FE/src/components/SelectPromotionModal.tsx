import { Button, Modal } from 'antd';
import { useEffect, useState } from 'react';
import promotionApi from '../services/promotionApi';
import PromotionItem from './PromotionItem';
import { useDispatch } from 'react-redux';
import {
    clearPromotionSelected,
    loadPromotionSelected,
    savePromotionSelected,
} from '../slices/jewelrySlice';
interface SelectPromotionModalProps {
    title: string;
    childen: React.ReactNode;
}

const SelectPromotionModal = ({ childen, title }: SelectPromotionModalProps) => {
    //------------------------ call api get sales ---------------------//
    const { isLoading, isError, error, data } = promotionApi.useGetJewelriesQuery();
    const dispatch = useDispatch();
    useEffect(() => {
        if (isError) {
            console.log('error load promotion: ', error);
        }
    }, [isError]);

    //------------------------ end call api get sales ---------------------//
    const [open, setopen] = useState(false);
    return (
        <div>
            <div
                onClick={() => {
                    setopen(true);
                    dispatch(loadPromotionSelected());
                }}
            >
                {childen}
            </div>
            <Modal
                width={300}
                style={{ padding: 0 }}
                closable={false}
                footer={
                    <div>
                        <div className="h-[1px] w-full bg-gray" />
                        <div className="flex justify-center gap-4 p-2">
                            <Button
                                type="primary"
                                className="rounded-sm bg-primary px-5 py-1 !text-white"
                                onClick={() => {
                                    dispatch(savePromotionSelected());
                                    setopen(false);
                                }}
                            >
                                OK
                            </Button>
                            <Button
                                type="primary"
                                className="rounded-sm bg-red-400 px-5 py-1 !text-white"
                                onClick={() => {
                                    dispatch(clearPromotionSelected());
                                    setopen(false);
                                }}
                            >
                                Huỷ
                            </Button>
                        </div>
                    </div>
                }
                title={
                    <div className="bg-primary p-2">
                        <p className="w-full text-center text-lg uppercase text-white">{title}</p>
                    </div>
                }
                open={open}
                loading={isLoading}
                styles={{
                    content: {
                        padding: 0,
                    },
                }}
                onCancel={() => setopen(false)}
                className="min-w-fit"
            >
                {!isLoading && data && (
                    <div className="grid grid-cols-5 gap-3 p-4">
                        {data.map((item) => (
                            <PromotionItem key={item.promotionId} item={item} />
                        ))}
                    </div>
                )}
            </Modal>
        </div>
    );
};

export default SelectPromotionModal;

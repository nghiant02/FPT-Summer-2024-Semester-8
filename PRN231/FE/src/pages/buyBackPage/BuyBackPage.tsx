import SelectBuyBackOptionsModel from '../../components/SelectBuyBackOptionsModel';
import { useDispatch, useSelector } from 'react-redux';
import { RootState } from '../../store';
import CheckBuyBackModel from '../../components/CheckBuyBackModel';
import { Button, Popconfirm, Skeleton, Tag } from 'antd';
import buyBackApi from '../../services/buyBackApi';
import { toMoney } from '../../utils/formater';
import { useEffect } from 'react';
import {
    setBuyBackBillId,
    setClearBuyBack,
    setPrice,
    setShowBuyBackBill,
} from '../../slices/buybackSlice';
import ShowBillBuyBack from '../../components/ShowBillBuyBack';

const BuyBackPage = () => {
    const dispatch = useDispatch();
    const selectedBuyBackMethod = useSelector((state: RootState) => state.buyBack.buyBackMethod);
    const buyBakcJewelry = useSelector((state: RootState) => state.buyBack.jewelry);
    //------------------ call api get buy id ---------------------//
    const [countBuyBack, { isError, isLoading, data, isSuccess: isCountSuccess }] =
        buyBackApi.useCountBuyBackByIdMutation();
    const [
        BuyBack,
        { isError: isBuyBackError, isLoading: isBuyBackLoading, data: buyBackData, isSuccess },
    ] = buyBackApi.useBuyBackByIdMutation();

    useEffect(() => {
        if (isSuccess && buyBackData) {
            dispatch(setBuyBackBillId(buyBackData?.message.billId));
            dispatch(setShowBuyBackBill(true));
        }
    }, [isSuccess]);
    useEffect(() => {
        if (isCountSuccess && data) {
            dispatch(setPrice(data.message.totalPrice));
        }
    }, [isCountSuccess]);

    return (
        <div className="mt-1 flex flex-1 flex-col gap-2">
            <SelectBuyBackOptionsModel open={selectedBuyBackMethod == 0} />
            <CheckBuyBackModel open={selectedBuyBackMethod == 1 && buyBakcJewelry == null} />

            {buyBakcJewelry && (
                <div className="flex flex-1 gap-3 rounded-md">
                    <div className="flex bg-primary p-10">
                        <div className="flex flex-col gap-4">
                            <div className="h-[300px] w-[300px] rounded-md bg-white">
                                <img
                                    src={buyBakcJewelry.imageUrl}
                                    className="h-[300px] w-[300px] object-center"
                                />
                            </div>

                            <div className="mt-6 flex">
                                <Tag color="cyan" className="text-sm">
                                    Tên sản phẩm
                                </Tag>
                                <div className="border-[1px] border-green-OUTLINE px-2 font-medium text-white">
                                    {buyBakcJewelry.name}
                                </div>
                            </div>
                            <div className="flex">
                                <Tag color="cyan" className="text-sm">
                                    Loại
                                </Tag>
                                <div className="border-[1px] border-green-OUTLINE px-2 font-medium text-white">
                                    {buyBakcJewelry.type}
                                </div>
                            </div>
                            <div className="flex">
                                <Tag color="cyan" className="h-fit text-sm">
                                    Chất liệu
                                </Tag>
                                <div className="flex flex-col gap-1">
                                    <Tag color="gold" className="text-sm font-medium">
                                        Vàng {buyBakcJewelry?.materials[0]?.gold.goldType} (
                                        {buyBakcJewelry?.materials[0]?.gold.goldQuantity} gam)
                                    </Tag>
                                    <Tag color="green" className="text-sm font-medium">
                                        Đá {buyBakcJewelry?.materials[0]?.gem.gem} (
                                        {buyBakcJewelry?.materials[0]?.gem.gemQuantity} carat)
                                    </Tag>
                                </div>
                            </div>
                            <Button
                                type="primary"
                                className="mt-10 rounded-sm bg-secondary hover:!bg-secondary-LIGHT"
                                size="large"
                                onClick={() =>
                                    countBuyBack({ jewelryId: buyBakcJewelry.jewelryId })
                                }
                            >
                                Tính tiền mua lại
                            </Button>
                        </div>
                    </div>
                    <div className="flex flex-1 flex-col bg-white p-10">
                        <p className="mb-6 text-xl font-medium">Thông tin mua lại</p>
                        {isLoading && <Skeleton active />}
                        {!isLoading && isError && (
                            <div>
                                <p>Sản phẩm không đủ điều kiện để mua lại</p>
                                <Button
                                    type="primary"
                                    className="mt-10 rounded-sm bg-secondary hover:!bg-secondary-LIGHT"
                                    size="large"
                                    onClick={() => dispatch(setClearBuyBack())}
                                >
                                    Đóng
                                </Button>
                            </div>
                        )}
                        {!isLoading && data && data.message.totalPrice != -1 && (
                            <div>
                                <p>
                                    Sản phẩm được mua lại với mức giá{' '}
                                    <span className="font-medium text-red-400">
                                        {toMoney(data.message.totalPrice)}
                                    </span>
                                </p>
                                <Popconfirm
                                    title="Xác nhận mua lại"
                                    description="Bằng cách ấn xác nhận giao dịch sẽ được lưu trong hệ thống."
                                    onConfirm={() =>
                                        BuyBack({
                                            jewelryId: buyBakcJewelry
                                                ? buyBakcJewelry.jewelryId
                                                : '',
                                        })
                                    }
                                    onCancel={() => {}}
                                    okText="Xác nhận"
                                    cancelText="Không"
                                >
                                    <Button
                                        type="primary"
                                        className="mt-10 rounded-sm bg-secondary hover:!bg-secondary-LIGHT"
                                        size="large"
                                        loading={isBuyBackLoading}
                                    >
                                        Xác nhận mua lại
                                    </Button>
                                </Popconfirm>
                                <ShowBillBuyBack />
                            </div>
                        )}
                    </div>
                </div>
            )}
        </div>
    );
};

export default BuyBackPage;

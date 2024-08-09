import CustomerModel from './CustomModel';
import moneyPay from '../assets/moneyPay.jpg';
import qrPay from '../assets/qrpay.jpg';
import { Button, Input, message } from 'antd';
import { FaArrowRight } from 'react-icons/fa';
import { useEffect, useState } from 'react';
import { FaCheckCircle } from 'react-icons/fa';
import { useDispatch, useSelector } from 'react-redux';
import { RootState } from '../store';
import { setPaymentMethod } from '../slices/jewelrySlice';
import GetNumberModel from './GetNumberModel';
import { formatNumber } from '../utils/formater';
import { FaAngleLeft } from 'react-icons/fa6';
import { MdOutlinePayments } from 'react-icons/md';
import billApi from '../services/billsApi';
import { setCheckoutOffLineData, setIsShowBill } from '../slices/billSlice';
import OnlineCheckout from './OnlineCheckout';
import warrantyApi from '../services/warranty';
interface CheckoutModelProps {
    open: boolean;
    setOpen: React.Dispatch<React.SetStateAction<boolean>>;
}
const payList = [
    {
        id: 1,
        title: 'Tiền mặt',
        src: moneyPay,
    },
    {
        id: 2,
        title: 'Chuyển khoản',
        src: qrPay,
    },
];

const CheckoutModel = ({ open, setOpen }: CheckoutModelProps) => {
    const dispatch = useDispatch();
    const selectedPaymentMethod = useSelector(
        (state: RootState) => state.jewelry.selectedPaymentMethod,
    );
    const totalMoney = useSelector((state: RootState) => state.jewelry.tempCart.pay);
    const createBillResult = useSelector((state: RootState) => state.bill.showBill.bill);
    const [cash, setcash] = useState(0);
    const [messageApi, contextHolder] = message.useMessage();
    const [selectMethod, setselectMethod] = useState(0);

    //---------------------- call checkout offline api ----------------//

    const [Checkout, { isLoading, isError, isSuccess, data }] =
        billApi.useCheckoutOfflineMutation();

    const handleCheckoutOffline = () => {
        Checkout({ billId: createBillResult.billId, cashAmount: cash });
    };
    useEffect(() => {
        if (isSuccess) {
            dispatch(setCheckoutOffLineData(data));
            onOnlineSuccess();
        }
    }, [isSuccess]);

    useEffect(() => {
        if (isError) {
            console.log('error at checkout off line');
        }
    }, [isError]);

    const onOnlineSuccess = () => {
        createWarranties();
        dispatch(setIsShowBill(true));
        messageApi.success('Thanh toán thành công!');
        setOpen(false);
    };

    //------------------------- online checkout ----------------------//
    const [
        CheckoutOnl,
        { data: checkoutOnlData, isLoading: isCheckoutOnlLoading, isError: isCheckoutOnlError },
    ] = billApi.useCheckoutOnlineMutation();

    useEffect(() => {
        if (isCheckoutOnlError) {
            console.log('error at checkout online');
        }
    }, [isCheckoutOnlError]);

    //------------------------- call warranty ----------------------//
    const getEndWarranty = () => {
        const currentDate = new Date(createBillResult.saleDate);
        currentDate.setFullYear(currentDate.getFullYear() + 2);
        return currentDate.toISOString();
    };
    const [AddWarranty] = warrantyApi.useAddWarrantyMutation();

    const createWarranties = () => {
        const endDate = getEndWarranty();
        createBillResult.items.forEach((item) => {
            AddWarranty({ jewelryId: item.jewelryId, description: 'Warranty', endDate });
        });
    };

    return (
        <div>
            {contextHolder}
            <CustomerModel
                title={
                    selectedPaymentMethod == 0
                        ? 'Chọn phương thức thanh toán'
                        : 'Thông tin thanh toán'
                }
                open={open}
                body={
                    <div className="relative px-10 py-6">
                        {selectedPaymentMethod == 0 && (
                            <div>
                                <div className="flex gap-20">
                                    {payList.map((i) => (
                                        <div key={i.id} className="relative">
                                            <img
                                                src={i.src}
                                                className="h-[150px] max-h-[150px] min-h-[150px] w-[150px] cursor-pointer rounded-full object-cover"
                                                onClick={() => {
                                                    setselectMethod(i.id);
                                                }}
                                            />
                                            {selectMethod === i.id && (
                                                <div className="absolute right-0 top-0 rounded-full bg-white">
                                                    <FaCheckCircle
                                                        size={30}
                                                        className="text-secondary"
                                                    />
                                                </div>
                                            )}
                                            <p className="mt-4 select-none text-center text-base font-medium text-primary-TEXT">
                                                {i.title}
                                            </p>
                                        </div>
                                    ))}
                                </div>
                                <div className="flex gap-4">
                                    <Button
                                        className="mt-8 rounded-sm"
                                        type="default"
                                        size="large"
                                        onClick={() => setOpen(false)}
                                    >
                                        Huỷ
                                    </Button>
                                    <Button
                                        className="mt-8 w-full rounded-sm bg-secondary hover:!bg-secondary-LIGHT"
                                        type="primary"
                                        disabled={selectMethod == 0}
                                        onClick={() => {
                                            dispatch(setPaymentMethod(selectMethod));
                                            if (selectMethod == 2) {
                                                CheckoutOnl({
                                                    amount: createBillResult.totalAmount,
                                                    id: createBillResult.billId,
                                                    returnUrl: 'http://localhost:5173',
                                                });
                                            }
                                        }}
                                        size="large"
                                        icon={<FaArrowRight />}
                                        iconPosition="end"
                                    >
                                        Tiếp tục
                                    </Button>
                                </div>
                            </div>
                        )}
                        {selectedPaymentMethod == 1 && (
                            <div className="mt-6 flex max-w-[300px] flex-col gap-4">
                                <Button
                                    type="link"
                                    className="absolute -left-0 -top-0 text-primary-TEXT"
                                    icon={<FaAngleLeft />}
                                    onClick={() => dispatch(setPaymentMethod(0))}
                                >
                                    Quay lại
                                </Button>
                                <div>
                                    <p className="text-base font-medium uppercase text-primary-TEXT">
                                        Tổng tiền khách hàng phải trả
                                    </p>
                                    <p className="mt-1 text-base font-medium text-red-400">
                                        {formatNumber(totalMoney + '') + ' VND'}
                                    </p>
                                </div>
                                <div>
                                    <p className="mb-1 text-base font-medium uppercase text-primary-TEXT">
                                        Tiền nhận từ khách
                                    </p>
                                    <GetNumberModel
                                        childen={
                                            <Input
                                                size="large"
                                                className="rounded-sm border-green-OUTLINE"
                                                value={formatNumber(cash + '') + ' VND'}
                                            />
                                        }
                                        numberType="Float"
                                        value={cash}
                                        title="Tiền nhận từ khách hàng"
                                        onOK={(n) => setcash(n)}
                                    />
                                    {cash < totalMoney && (
                                        <p className="italic text-red-400">
                                            * Số tiền nhận từ khách hàng phải lớn hơn hoặc bằng số
                                            tiền phải trả!
                                        </p>
                                    )}
                                </div>
                                <Button
                                    type="primary"
                                    className="mt-6 w-full rounded-sm bg-secondary font-medium text-white hover:!bg-secondary-LIGHT"
                                    htmlType="submit"
                                    size="large"
                                    loading={isLoading}
                                    onClick={handleCheckoutOffline}
                                    icon={<MdOutlinePayments />}
                                >
                                    Thanh toán
                                </Button>
                            </div>
                        )}
                        {selectedPaymentMethod == 2 && (
                            <div className="mt-6 flex flex-col gap-4">
                                {/* <Button
                                    type="link"
                                    className="absolute -left-0 -top-0 text-primary-TEXT"
                                    icon={<FaAngleLeft />}
                                    onClick={() => dispatch(setPaymentMethod(0))}
                                >
                                    Quay lại
                                </Button> */}
                                <OnlineCheckout
                                    isLoading={isCheckoutOnlLoading}
                                    data={checkoutOnlData}
                                    onPaymentSuccess={onOnlineSuccess}
                                    onBackClick={() => {
                                        setOpen(false);
                                    }}
                                />
                            </div>
                        )}
                    </div>
                }
            />
        </div>
    );
};

export default CheckoutModel;

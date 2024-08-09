import { Button, message, Skeleton } from 'antd';
import { useState } from 'react';
import { CheckoutOnlineRespone } from '../types/bill.type';
import IframeComponent from './IframeComponent';
import failedAmin from '../assets/failed.json';
import Lottie from 'lottie-react';
import { useDispatch } from 'react-redux';
import { setPaymentMethod } from '../slices/jewelrySlice';

interface OnlineCheckoutProps {
    data?: CheckoutOnlineRespone;
    isLoading: boolean;
    onPaymentSuccess: () => void;
    onBackClick: () => void;
}

type Status = 'pending' | 'success' | 'failed';

function OnlineCheckout({ data, isLoading, onPaymentSuccess, onBackClick }: OnlineCheckoutProps) {
    const [paymentStatus, setPaymentStatus] = useState<Status>('pending');
    const [messageApi, contextHolder] = message.useMessage();
    const dispatch = useDispatch();
    const onUrlChange = (url: string) => {
        if (url.includes('success')) {
            setPaymentStatus('success');
            onPaymentSuccess();
        } else if (url.includes('canceled')) {
            setPaymentStatus('failed');
            messageApi.error('Thanh toán thất bại');
        }
    };

    return (
        <div>
            {contextHolder}
            {isLoading && <Skeleton active />}
            {!isLoading && data && paymentStatus === 'pending' && (
                <IframeComponent url={data.checkoutUrl} onUrlChange={onUrlChange} />
            )}
            {paymentStatus == 'failed' && (
                <div className="flex flex-col items-center justify-center">
                    <Lottie
                        animationData={failedAmin}
                        loop={false}
                        className="w-full max-w-[200px]"
                    />
                    <p className="text-center text-lg font-medium text-secondary">
                        Thanh toán thất bại!
                    </p>
                    <Button
                        type="primary"
                        className="mt-7 rounded-sm bg-secondary hover:!bg-secondary-LIGHT"
                        size="large"
                        onClick={() => {
                            onBackClick();
                            dispatch(setPaymentMethod(0));
                        }}
                    >
                        Đóng
                    </Button>
                </div>
            )}
        </div>
    );
}

export default OnlineCheckout;

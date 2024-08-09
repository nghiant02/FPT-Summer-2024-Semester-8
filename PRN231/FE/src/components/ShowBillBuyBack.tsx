import { Button, Modal, Skeleton } from 'antd';
import { IoPrintSharp } from 'react-icons/io5';
import { useEffect, useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { RootState } from '../store';
import { setClearBuyBack, setShowBuyBackBill } from '../slices/buybackSlice';
import PrintBuyBackInVoice from './PrintBuyBackInVoice';
import billApi from '../services/billsApi';

const ShowBillBuyBack = () => {
    const dispatch = useDispatch();
    const show = useSelector((state: RootState) => state.buyBack.showBill);
    const [isPrint, setisPrint] = useState(false);
    const handleDone = () => {
        dispatch(setClearBuyBack());
        dispatch(setShowBuyBackBill(false));
    };
    const billId = useSelector((state: RootState) => state.buyBack.billId);
    const { currentData, isFetching } = billApi.useGetBillBuyIdQuery(billId);
    return (
        <Modal
            width={300}
            style={{ padding: 0 }}
            closable={false}
            footer={false}
            title={
                <div className="bg-primary p-2">
                    <p className="w-full text-center text-lg uppercase text-white">
                        Thông tin hóa đơn
                    </p>
                </div>
            }
            open={show}
            styles={{
                content: {
                    padding: 0,
                },
                wrapper: {
                    maxHeight: '100vh',
                },
            }}
            className="min-w-[60%]"
        >
            <div className="mx-auto max-h-[75vh] max-w-[90%] overflow-y-auto">
                {isFetching && <Skeleton active />}
                {!isFetching && currentData && (
                    <PrintBuyBackInVoice bill={currentData} isPrint={isPrint} />
                )}
            </div>
            <div className="flex justify-end gap-8 p-4">
                <Button
                    className="rounded-sm bg-secondary text-white hover:!border-secondary hover:!text-secondary"
                    size="large"
                    onClick={handleDone}
                >
                    Hoàn Thành
                </Button>
                <Button
                    className="rounded-sm bg-primary text-white"
                    size="large"
                    icon={<IoPrintSharp />}
                    iconPosition="end"
                    onClick={() => setisPrint(true)}
                >
                    In hóa đơn
                </Button>
            </div>
        </Modal>
    );
};

export default ShowBillBuyBack;

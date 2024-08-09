import { Button, Modal } from 'antd';
import { IoPrintSharp } from 'react-icons/io5';
import PrintInvoice from './PrintInvoice';
import { useState } from 'react';
import { useDispatch, useSelector } from 'react-redux';
import { RootState } from '../store';
import { setIsShowBill } from '../slices/billSlice';
import { clearBill, clearCart } from '../slices/jewelrySlice';
import { clearCustomer } from '../slices/customerSlice';

const ShowBillModel = () => {
    const dispatch = useDispatch();
    const show = useSelector((state: RootState) => state.bill.showBill.isShow);
    const [isPrint, setisPrint] = useState(false);
    const handleDone = () => {
        dispatch(clearBill());
        dispatch(clearCart());
        dispatch(clearCustomer());
        dispatch(setIsShowBill(false));
    };
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
                <PrintInvoice isPrint={isPrint} />
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

export default ShowBillModel;

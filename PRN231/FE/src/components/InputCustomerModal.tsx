import { Button, Empty, Form, Input, Modal } from 'antd';
import { useDispatch, useSelector } from 'react-redux';
import { RootState } from '../store';
import { useEffect, useState } from 'react';
import { FaUserPlus } from 'react-icons/fa';
import CustomerInfo from './CustomerInfo';
import customerApi from '../services/customerApi';
import { clearCustomer, setCustomer, setShowCustomerModal } from '../slices/customerSlice';
import { FaArrowRightLong } from 'react-icons/fa6';
import { FaAngleLeft } from 'react-icons/fa';

interface InputCustomerModalProps {
    title: string;
}

const InputCustomerModal = ({ title }: InputCustomerModalProps) => {
    const dispatch = useDispatch();
    const open = useSelector((state: RootState) => state.customer.showCustomerModal);
    const customer = useSelector((state: RootState) => state.customer.customer);
    const [isNotFount, setisNotFount] = useState(false);
    const [searchPhone, setsearchPhone] = useState('');
    const [isCreateNew, setisCreateNew] = useState(false);
    const tempPhone = useSelector((state: RootState) => state.customer.tempPhone);
    const initValue = {
        phone: tempPhone,
    };
    // -------------------------------- call api find customer by phone ----------------------------//
    const { currentData, isError, isLoading, isSuccess } =
        customerApi.useFindCustomerByPhoneQuery(searchPhone);

    useEffect(() => {
        if (isSuccess && currentData) {
            dispatch(setCustomer(currentData));
        }
    }, [isSuccess, currentData]);

    useEffect(() => {
        if (isError) {
            setisNotFount(true);
        }
    }, [isError]);

    // -------------------------------- end call api find customer by phone ----------------------------//

    return (
        <div>
            <Modal
                footer={false}
                width={300}
                style={{ padding: 0 }}
                closable={false}
                title={
                    <div className="bg-primary p-2">
                        <p className="w-full text-center text-lg uppercase text-white">{title}</p>
                    </div>
                }
                open={open}
                styles={{
                    content: {
                        padding: 0,
                    },
                }}
                onCancel={() => dispatch(setShowCustomerModal(false))}
                className="min-w-fit"
            >
                <div className="w-[400px] max-w-[400px] p-6">
                    {/* show find customer dialog when slice dont have data */}
                    {!isCreateNew && customer && customer.customerId.length == 0 && (
                        <div>
                            <Form
                                initialValues={initValue}
                                onFinish={(v) => {
                                    if (currentData && currentData.phone == v.phone) {
                                        dispatch(setCustomer(currentData));
                                    } else {
                                        setsearchPhone(v.phone);
                                    }
                                }}
                                className="flex gap-2"
                            >
                                <Form.Item name={'phone'} className="w-full">
                                    <Input
                                        style={{ borderRadius: 2 }}
                                        className="border-green-OUTLINE"
                                        placeholder="Nhập số điện thoại"
                                        type="number"
                                        onFocus={() => setisNotFount(false)}
                                    />
                                </Form.Item>

                                <Button
                                    type="primary"
                                    className="rounded-sm bg-primary text-white"
                                    htmlType="submit"
                                    loading={isLoading}
                                >
                                    Tìm kiếm
                                </Button>
                            </Form>
                            {/* search not found */}
                            {isNotFount && (
                                <div className="mt-5">
                                    <Empty
                                        imageStyle={{ height: 100 }}
                                        description={
                                            <p>
                                                Không tìm thấy thông tin khách hàng
                                                <b className="font-medium">{' ' + searchPhone}</b>
                                            </p>
                                        }
                                    />
                                </div>
                            )}
                            <Button
                                type="primary"
                                size="middle"
                                className="mt-5 w-full rounded-sm bg-secondary text-white hover:!bg-secondary-LIGHT"
                                icon={<FaUserPlus />}
                                onClick={() => setisCreateNew(true)}
                            >
                                <p>Tạo mới</p>
                            </Button>
                        </div>
                    )}
                    {customer && customer.customerId.length > 0 && (
                        <div className="relative mt-2">
                            <Button
                                type="link"
                                className="absolute -left-5 -top-8"
                                icon={<FaAngleLeft />}
                                onClick={() => {
                                    dispatch(clearCustomer());
                                    setisCreateNew(false);
                                }}
                            >
                                Thay đổi
                            </Button>
                            <CustomerInfo mode={'View'} value={customer} />
                            <Button
                                iconPosition="end"
                                onClick={() => dispatch(setShowCustomerModal(false))}
                                type="primary"
                                size="large"
                                className="mt-5 w-full rounded-sm bg-secondary font-medium text-white hover:!bg-secondary-LIGHT hover:tracking-wider"
                                icon={<FaArrowRightLong />}
                            >
                                Tiếp tục thanh toán
                            </Button>
                        </div>
                    )}
                    {isCreateNew && customer && customer.customerId.length == 0 && (
                        <div className="relative mt-4">
                            <Button
                                type="link"
                                className="absolute -left-5 -top-10"
                                icon={<FaAngleLeft />}
                                onClick={() => setisCreateNew(false)}
                            >
                                Quay lại
                            </Button>
                            <CustomerInfo mode={'Create'} />
                        </div>
                    )}
                </div>
            </Modal>
        </div>
    );
};

export default InputCustomerModal;

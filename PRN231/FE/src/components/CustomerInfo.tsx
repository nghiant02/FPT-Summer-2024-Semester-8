import { Button, Form, Input, Tag } from 'antd';
import { CreateCustomerRequest, Customer } from '../types/customer.type';
import { useEffect } from 'react';
import customerApi from '../services/customerApi';
import { useDispatch } from 'react-redux';
import { setCustomer } from '../slices/customerSlice';
import { Mode } from '../types/base.type';

interface CustomerInfoProps {
    mode: Mode;
    value?: Customer;
}

const initFormData: CreateCustomerRequest = {
    address: '',
    email: '',
    fullName: '',
    gender: '',
    phone: '',
    point: 0,
    userName: '',
};

const customizeRequiredMark = (label: React.ReactNode, { required }: { required: boolean }) => (
    <>
        {required ? <Tag color="cyan-inverse">Bắt buộc</Tag> : <Tag color="green">Tùy chọn</Tag>}
        {label}
    </>
);

const CustomerInfo = ({ mode, value }: CustomerInfoProps) => {
    const dispatch = useDispatch();
    //------------------------- call api add new customer -----------------------------//

    const [AddCustomer, { isError, isLoading, isSuccess, error, data }] =
        customerApi.useCreateCustomerMutation();

    useEffect(() => {
        if (isSuccess && data) {
            dispatch(setCustomer(data));
        }
    }, [isSuccess]);

    useEffect(() => {
        if (isError) {
            console.log(error);
        }
    }, [isError]);

    //------------------------- end call api add new customer -----------------------------//

    const onSubmit = (values: CreateCustomerRequest) => {
        AddCustomer(values);
    };

    return (
        <div className="text-[#333]">
            {mode == 'View' && (
                <div>
                    <div className="flex justify-between gap-2">
                        <p className="text-base">
                            <b className="font-medium">Họ và tên:</b>
                        </p>
                        <p className="text-base font-bold capitalize text-primary-TEXT">
                            {value?.fullName}
                        </p>
                    </div>
                    <div className="flex justify-between gap-2">
                        <p className="text-base">
                            <b className="font-medium">Số điện thoại:</b>
                        </p>
                        <p className="text-base font-bold text-secondary-DARK">{value?.phone}</p>
                    </div>
                    <div className="flex justify-between gap-2">
                        <p className="text-base">
                            <b className="font-medium">Điểm tích lũy:</b>
                        </p>
                        <p className="text-base font-bold text-red-400">
                            {value?.point ?? 0} Points
                        </p>
                    </div>
                    <div className="flex justify-between gap-2 text-base">
                        <b className="font-medium">Địa chỉ: </b>
                        <p>{value?.address}</p>
                    </div>
                </div>
            )}
            {mode == 'Create' && (
                <div className="mt-5">
                    <Form
                        requiredMark={customizeRequiredMark}
                        initialValues={initFormData}
                        onFinish={onSubmit}
                        layout="vertical"
                    >
                        <Form.Item
                            label={<b className="font-medium">Họ và tên</b>}
                            name="fullName"
                            rules={[
                                { required: true, message: 'Vui lòng không để trống trường này!' },
                            ]}
                        >
                            <Input
                                className="rounded-sm border-green-OUTLINE"
                                placeholder="Nguyễn Văn A"
                            />
                        </Form.Item>
                        <Form.Item
                            name="phone"
                            label={<b className="font-medium">Số điện thoại</b>}
                            rules={[
                                { required: true, message: 'Vui lòng không để trống trường này!' },
                                { pattern: /^\d{10,11}$/, message: 'Số điện thoại không hợp lệ!' },
                            ]}
                        >
                            <Input
                                className="rounded-sm border-green-OUTLINE"
                                placeholder="12345678910"
                            />
                        </Form.Item>
                        <Form.Item
                            name="address"
                            label={<b className="font-medium">Địa chỉ</b>}
                            rules={[
                                { required: true, message: 'Vui lòng không để trống trường này!' },
                            ]}
                        >
                            <Input
                                className="rounded-sm border-green-OUTLINE"
                                placeholder="Nhập địa chỉ"
                            />
                        </Form.Item>
                        <Button
                            className="w-full rounded-sm bg-secondary hover:!bg-secondary-LIGHT"
                            type="primary"
                            htmlType="submit"
                            loading={isLoading}
                        >
                            Tạo hồ sơ khách hàng
                        </Button>
                    </Form>
                </div>
            )}
        </div>
    );
};

export default CustomerInfo;

import { Button, Dropdown, Input, MenuProps, Space } from 'antd';
import { FaUser } from 'react-icons/fa';
import { FaArrowRight } from 'react-icons/fa6';
import { FaUnlock } from 'react-icons/fa';
import { useEffect, useState } from 'react';
import { SignInRequest, TokenDecode } from '../../types/user.type';
import accountApi from '../../services/accountApi';
import { isValidateEmail } from '../../utils/validation';
import { useNavigate } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { setToken } from '../../slices/authSlice';

import counterApi from '../../services/counterApi';
import { ItemType } from 'antd/es/menu/interface';
import { DownOutlined } from '@ant-design/icons';

const LoginPage = () => {
    const [loginForm, setloginForm] = useState<SignInRequest>({
        email: '',
        password: '',
        counterId: '',
    });
    const [error, seterror] = useState('');
    const navigate = useNavigate();
    const dispatch = useDispatch();
    //-------------------------- handle call call api signin --------------------------------//
    const [SignIn, { isLoading, isSuccess, isError, data, error: errorRequest }] =
        accountApi.useSignInMutation();

    useEffect(() => {
        if (isSuccess && data) {
            dispatch(setToken(data.token));
            navigate('/manager/selling');
        }
    }, [isSuccess]);

    useEffect(() => {
        if (isError) {
            seterror('Địa chỉ email hoặc mật khẩu không đúng!');
            console.log(errorRequest);
        }
    }, [isError]);

    const handleSignIn = () => {
        if (!isValidateEmail(loginForm.email)) {
            seterror('Địa chỉ email không hợp lệ!');
        } else if (loginForm.password.length == 0) {
            seterror('mật khẩu không được để trống!');
        } else {
            console.log(loginForm);
            SignIn(loginForm);
        }
    };

    //-------------------------- end handle call call api signin --------------------------------//
    //-------------------------- handle call call api get counter --------------------------------//
    const [countersList, setcountersList] = useState<ItemType[]>([]);
    const {
        data: counters,
        isLoading: isCountersLoading,
        isSuccess: isGetCounterSuccess,
    } = counterApi.useGetAvailableCountersQuery();

    useEffect(() => {
        if (isGetCounterSuccess && counters) {
            const items: MenuProps['items'] = counters.map((c) => ({
                key: c.id,
                label: 'Quầy số ' + c.number,
                onClick: () => handleSelectCounter(c.id, c.number),
            }));
            setcountersList(items);
        }
    }, [isGetCounterSuccess, counters]);

    //-------------------------- end handle call call api counter --------------------------------//

    const [selectedCounter, setSelectedCounter] = useState<string>('');
    const handleSelectCounter = (counterId: string, counterNumber: string) => {
        setloginForm((prevForm) => ({
            ...prevForm,
            counterId: counterId,
        }));
        setSelectedCounter('Quầy số ' + counterNumber);
    };
    return (
        <div>
            <div className="flex h-full min-h-screen flex-col items-center justify-center bg-primary">
                <div className="h-fit w-fit rounded-xl bg-white pb-8 pl-10 pr-10 pt-7">
                    <p className="mb-[20px] mt-[15px] text-2xl text-primary-TEXT">
                        ĐĂNG NHẬP HỆ THỐNG
                    </p>
                    <div className="flex flex-col gap-4">
                        <div>
                            <p className="text-sm font-semibold text-primary-TEXT">Địa chỉ email</p>
                            <div className="mt-1 flex w-full">
                                <div className="bg-primary p-2">
                                    <FaUser color="#fff" />
                                </div>
                                <Input
                                    style={{
                                        borderTopLeftRadius: 0,
                                        borderBottomLeftRadius: 0,
                                        borderColor: '#5DA19F',
                                    }}
                                    onFocus={() => seterror('')}
                                    inputMode="email"
                                    value={loginForm.email}
                                    onChange={(e) =>
                                        setloginForm({ ...loginForm, email: e.target.value })
                                    }
                                    className="flex-1"
                                    placeholder="Nhập địa chỉ email"
                                />
                            </div>
                        </div>
                        <div>
                            <p className="text-sm font-semibold text-primary-TEXT">Mật khẩu</p>
                            <div className="mt-1 flex w-full">
                                <div className="bg-primary p-2">
                                    <FaUnlock color="#fff" />
                                </div>
                                <Input.Password
                                    className="flex-1"
                                    onFocus={() => seterror('')}
                                    style={{
                                        borderTopLeftRadius: 0,
                                        borderBottomLeftRadius: 0,
                                        borderColor: '#5DA19F',
                                    }}
                                    value={loginForm.password}
                                    onChange={(e) =>
                                        setloginForm({ ...loginForm, password: e.target.value })
                                    }
                                    placeholder="Nhập mật khẩu"
                                />
                            </div>
                        </div>

                        {!isCountersLoading && counters && (
                            <div>
                                <Dropdown
                                    menu={{
                                        items: countersList,
                                        selectable: true,
                                    }}
                                >
                                    <Space>
                                        <p className="text-sm font-semibold text-primary-TEXT">
                                            {selectedCounter || 'Chọn quầy'}
                                        </p>
                                        <DownOutlined className="text-primary-TEXT" />
                                    </Space>
                                </Dropdown>
                            </div>
                        )}
                        <p className="text-sm text-red-500">{error}</p>

                        <div className="mt-4 flex justify-end">
                            <Button
                                onClick={handleSignIn}
                                loading={isLoading}
                                iconPosition="end"
                                className="!bg-secondary p-4 font-medium text-white hover:!border-secondary-B hover:!bg-secondary-B hover:!text-white"
                                icon={<FaArrowRight />}
                            >
                                Đăng nhập{' '}
                            </Button>
                        </div>
                    </div>
                </div>
                <p className="mt-4 text-sm text-white">
                    2024 © Jewelry sales system at the store WebApp
                </p>
            </div>
        </div>
    );
};

export default LoginPage;

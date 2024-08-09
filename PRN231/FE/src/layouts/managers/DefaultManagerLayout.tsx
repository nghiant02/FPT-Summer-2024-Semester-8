import React, { useEffect } from 'react';
import HeaderMenuDropDown from './HeaderMenuDropDown';
import { IoMdSettings } from 'react-icons/io';
import { FaShoppingCart, FaGift, FaCartArrowDown, FaDollarSign, FaUser } from 'react-icons/fa';
import { MdHomeWork, MdCategory } from 'react-icons/md';
import { GiGoldBar } from 'react-icons/gi';
import { useDispatch, useSelector } from 'react-redux';
import { persistor, RootState } from '../../store';
import { useLocation, useNavigate } from 'react-router-dom';
import { LayoutProps } from '../../routes';
import accountApi from '../../services/accountApi';
import { setUser } from '../../slices/authSlice';
import { RoleType } from '../../enums';
import { Button, Popover } from 'antd';

interface HeaderMenu {
    preIcon?: React.ReactNode;
    title: string;
    subMenu?: string[][];
    id: number;
    href: string;
}

const menus: HeaderMenu[] = [
    {
        title: 'Hệ thống',
        preIcon: <IoMdSettings />,
        id: 1,
        href: '',
    },
    {
        title: 'Danh mục',
        preIcon: <MdCategory />,
        id: 2,
        href: '',
    },
    {
        title: 'Kho hàng',
        preIcon: <MdHomeWork />,
        id: 3,
        href: '',
    },
    {
        title: 'Thu tiền',
        preIcon: <FaDollarSign />,
        id: 4,
        href: '',
    },
    {
        title: 'K.Mãi-VIP',
        preIcon: <FaGift />,
        id: 5,
        href: '',
    },
    {
        title: 'Mua lại',
        preIcon: <FaCartArrowDown />,
        id: 6,
        href: '/manager/buy-back',
    },
    {
        title: 'Bán hàng',
        preIcon: <FaShoppingCart />,
        id: 7,
        href: '/manager/selling',
    },
    {
        title: 'Giá Vàng',
        preIcon: <GiGoldBar />,
        id: 8,
        href: '/manager/gold-price',
    },
];

const DefaultManagerLayout = ({ childen, requireRole, whenRoleUnMatchNavTo }: LayoutProps) => {
    const navigate = useNavigate();
    const dispatch = useDispatch();
    const currentRole = useSelector((state: RootState) => state.auth.tokenDecode.role);
    const userName = useSelector((state: RootState) => state.auth.user.fullName);
    const userId = useSelector((state: RootState) => state.auth.tokenDecode.nameid);
    const location = useLocation();
    //-------------------------- handle call call api get user info --------------------------------//

    const {
        isSuccess: isGetUserSuccess,
        isError: isGetUserError,
        data: userData,
        error: getUserError,
    } = accountApi.useGetUserByIdQuery(userId);

    useEffect(() => {
        if (isGetUserSuccess && userData) {
            dispatch(setUser(userData));
        }
    }, [isGetUserSuccess]);

    useEffect(() => {
        if (isGetUserError) {
            console.log(getUserError);
        }
    }, [isGetUserError]);

    //-------------------------- end handle call call api get user info --------------------------------//

    const [Logout, { isSuccess, isLoading, isError, error }] = accountApi.useLogoutMutation();

    useEffect(() => {
        if (isSuccess) {
            localStorage.clear();
            persistor.purge().then(() => {
                window.location.href = '/login';
            });
        }
    }, [isSuccess]);

    useEffect(() => {
        if (isError) {
            console.log(error);
        }
    }, [isError]);

    useEffect(() => {
        if (!requireRole?.includes(currentRole as RoleType) && whenRoleUnMatchNavTo) {
            navigate(whenRoleUnMatchNavTo);
        }
    }, [currentRole, requireRole, whenRoleUnMatchNavTo, navigate]);

    return (
        <div className="flex min-h-screen w-full flex-col">
            <div className="sticky flex justify-between">
                <div className="flex gap-2">
                    {menus.map((menu) => (
                        <HeaderMenuDropDown
                            key={menu.id}
                            title={menu.title}
                            preIcon={menu.preIcon}
                            isSelect={location.pathname === menu.href}
                            onItemClick={() => {
                                navigate(menu.href);
                            }}
                        />
                    ))}
                </div>
                <div>
                    <Popover
                        content={
                            <div>
                                <Button
                                    loading={isLoading}
                                    type="dashed"
                                    className="border-white bg-red-400 !text-white hover:!border-red-400 hover:!text-red-500"
                                    onClick={() => Logout(userId)}
                                >
                                    Đăng xuất
                                </Button>
                            </div>
                        }
                        trigger="click"
                    >
                        <div className="flex h-full cursor-pointer items-center gap-2 px-4 text-base text-gray-600 hover:bg-gray-200">
                            <p className="h-fit">{userName}</p>
                            <FaUser />
                        </div>
                    </Popover>
                </div>
            </div>
            <div className="flex flex-1 flex-col bg-gray">{childen}</div>
        </div>
    );
};

export default DefaultManagerLayout;

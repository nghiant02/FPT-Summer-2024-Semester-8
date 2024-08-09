import { Button, Popconfirm, Table, TableProps, Tag, Tooltip } from 'antd';
import { FaClock } from 'react-icons/fa';
import { CartItem } from '../../types/cart.type';
import { useDispatch, useSelector } from 'react-redux';
import { RootState } from '../../store';
import { removeFromCart } from '../../slices/jewelrySlice';
import { toMoney } from '../../utils/formater';

import { MdDelete } from 'react-icons/md';
import SelectPromotionModal from '../../components/SelectPromotionModal';

const SelectedItems = () => {
    const cart = useSelector((state: RootState) => state.jewelry.cart);
    const tempCart = useSelector((state: RootState) => state.jewelry.tempCart);
    const counterNumber = useSelector((state: RootState) => state.auth.user.counterNumber);

    const dispatch = useDispatch();
    const getCurrentTime = () => {
        const options: Intl.DateTimeFormatOptions = {
            month: 'numeric',
            day: 'numeric',
            year: 'numeric',
            hour: '2-digit',
            minute: '2-digit',
            hour12: false,
        };

        return new Date().toLocaleString('vi', options);
    };

    const columns: TableProps<CartItem>['columns'] = [
        {
            title: 'STT',
            dataIndex: 'index',
            key: 'index',
            render: (_, __, index) => index + 1,
        },
        {
            title: 'Hàng hóa',
            dataIndex: 'name',
            key: 'name',
            render: (text) => (
                <Tooltip title={text}>
                    <p className="line-clamp-1 w-[120px] max-w-[120px] cursor-help overflow-x-auto">
                        {text}
                    </p>
                </Tooltip>
            ),
        },
        {
            title: 'Loại',
            key: 'typeName',
            render: (_, { typeName }) => <Tag color="cyan">{typeName}</Tag>,
        },
        {
            title: 'Giá',
            dataIndex: 'price',
            key: 'price',
            render: (_, { price }) => (
                <p className="text-sm font-medium text-red-400">{toMoney(price)}</p>
            ),
        },

        {
            title: 'Action',
            key: 'action',
            render: (_, { id, name }) => (
                <Popconfirm
                    title={'Xác nhận xóa'}
                    description={
                        <p>
                            Bạn có chắc muốn xóa <b>{name}</b>
                        </p>
                    }
                    okText="Xóa"
                    okType="danger"
                    cancelText="Hủy"
                    onConfirm={() => dispatch(removeFromCart(id))}
                >
                    <Button danger icon={<MdDelete size={18} />}></Button>
                </Popconfirm>
            ),
        },
    ];
    return (
        <div className="flex min-w-[600px] max-w-[600px] flex-col gap-2 bg-primary px-2 pt-1 text-white">
            <div className="flex justify-between">
                <p className="font-medium">Quầy {counterNumber} - Bán hàng</p>
                <div className="flex items-center gap-1">
                    <FaClock /> <p> Giờ: {getCurrentTime()}</p>
                </div>
            </div>
            <div className="flex justify-between">
                <div className="flex gap-4"></div>
                <div>
                    <div className="flex items-center gap-1">
                        <p className="text-lg font-medium">Phải trả: </p>
                        <p className="text-xl font-semibold">{toMoney(tempCart.pay)}</p>
                    </div>
                </div>
            </div>
            <div className="flex-1">
                <Table
                    columns={columns}
                    rowClassName={(_, index) => (index % 2 == 0 ? 'bg-[#f1faff]' : '')}
                    dataSource={cart}
                />
            </div>
            <div className="grid grid-cols-2 p-2">
                <div className="flex items-center justify-end gap-1">
                    <p className="text-lg">Tổng tiền:</p>
                    <div className="rounded-sm bg-white px-2 text-xl font-medium text-[#333]">
                        <p>{toMoney(tempCart.totalPrice)}</p>
                    </div>
                </div>
                <div className="flex cursor-pointer items-center justify-end gap-1">
                    <p className="text-lg">Giảm bill:</p>
                    <SelectPromotionModal
                        title="Điều chỉnh khuyến mãi"
                        childen={
                            <div className="rounded-sm bg-white px-2 text-xl font-medium text-[#333]">
                                <p>{toMoney(tempCart.discount)}</p>
                            </div>
                        }
                    />
                </div>
            </div>
        </div>
    );
};

export default SelectedItems;

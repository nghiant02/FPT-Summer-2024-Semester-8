import { LoadingOutlined } from '@ant-design/icons';
import { Spin } from 'antd';
import { FaAngleDown, FaAngleUp } from 'react-icons/fa6';

export type Direction = 'UP' | 'DOWN';
export type Orientation = 'Vertical' | 'Horizontal';

interface MenuItemProps {
    preIcon?: React.ReactNode;
    title: string;
    submenu?: React.ReactNode;
    isSelect?: boolean;
    onItemClick?: () => void;
    expendDirection?: Direction;
    orientation?: Orientation;
    containerStyle?: string;
    containerSelectedStyle?: string;
    textStyle?: string;
    isLoading?: boolean;
}
const MenuItem = ({
    title,
    isSelect,
    onItemClick,
    preIcon,
    submenu,
    expendDirection = 'DOWN',
    orientation = 'Horizontal',
    containerStyle = '',
    containerSelectedStyle = '',
    textStyle = '',
    isLoading,
}: MenuItemProps) => {
    return (
        <div
            className={
                'flex cursor-pointer items-center justify-center gap-1 px-2 py-1 text-sm text-gray-600 ' +
                (isSelect ? containerSelectedStyle : '') +
                (orientation === 'Vertical' ? ' flex-col' : '') +
                (' ' + containerStyle)
            }
            onClick={onItemClick}
        >
            {isLoading ? (
                <Spin
                    className="text-white"
                    indicator={<LoadingOutlined color="white" style={{ fontSize: 24 }} spin />}
                />
            ) : (
                preIcon
            )}
            <p className={textStyle}>{title}</p>
            {submenu && (expendDirection == 'DOWN' ? <FaAngleDown /> : <FaAngleUp />)}
        </div>
    );
};

export default MenuItem;

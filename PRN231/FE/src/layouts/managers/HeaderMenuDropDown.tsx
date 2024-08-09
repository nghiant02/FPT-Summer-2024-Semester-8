import React from 'react';
import { FaAngleDown } from 'react-icons/fa6';

export interface HeaderMenuDropDownProps {
    preIcon?: React.ReactNode;
    title: string;
    submenu?: React.ReactNode;
    isSelect?: boolean;
    onItemClick?: () => void;
}

const HeaderMenuDropDown = ({
    title,
    preIcon,
    submenu,
    isSelect,
    onItemClick,
}: HeaderMenuDropDownProps) => {
    return (
        <div
            className={
                'flex cursor-pointer items-center justify-center gap-1 px-3 py-2 text-sm text-gray-600 ' +
                (isSelect ? 'bg-secondary !text-white' : 'hover:bg-gray-200')
            }
            onClick={onItemClick}
        >
            {preIcon}
            <p className="">{title}</p>
            {submenu && <FaAngleDown />}
        </div>
    );
};

export default HeaderMenuDropDown;

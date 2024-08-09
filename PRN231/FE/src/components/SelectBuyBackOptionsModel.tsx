import { Button, Modal } from 'antd';
import market from '../assets/market.jpg';
import company from '../assets/logo.png';
import { useState } from 'react';
import { FaCheckCircle } from 'react-icons/fa';
import { FaArrowRight } from 'react-icons/fa6';
import { useDispatch, useSelector } from 'react-redux';
import { setBuyBackMethod } from '../slices/buybackSlice';
interface SelectBuyBackOptionsModelProps {
    open: boolean;
}

const method = [
    {
        id: 1,
        title: 'Sản phẩm của công ty',
        src: company,
    },
    {
        id: 2,
        title: 'Sản phẩm ngoài',
        src: market,
    },
];

const SelectBuyBackOptionsModel = ({ open }: SelectBuyBackOptionsModelProps) => {
    const dispatch = useDispatch();
    const [selectmethod, setselectmethod] = useState(0);

    return (
        <Modal
            width={300}
            style={{ padding: 0 }}
            closable={false}
            footer={false}
            title={
                <div className="bg-primary p-2">
                    <p className="w-full text-center text-lg uppercase text-white">
                        Chọn phương thức mua lại
                    </p>
                </div>
            }
            open={open}
            styles={{
                content: {
                    padding: 0,
                },
            }}
            className="min-w-fit"
        >
            <div className="p-6">
                <div className="flex gap-20">
                    {method.map((i) => (
                        <div key={i.id} className="relative">
                            <img
                                src={i.src}
                                className="h-[150px] max-h-[150px] min-h-[150px] w-[150px] cursor-pointer rounded-full border-[1px] object-cover"
                                onClick={() => {
                                    setselectmethod(i.id);
                                }}
                            />
                            {selectmethod === i.id && (
                                <div className="absolute right-0 top-0 rounded-full bg-white">
                                    <FaCheckCircle size={30} className="text-secondary" />
                                </div>
                            )}
                            <p className="mt-4 select-none text-center text-base font-medium text-primary-TEXT">
                                {i.title}
                            </p>
                        </div>
                    ))}
                </div>
                <div className="flex gap-4">
                    <Button
                        className="mt-8 w-full rounded-sm bg-secondary hover:!bg-secondary-LIGHT"
                        type="primary"
                        disabled={selectmethod == 0}
                        onClick={() => dispatch(setBuyBackMethod(selectmethod))}
                        size="large"
                        icon={<FaArrowRight />}
                        iconPosition="end"
                    >
                        Tiếp tục
                    </Button>
                </div>
            </div>
        </Modal>
    );
};

export default SelectBuyBackOptionsModel;

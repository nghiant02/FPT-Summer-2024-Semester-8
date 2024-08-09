import { Input, Modal } from 'antd';
import { useEffect, useState } from 'react';
import { formatNumber, parseFormattedNumber } from '../utils/formater';

type NumberType = 'Integer' | 'Float';

interface GetNumberModelProps {
    title: string;
    childen: React.ReactNode;
    value: number;
    onOK: (value: number) => void;
    numberType: NumberType;
}

const numbers = ['7', '8', '9', '4', '5', '6', '1', '2', '3', '0', '000'];

const GetNumberModel = ({ title, childen, onOK, value, numberType }: GetNumberModelProps) => {
    const [open, setopen] = useState(false);
    const [number, setnumber] = useState(value + '');
    useEffect(() => {
        setnumber(value + '');
    }, [value, open]);

    return (
        <div>
            <div onClick={() => setopen(true)}>{childen}</div>
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
                onCancel={() => setopen(false)}
                className="min-w-fit"
            >
                <div className="mt-4 flex select-none gap-2 p-4">
                    <div className="flex flex-col gap-2">
                        <Input
                            className="h-[45px] w-[265px] rounded-sm text-right text-3xl font-bold"
                            value={formatNumber(number)}
                            onChange={(e) => setnumber(e.target.value)}
                        />
                        <div className="grid w-[265] grid-cols-3 gap-2">
                            {numbers.map((n) => (
                                <div
                                    key={n}
                                    onClick={() => setnumber(number + n)}
                                    className="flex h-[45px] w-[80px] cursor-pointer items-center justify-center rounded-sm border-[1px] border-[#ccc] hover:bg-gray"
                                >
                                    <p className="select-none text-center text-2xl font-bold text-[#333]">
                                        {n}
                                    </p>
                                </div>
                            ))}
                            {numberType === 'Float' && (
                                <div
                                    onClick={() => {
                                        if (!number.includes('.')) setnumber(number + '.');
                                    }}
                                    className="flex h-[45px] w-[80px] cursor-pointer items-center justify-center rounded-sm border-[1px] border-[#ccc] hover:bg-gray"
                                >
                                    <p className="select-none text-center text-2xl font-bold text-[#333]">
                                        .
                                    </p>
                                </div>
                            )}
                        </div>
                    </div>
                    <div className="flex flex-col gap-2">
                        <div
                            onClick={() => setnumber('')}
                            className="flex h-[45px] w-[80px] cursor-pointer items-center justify-center rounded-sm border-[1px] border-[#ccc] hover:bg-gray"
                        >
                            <p className="select-none text-center text-2xl font-bold text-[#333]">
                                Xóa
                            </p>
                        </div>
                        <div
                            onClick={() => {
                                setopen(false), onOK(parseFormattedNumber(number));
                            }}
                            className="flex h-[99px] w-[80px] cursor-pointer items-center justify-center rounded-sm border-[1px] border-[#ccc] hover:bg-gray"
                        >
                            <p className="select-none text-center text-2xl font-bold text-[#333]">
                                OK
                            </p>
                        </div>
                        <div
                            onClick={() => {
                                setopen(false);
                            }}
                            className="flex h-[99px] w-[80px] cursor-pointer items-center justify-center rounded-sm border-[1px] border-[#ccc] hover:bg-gray"
                        >
                            <p className="select-none text-center text-2xl font-bold text-[#333]">
                                Hủy
                            </p>
                        </div>
                    </div>
                </div>
            </Modal>
        </div>
    );
};

export default GetNumberModel;

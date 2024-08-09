import { useEffect } from 'react';
import jewelryApi from '../../services/jewelryApi';
import { Skeleton } from 'antd';

interface ProverCategoryProps {
    selectedId: string;
    onSelect: (id: string) => void;
}

const ProverCategory = ({ onSelect, selectedId }: ProverCategoryProps) => {
    //-----------------------handle call get Jewelries type ---------------------------//
    const { isLoading, isError, error, data } = jewelryApi.useGetJewelryTypesQuery();

    useEffect(() => {
        if (isError) {
            console.log('error load types : ', error);
        }
    }, [isError]);
    //----------------------- end handle call get Jewelries type ---------------------------//
    return (
        <div>
            {!isLoading && data && (
                <div className="grid w-[400px] grid-cols-2 gap-2 px-2">
                    {data.map((type) => (
                        <p
                            onClick={() => onSelect(type.jewelryTypeId)}
                            key={type.jewelryTypeId}
                            className={
                                'cursor-pointer select-none rounded-sm border-[1px] py-2 text-center ' +
                                (selectedId == type.jewelryTypeId
                                    ? 'border-secondary bg-secondary text-white hover:bg-secondary-LIGHT'
                                    : 'border-[#ccc] hover:bg-gray')
                            }
                        >
                            {type.name}
                        </p>
                    ))}
                </div>
            )}
            {isLoading && (
                <div className="flex items-center justify-center">
                    <Skeleton active />
                </div>
            )}
        </div>
    );
};

export default ProverCategory;

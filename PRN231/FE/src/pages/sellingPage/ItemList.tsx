import { Empty, Input, Popover, Skeleton } from 'antd';
import { useCallback, useEffect, useState } from 'react';
import { FaTags } from 'react-icons/fa';
import { FaCaretDown } from 'react-icons/fa';
import ProverCategory from './ProverCategory';
import Item from '../../components/Item';
import jewelryApi from '../../services/jewelryApi';
import { Jewelry } from '../../types/jewelry.type';
import { PaggingRespone } from '../../types/base.type';
import { FaChevronCircleLeft } from 'react-icons/fa';
import { FaChevronCircleRight } from 'react-icons/fa';
import { useDispatch, useSelector } from 'react-redux';
import { setSearch, toggleCart } from '../../slices/jewelrySlice';
import { RootState } from '../../store';
import { debounce } from 'lodash';
import CounterList from './CounterList';

type SellingHeaderTab = 'Counters' | 'Jewelrys';
interface Tab {
    id: SellingHeaderTab;
    title: string;
}

const tabs: Tab[] = [
    {
        id: 'Counters',
        title: 'Quầy',
    },
    {
        id: 'Jewelrys',
        title: 'Trang sức',
    },
];

const ItemList = () => {
    const [selectedTab, setselectedTab] = useState<SellingHeaderTab>('Jewelrys');
    const [selectedType, setselectedType] = useState('');
    const dispatch = useDispatch();
    const [itemList, setitemList] = useState<PaggingRespone<Jewelry>>({
        data: [],
        pageNumber: 1,
        pageSize: 20,
        totalPage: 0,
        totalRecord: 0,
    });
    const search = useSelector((state: RootState) => state.jewelry.search);
    const [searchText, setSearchText] = useState('');
    //-----------------------handle call get Jewelries ---------------------------//
    const { data, isSuccess, isFetching, isError, error, refetch } =
        jewelryApi.useGetJewelriesQuery({
            pageNumber: itemList.pageNumber,
            pageSize: itemList.pageSize,
            data: { jewelryTypeId: selectedType, name: search },
        });

    useEffect(() => {
        if (isSuccess && data) {
            setitemList(data);
        }
    }, [isSuccess, data]);

    useEffect(() => {
        if (isError) {
            console.log('error load jewelries', error);
        }
    }, [isError]);

    //----------------------- end handle call get Jewelries ---------------------------//
    const debouncedSearch = useCallback(
        debounce((value) => {
            dispatch(setSearch(value));
        }, 500),
        [],
    );
    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setSearchText(e.currentTarget.value);
        debouncedSearch(e.currentTarget.value);
    };

    const goToNextPage = () => {
        setitemList({ ...itemList, pageNumber: itemList.pageNumber + 1 });
        refetch();
    };
    const goToPreviousPage = () => {
        setitemList({ ...itemList, pageNumber: itemList.pageNumber - 1 });
        refetch();
    };
    return (
        <div className="relative flex h-full flex-col">
            <div className="flex items-center">
                <div className="flex flex-1">
                    {tabs.map((t) => (
                        <p
                            onClick={() => setselectedTab(t.id)}
                            key={t.id}
                            className={
                                'cursor-pointer select-none px-3 py-2 text-base font-bold uppercase text-white hover:opacity-80 ' +
                                (selectedTab == t.id ? 'bg-secondary-DARK' : 'bg-primary')
                            }
                        >
                            {t.title}
                        </p>
                    ))}
                </div>
                <div className="flex-1">
                    <Input
                        style={{ borderRadius: 0, borderColor: '#5DA19F' }}
                        placeholder="Nhập tên hàng  để tìm kiếm"
                        value={searchText}
                        onChange={handleChange}
                    />
                </div>
            </div>
            <div className="flex h-9 items-center justify-end !bg-secondary-DARK py-[1px]">
                {selectedTab == tabs[1].id && (
                    <div className="flex h-full gap-2 text-white">
                        <div
                            onClick={() => setselectedType('')}
                            className={
                                'flex h-full cursor-pointer items-center px-2 hover:bg-black/10 ' +
                                (selectedType.length == 0 ? '!bg-white text-black' : '')
                            }
                        >
                            <p className="select-none">Tất cả hàng hóa</p>
                        </div>
                        <Popover
                            content={
                                <ProverCategory
                                    onSelect={(id) => setselectedType(id)}
                                    selectedId={selectedType}
                                />
                            }
                            showArrow
                            trigger="click"
                        >
                            <div
                                className={
                                    'flex h-full cursor-pointer items-center gap-1 px-2 hover:bg-black/10 ' +
                                    (selectedType.length > 0 ? '!bg-white text-black' : '')
                                }
                            >
                                <FaTags />
                                <p className="select-none">Nhóm hàng</p>
                                <FaCaretDown />
                            </div>
                        </Popover>
                    </div>
                )}
            </div>
            {selectedTab == tabs[1].id && (
                <div className="flex flex-wrap gap-2 p-2 pr-4">
                    {/* display items */}
                    {!isFetching &&
                        data &&
                        itemList.data.map((item, index) => (
                            <Item
                                onItemClick={() => dispatch(toggleCart(item))}
                                item={item}
                                key={index}
                            />
                        ))}

                    {!isFetching && itemList.data.length == 0 && (
                        <div className="m-auto">
                            <Empty description="Không tìm thấy sản phẩm nào." />
                        </div>
                    )}
                    {isFetching && (
                        <div className="flex flex-1 flex-col">
                            <Skeleton active />
                            <Skeleton active />
                            <Skeleton active />
                        </div>
                    )}
                    {/* display pagging  */}
                    {itemList.pageNumber > 1 && (
                        <div
                            onClick={goToPreviousPage}
                            className="absolute left-0 top-1/2 -translate-x-1/2 -translate-y-1/2 cursor-pointer rounded-full bg-white text-primary hover:text-sky-300"
                        >
                            <FaChevronCircleLeft size={50} />
                        </div>
                    )}
                    {itemList.pageNumber < itemList.totalPage && (
                        <div
                            onClick={goToNextPage}
                            className="absolute right-0 top-1/2 -translate-x-[10px] -translate-y-1/2 cursor-pointer rounded-full bg-white text-primary hover:text-sky-300"
                        >
                            <FaChevronCircleRight size={50} />
                        </div>
                    )}
                </div>
            )}
            {selectedTab == tabs[0].id && <CounterList />}
        </div>
    );
};

export default ItemList;

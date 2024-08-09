import { Skeleton } from 'antd';
import CounterItem from '../../components/CounterItem';
import counterApi from '../../services/counterApi';

const CounterList = () => {
    //
    const { isLoading, data } = counterApi.useGetAvailableCountersQuery();

    return (
        <div className="flex flex-wrap gap-2 p-2 pr-4">
            {isLoading && <Skeleton active />}
            {!isLoading && data && data.map((counter) => <CounterItem counter={counter} />)}
        </div>
    );
};

export default CounterList;

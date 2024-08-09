import { Counter } from '../types/counter.type';

interface CounterItemProps {
    counter: Counter;
}

const CounterItem = ({ counter }: CounterItemProps) => {
    return (
        <div className="rounded-sm border-[1px] border-green-OUTLINE">
            <div className="h-[30px] bg-gray-300"></div>
            <div className="px-4 pb-6 pt-4">
                <p className="text-xl font-medium text-primary-TEXT">Quầy {counter.counterId}</p>
            </div>
        </div>
    );
};

export default CounterItem;

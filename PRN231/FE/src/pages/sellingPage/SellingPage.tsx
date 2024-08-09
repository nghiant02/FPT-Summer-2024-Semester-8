import ItemList from './ItemList';
import SelectedItems from './SelectedItems';
import SellingPageFooter from './SellingPageFooter';

const SellingPage = () => {
    return (
        <div className="flex flex-1 gap-2">
            <SelectedItems />
            <div className="mt-1 flex flex-1 flex-col gap-2">
                <div className="flex-1 bg-white">
                    <ItemList />
                </div>
                <SellingPageFooter />
            </div>
        </div>
    );
};

export default SellingPage;

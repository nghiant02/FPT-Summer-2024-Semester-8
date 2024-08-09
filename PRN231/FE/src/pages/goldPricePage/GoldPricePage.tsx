import  goldPriceApi from '../../services/goldPriceApi';
import { GoldPrice } from '../../types/goldPrice.type';

const GoldPricePage = () => {
    const { data: goldPrices, error, isLoading } = goldPriceApi.useGetGoldPricesQuery();

    if (isLoading) return <p className="text-center text-yellow-500">Loading...</p>;
    if (error) return <p className="text-center text-red-500">Error fetching gold prices</p>;

    return (
        <div className="gold-price-page text-yellow-500 bg-navy h-screen w-screen p-5">
            <div className="flex flex-col items-center justify-center h-full">
                <h1 className="text-3xl font-bold mb-4">Giá Vàng Hiện Tại</h1>
                <div className="overflow-x-auto w-full">
                    <table className="min-w-full border-collapse">
                        <thead>
                            <tr className="bg-darkblue">
                                <th className="border border-yellow-500 px-4 py-2">Loại vàng</th>
                                <th className="border border-yellow-500 px-4 py-2">Thành Phố</th>
                                <th className="border border-yellow-500 px-4 py-2">Mua vào</th>
                                <th className="border border-yellow-500 px-4 py-2">Bán ra</th>
                                <th className="border border-yellow-500 px-4 py-2">Cập nhật gần nhất</th>
                            </tr>
                        </thead>
                        <tbody>
                            {goldPrices && goldPrices.map((price: GoldPrice) => (
                                <tr key={price.goldId} className="bg-blue">
                                    <td className="border border-yellow-500 px-4 py-2 text-red-500">{price.type}</td>
                                    <td className="border border-yellow-500 px-4 py-2 text-red-500">{price.city}</td>
                                    <td className="border border-yellow-500 px-4 py-2 text-red-500">{price.buyPrice}</td>
                                    <td className="border border-yellow-500 px-4 py-2 text-red-500">{price.sellPrice}</td>
                                    <td className="border border-yellow-500 px-4 py-2 text-red-500">{new Date(price.lastUpdated).toLocaleString()}</td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    );
};

export default GoldPricePage;

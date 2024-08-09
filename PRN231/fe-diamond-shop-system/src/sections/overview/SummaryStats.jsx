import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Grid } from '@mui/material';
import AppWidgetSummary from '../overview/app-widget-summary';
import AppWidgetSummaryString from '../overview/AppWidgetSummaryString';

const SummaryStats = () => {
  const [totalRevenue, setTotalRevenue] = useState(0);
  const [totalCustomers, setTotalCustomers] = useState(0);
  const [bestSellingJewelry, setBestSellingJewelry] = useState('N/A');

  useEffect(() => {
    const fetchData = async () => {
      const token = localStorage.getItem('TOKEN');
      const config = {
        headers: {
          Authorization: `Bearer ${token}`
        }
      };
      try {
        const totalRevenueResponse = await axios.get('http://localhost:5188/api/Dashboard/TotalRevenueAllTime', config);
        setTotalRevenue(totalRevenueResponse.data.totalRevenue);

        const totalCustomersResponse = await axios.get('http://localhost:5188/api/Dashboard/TotalCustomers', config);
        setTotalCustomers(totalCustomersResponse.data);

        const bestSellingJewelryResponse = await axios.get('http://localhost:5188/api/Dashboard/BestSellingJewelry', config);
        if (bestSellingJewelryResponse.data.length > 0) {
          const bestSelling = bestSellingJewelryResponse.data.reduce((prev, current) => {
            return (prev.purchaseTime > current.purchaseTime) ? prev : current;
          });
          setBestSellingJewelry(bestSelling.jewelryName);
        }
      } catch (error) {
        console.error("Error fetching data: ", error);
        setBestSellingJewelry('Error');
      }
    };

    fetchData();
  }, []);

  return (
    <Grid container spacing={3}>
      <Grid item xs={12} sm={4}>
        <AppWidgetSummary
          title="Total Revenue"
          total={totalRevenue}
          color="success"
          icon={<img alt="icon" src="/assets/icons/glass/ic_glass_bag.png" />}
        />
      </Grid>

      <Grid item xs={12} sm={4}>
        <AppWidgetSummary
          title="Total Customers"
          total={totalCustomers}
          color="info"
          icon={<img alt="icon" src="/assets/icons/glass/ic_glass_users.png" />}
        />
      </Grid>

      <Grid item xs={12} sm={4}>
        <AppWidgetSummaryString
          title="Best Selling Jewelry"
          total={bestSellingJewelry}
          color="warning"
          icon={<img alt="icon" src="/assets/icons/glass/ic_glass_buy.png" />}
        />
      </Grid>
    </Grid>
  );
};

export default SummaryStats;

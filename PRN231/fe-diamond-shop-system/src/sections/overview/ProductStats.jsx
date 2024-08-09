import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Card, CardContent, Typography, Grid } from '@mui/material';
import { BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer } from 'recharts';

const ProductStats = () => {
  const [bestSellingJewelry, setBestSellingJewelry] = useState([]);
  const [bestSellingJewelryTypes, setBestSellingJewelryTypes] = useState([]);
  const [totalRevenueByJewelry, setTotalRevenueByJewelry] = useState([]);
  const [totalRevenueByJewelryTypes, setTotalRevenueByJewelryTypes] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      const token = localStorage.getItem('TOKEN');
      const config = {
        headers: {
          Authorization: `Bearer ${token}`
        }
      };
      try {
        const bestSellingJewelryResponse = await axios.get('http://localhost:5188/api/Dashboard/BestSellingJewelry', config);
        setBestSellingJewelry(bestSellingJewelryResponse.data);

        const bestSellingJewelryTypesResponse = await axios.get('http://localhost:5188/api/Dashboard/BestSellingJewelryTypes', config);
        setBestSellingJewelryTypes(bestSellingJewelryTypesResponse.data);

        const totalRevenueByJewelryResponse = await axios.get('http://localhost:5188/api/Dashboard/TotalRevenueByJewelry', config);
        setTotalRevenueByJewelry(totalRevenueByJewelryResponse.data);

        const totalRevenueByJewelryTypesResponse = await axios.get('http://localhost:5188/api/Dashboard/TotalRevenueByJewelryTypes', config);
        setTotalRevenueByJewelryTypes(totalRevenueByJewelryTypesResponse.data);
      } catch (error) {
        console.error('Error fetching data: ', error);
      }
    };

    fetchData();
  }, []);

  return (
    <Grid container spacing={3}>
      <Grid item xs={12} sm={6}>
        <Card>
          <CardContent>
            <Typography variant="h6">Best Selling Jewelry</Typography>
            <ResponsiveContainer width="100%" height={300}>
              <BarChart data={bestSellingJewelry}>
                <CartesianGrid strokeDasharray="3 3" />
                <XAxis dataKey="jewelryName" />
                <YAxis />
                <Tooltip />
                <Legend />
                <Bar dataKey="purchaseTime" fill="#8884d8" />
              </BarChart>
            </ResponsiveContainer>
          </CardContent>
        </Card>
      </Grid>

      <Grid item xs={12} sm={6}>
        <Card>
          <CardContent>
            <Typography variant="h6">Best Selling Jewelry Types</Typography>
            <ResponsiveContainer width="100%" height={300}>
              <BarChart data={bestSellingJewelryTypes}>
                <CartesianGrid strokeDasharray="3 3" />
                <XAxis dataKey="jewelryTypeName" />
                <YAxis />
                <Tooltip />
                <Legend />
                <Bar dataKey="purchaseTime" fill="#82ca9d" />
              </BarChart>
            </ResponsiveContainer>
          </CardContent>
        </Card>
      </Grid>

      <Grid item xs={12} sm={6}>
        <Card>
          <CardContent>
            <Typography variant="h6">Total Revenue by Jewelry</Typography>
            <ResponsiveContainer width="100%" height={300}>
              <BarChart data={totalRevenueByJewelry}>
                <CartesianGrid strokeDasharray="3 3" />
                <XAxis dataKey="jewelryName" />
                <YAxis />
                <Tooltip />
                <Legend />
                <Bar dataKey="jewelryRevenue" fill="#ffc658" />
              </BarChart>
            </ResponsiveContainer>
          </CardContent>
        </Card>
      </Grid>

      <Grid item xs={12} sm={6}>
        <Card>
          <CardContent>
            <Typography variant="h6">Total Revenue by Jewelry Types</Typography>
            <ResponsiveContainer width="100%" height={300}>
              <BarChart data={totalRevenueByJewelryTypes}>
                <CartesianGrid strokeDasharray="3 3" />
                <XAxis dataKey="jewelryTypeName" />
                <YAxis />
                <Tooltip />
                <Legend />
                <Bar dataKey="jewelryTypeRevenue" fill="#d0ed57" />
              </BarChart>
            </ResponsiveContainer>
          </CardContent>
        </Card>
      </Grid>
    </Grid>
  );
};

export default ProductStats;

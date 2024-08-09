import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Card, CardContent, Typography, Grid, MenuItem, Select, FormControl, InputLabel, Box } from '@mui/material';
import { BarChart, Bar, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer } from 'recharts';

const RevenueStats = () => {
  const [totalRevenue, setTotalRevenue] = useState(0);
  const [monthlyRevenueData, setMonthlyRevenueData] = useState([]);
  const [selectedMonth, setSelectedMonth] = useState(new Date().getMonth() + 1); // Default current month
  const [selectedYear, setSelectedYear] = useState(new Date().getFullYear()); // Default current year

  useEffect(() => {
    const fetchData = async () => {
      const totalResponse = await axios.get('http://localhost:5188/api/Dashboard/TotalRevenueAllTime');
      setTotalRevenue(totalResponse.data.totalRevenue);

      const monthlyResponse = await axios.get('http://localhost:5188/api/Dashboard/GetTotalRevenue', {
        params: { startDate: `${selectedYear}-${selectedMonth}-01`, endDate: `${selectedYear}-${selectedMonth}-31` } // Month dates
      });

      setMonthlyRevenueData([{
        name: `${new Date(selectedYear, selectedMonth - 1).toLocaleString('default', { month: 'long' })} ${selectedYear}`,
        revenue: monthlyResponse.data.totalRevenue
      }]);
    };

    fetchData();
  }, [selectedMonth, selectedYear]);

  return (
    <Grid container spacing={3}>
      <Grid item xs={12}>
        <Card>
          <CardContent>
            <Typography variant="h6">Total Revenue</Typography>
            <Typography variant="h4">${totalRevenue}</Typography>
          </CardContent>
        </Card>
      </Grid>
      <Grid item xs={12}>
        <Card>
          <CardContent>
            <Typography variant="h6">Monthly Revenue</Typography>
            <Box sx={{ minWidth: 120, mt: 2 }}>
              <FormControl fullWidth>
                <InputLabel>Month</InputLabel>
                <Select
                  value={selectedMonth}
                  label="Month"
                  onChange={(e) => setSelectedMonth(e.target.value)}
                >
                  {Array.from({ length: 12 }, (_, index) => (
                    <MenuItem key={index + 1} value={index + 1}>
                      {new Date(0, index).toLocaleString('default', { month: 'long' })}
                    </MenuItem>
                  ))}
                </Select>
              </FormControl>
            </Box>
            <Box sx={{ minWidth: 120, mt: 2 }}>
              <FormControl fullWidth>
                <InputLabel>Year</InputLabel>
                <Select
                  value={selectedYear}
                  label="Year"
                  onChange={(e) => setSelectedYear(e.target.value)}
                >
                  {Array.from({ length: 10 }, (_, index) => (
                    <MenuItem key={selectedYear - index} value={selectedYear - index}>
                      {selectedYear - index}
                    </MenuItem>
                  ))}
                </Select>
              </FormControl>
            </Box>
            <ResponsiveContainer width="100%" height={400}>
              <BarChart data={monthlyRevenueData}>
                <CartesianGrid strokeDasharray="3 3" />
                <XAxis dataKey="name" />
                <YAxis />
                <Tooltip />
                <Legend />
                <Bar dataKey="revenue" fill="#8884d8" />
              </BarChart>
            </ResponsiveContainer>
          </CardContent>
        </Card>
      </Grid>
    </Grid>
  );
};

export default RevenueStats;

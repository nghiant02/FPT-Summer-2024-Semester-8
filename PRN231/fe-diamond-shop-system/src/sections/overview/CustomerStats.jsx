import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Card, CardContent, Typography, Grid } from '@mui/material';

const CustomerStats = () => {
  const [totalCustomers, setTotalCustomers] = useState(0);
  const [activeCustomers, setActiveCustomers] = useState(0);
  const [newCustomers, setNewCustomers] = useState(0);

  useEffect(() => {
    const fetchData = async () => {
      const totalResponse = await axios.get('http://localhost:5188/api/Dashboard/TotalCustomers');
      setTotalCustomers(totalResponse.data);

      const activeResponse = await axios.get('/api/Dashboard/ActiveCustomers', {
        params: { startDate: '2024-01-01', endDate: '2024-07-07' }
      });
      setActiveCustomers(activeResponse.data);

      const newResponse = await axios.get('/api/Dashboard/NewCustomers', {
        params: { startDate: '2024-01-01', endDate: '2024-07-07' }
      });
      setNewCustomers(newResponse.data);
    };

    fetchData();
  }, []);

  return (
    <Grid container spacing={3}>
      <Grid item xs={12} sm={4}>
        <Card>
          <CardContent>
            <Typography variant="h6">Total Customers</Typography>
            <Typography variant="h4">{totalCustomers}</Typography>
          </CardContent>
        </Card>
      </Grid>
      <Grid item xs={12} sm={4}>
        <Card>
          <CardContent>
            <Typography variant="h6">Active Customers</Typography>
            <Typography variant="h4">{activeCustomers}</Typography>
          </CardContent>
        </Card>
      </Grid>
      <Grid item xs={12} sm={4}>
        <Card>
          <CardContent>
            <Typography variant="h6">New Customers</Typography>
            <Typography variant="h4">{newCustomers}</Typography>
          </CardContent>
        </Card>
      </Grid>
    </Grid>
  );
};

export default CustomerStats;

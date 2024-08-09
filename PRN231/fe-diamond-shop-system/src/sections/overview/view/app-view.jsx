import Container from '@mui/material/Container';
import Grid from '@mui/material/Unstable_Grid2';
import Typography from '@mui/material/Typography';


import ProductStats from '../ProductStats';
import SummaryStats from '../SummaryStats';

// ----------------------------------------------------------------------

export default function AppView() {
  return (
    <Container maxWidth="xl">
      <Typography variant="h4" sx={{ mb: 5 }}>
        Hi, Welcome back 👋
      </Typography>

      <SummaryStats /> <br/>

      <Grid container spacing={3}>

        <Grid xs={12}>
          <Typography variant="h5" sx={{ mb: 3 }}>Product Statistics</Typography>
          <ProductStats />
        </Grid>

      </Grid>
      
    </Container>
  );
}

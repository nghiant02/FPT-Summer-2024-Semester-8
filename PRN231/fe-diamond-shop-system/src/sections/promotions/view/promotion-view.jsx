import axios from 'axios';
import { useState, useEffect } from 'react';
import { toast } from 'react-toastify';

import Card from '@mui/material/Card';
import Stack from '@mui/material/Stack';
import Table from '@mui/material/Table';
import Button from '@mui/material/Button';
import Container from '@mui/material/Container';
import TableBody from '@mui/material/TableBody';
import Typography from '@mui/material/Typography';
import TableContainer from '@mui/material/TableContainer';
import TablePagination from '@mui/material/TablePagination';

import Iconify from 'src/components/iconify';
import Scrollbar from 'src/components/scrollbar';

import TableNoData from '../table-no-data';
import UserTableRow from '../promo-table-row';
import UserTableHead from '../promo-table-head';
import TableEmptyRows from '../table-empty-rows';
import PromotionForm from '../create-promo-table';
import UserTableToolbar from '../promo-table-toolbar';
import { emptyRows, applyFilter, getComparator } from '../utils';

// ----------------------------------------------------------------------

export default function PromotionView() {
  const [promotion, setPromotion] = useState([]);
  const [page, setPage] = useState(0);
  const [order, setOrder] = useState('asc');
  const [selected, setSelected] = useState([]);
  const [orderBy, setOrderBy] = useState('name');
  const [filterName, setFilterName] = useState('');
  const [rowsPerPage, setRowsPerPage] = useState(5);
  const [showPromotionForm, setShowPromotionForm] = useState(false);
  const [managers, setManagers] = useState([]);

  useEffect(() => {
    getPromotion();
    fetchManagers();
  }, []);

  const getPromotion = async () => {
    const token = localStorage.getItem('TOKEN');
    const config = {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    };
    const res = await axios.get('http://localhost:5188/api/Promotion/GetPromotions', config);
    setPromotion(res.data);
  };

  const fetchManagers = async () => {
    const token = localStorage.getItem('TOKEN');
    const config = {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    };
    try {
      const response = await axios.get('http://localhost:5188/api/User/GetUsers', config);
      const managerData = response.data.filter((user) => user.roleName === 'Manager');
      setManagers(managerData);
    } catch (error) {
      console.error('Error fetching managers:', error);
    }
  };

  const handleSort = (event, id) => {
    const isAsc = orderBy === id && order === 'asc';
    if (id !== '') {
      setOrder(isAsc ? 'desc' : 'asc');
      setOrderBy(id);
    }
  };

  const handleSelectAllClick = (event) => {
    if (event.target.checked) {
      const newSelecteds = promotion.map((n) => n.name);
      setSelected(newSelecteds);
      return;
    }
    setSelected([]);
  };

  const handleClick = (event, name) => {
    const selectedIndex = selected.indexOf(name);
    let newSelected = [];
    if (selectedIndex === -1) {
      newSelected = newSelected.concat(selected, name);
    } else if (selectedIndex === 0) {
      newSelected = newSelected.concat(selected.slice(1));
    } else if (selectedIndex === selected.length - 1) {
      newSelected = newSelected.concat(selected.slice(0, -1));
    } else if (selectedIndex > 0) {
      newSelected = newSelected.concat(selected.slice(0, selectedIndex), selected.slice(selectedIndex + 1));
    }
    setSelected(newSelected);
  };

  const handleChangePage = (event, newPage) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (event) => {
    setPage(0);
    setRowsPerPage(parseInt(event.target.value, 10));
  };

  const handleFilterByName = (event) => {
    setPage(0);
    setFilterName(event.target.value);
  };

  const dataFiltered = applyFilter({
    inputData: promotion,
    comparator: getComparator(order, orderBy),
    filterName,
  });

  const notFound = !dataFiltered.length && !!filterName;

  const handleClosePromotionForm = () => {
    setShowPromotionForm(false);
  };

  const handleNewPromotionClick = async (newPromotionData) => {
    const token = localStorage.getItem('TOKEN');
    const config = {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    };
    try {
      const res = await axios.post(
        `http://localhost:5188/api/Promotion/AddNewPromotion?userId=${newPromotionData.userId}`,
        newPromotionData,
        config
      );
      toast.success('Create promotion successful!', {
        position: 'bottom-right',
        theme: 'colored',
      });
      getPromotion();
    } catch (error) {
      toast.error('Create promotion fail');
      console.error('There was an error creating the promotion:', error);
    }
    setShowPromotionForm(false);
  };

  return (
    <Container>
      <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
        <Typography variant="h4">Promotion</Typography>

        <Button
          onClick={() => setShowPromotionForm(true)}
          variant="contained"
          color="inherit"
          startIcon={<Iconify icon="eva:plus-fill" />}
        >
          New Promotion
        </Button>
      </Stack>

      <PromotionForm
        open={showPromotionForm}
        onClose={handleClosePromotionForm}
        onSubmit={handleNewPromotionClick}
        managers={managers} // Pass managers to the form
      />

      <Card>
        <UserTableToolbar
          numSelected={selected.length}
          filterName={filterName}
          onFilterName={handleFilterByName}
        />

        <Scrollbar>
          <TableContainer sx={{ overflow: 'unset' }}>
            <Table sx={{ minWidth: 800 }}>
              <UserTableHead
                order={order}
                orderBy={orderBy}
                rowCount={promotion.length}
                numSelected={selected.length}
                onRequestSort={handleSort}
                onSelectAllClick={handleSelectAllClick}
                headLabel={[
                  { id: 'type', label: 'Type' },
                  { id: 'discountRate', label: 'DiscountRate' },
                  { id: 'startDate', label: 'StartDate' },
                  { id: 'endDate', label: 'EndDate' },
                  { id: 'actions', label: 'Actions', width: 165 },
                ]}
              />
              <TableBody>
                {dataFiltered
                  .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                  .map((row, index) => (
                    <UserTableRow
                      key={row.promotionId}
                      promotionId={row.promotionId}
                      type={row.type}
                      userId={row.userId}
                      description={row.description}
                      discountRate={row.discountRate}
                      startDate={row.startDate}
                      endDate={row.endDate}
                      selected={selected.indexOf(row.promotionId) !== -1}
                      handleClick={(event) => handleClick(event, row.promotionId)}
                    />
                  ))}

                <TableEmptyRows height={77} emptyRows={emptyRows(page, rowsPerPage, promotion.length)} />

                {notFound && <TableNoData query={filterName} />}
              </TableBody>
            </Table>
          </TableContainer>
        </Scrollbar>

        <TablePagination
          page={page}
          component="div"
          count={promotion.length}
          rowsPerPage={rowsPerPage}
          onPageChange={handleChangePage}
          rowsPerPageOptions={[5, 10, 25]}
          onRowsPerPageChange={handleChangeRowsPerPage}
        />
      </Card>
    </Container>
  );
}

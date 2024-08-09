import { useState, useEffect } from 'react';
import axios from 'axios';
import Card from '@mui/material/Card';
import Stack from '@mui/material/Stack';
import Table from '@mui/material/Table';
// import Button from '@mui/material/Button';
import Container from '@mui/material/Container';
import TableBody from '@mui/material/TableBody';
import Typography from '@mui/material/Typography';
import TableContainer from '@mui/material/TableContainer';
import TablePagination from '@mui/material/TablePagination';
//import Iconify from 'src/components/iconify';
import Scrollbar from 'src/components/scrollbar';
import TableNoData from '../table-no-data';
import BillTableRow from '../bill-table-row';
import BillTableHead from '../bill-table-head';
import TableEmptyRows from '../table-empty-rows';
import BillTableToolbar from '../bill-table-toolbar';
import BillDetailsDialog from '../bill-details-dialog';
import { emptyRows, applyFilter, getComparator } from '../utils';

// ----------------------------------------------------------------------

export default function BillPage() {
  const [page, setPage] = useState(0);
  const [order, setOrder] = useState('asc');
  const [selected, setSelected] = useState([]);
  const [orderBy, setOrderBy] = useState('name');
  const [filterName, setFilterName] = useState('');
  const [rowsPerPage, setRowsPerPage] = useState(5);
  const [bills, setBills] = useState([]);
  const [totalRecords, setTotalRecords] = useState(0);
  const [selectedBill, setSelectedBill] = useState(null);

  useEffect(() => {
    fetchBills(page + 1, rowsPerPage);
  }, [page, rowsPerPage]);

  const fetchBills = async (pageNumber, pageSize) => {
    const token = localStorage.getItem('TOKEN');
    const config = {
      headers: {
        Authorization: `Bearer ${token}`
      }
    };
    try {
      const response = await axios.get(`http://localhost:5188/api/Bill/GetBills`, config, {
        params: {
          pageNumber,
          pageSize,
        },
      });
      setBills(response.data.data);
      setTotalRecords(response.data.totalRecord);
    } catch (error) {
      console.error('Error fetching bills: ', error);
    }
  };

  const handleSort = (event, id) => {
    const isAsc = orderBy === id && order === 'asc';
    setOrder(isAsc ? 'desc' : 'asc');
    setOrderBy(id);
  };

  const handleSelectAllClick = (event) => {
    if (event.target.checked) {
      const newSelecteds = bills.map((n) => n.billId);
      setSelected(newSelecteds);
      return;
    }
    setSelected([]);
  };

  const handleClick = (event, billId) => {
    const selectedIndex = selected.indexOf(billId);
    let newSelected = [];
    if (selectedIndex === -1) {
      newSelected = newSelected.concat(selected, billId);
    } else if (selectedIndex === 0) {
      newSelected = newSelected.concat(selected.slice(1));
    } else if (selectedIndex === selected.length - 1) {
      newSelected = newSelected.concat(selected.slice(0, -1));
    } else if (selectedIndex > 0) {
      newSelected = newSelected.concat(
        selected.slice(0, selectedIndex),
        selected.slice(selectedIndex + 1)
      );
    }
    setSelected(newSelected);
  };

  const handleChangePage = (event, newPage) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (event) => {
    setRowsPerPage(parseInt(event.target.value, 10));
    setPage(0);
  };

  const handleFilterByName = (event) => {
    setPage(0);
    setFilterName(event.target.value);
  };

  const handleMoreInfoClick = (bill) => {
    setSelectedBill(bill);
  };

  const handleCloseBillDetails = () => {
    setSelectedBill(null);
  };

  const dataFiltered = applyFilter({
    inputData: bills,
    comparator: getComparator(order, orderBy),
    filterName,
  });

  const notFound = !dataFiltered.length && !!filterName;

  return (
    <Container>
      <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
        <Typography variant="h4">Bill</Typography>
      </Stack>

      <Card>
        <BillTableToolbar
          numSelected={selected.length}
          filterName={filterName}
          onFilterName={handleFilterByName}
        />

        <Scrollbar>
          <TableContainer sx={{ overflow: 'unset' }}>
            <Table sx={{ minWidth: 800 }}>
              <BillTableHead
                order={order}
                orderBy={orderBy}
                rowCount={bills.length}
                numSelected={selected.length}
                onRequestSort={handleSort}
                onSelectAllClick={handleSelectAllClick}
                headLabel={[
                  { id: 'staffName', label: 'Staff Name' },
                  { id: 'customerName', label: 'Customer Name' },
                  { id: 'totalAmount', label: 'Total Amount' },
                  { id: 'totalDiscount', label: 'Total Discount' },
                  { id: 'saleDate', label: 'Sale Date' },
                  { id: 'items', label: 'Items' },
                  { id: 'finalAmount', label: 'Final Amount' },
                  { id: '' },
                ]}
              />
              <TableBody>
                {dataFiltered
                  .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                  .map((bill) => (
                    <BillTableRow
                      key={bill.id}
                      bill={bill}
                      selected={selected.indexOf(bill.billId) !== -1}
                      handleClick={(event) => handleClick(event, bill.billId)}
                      onMoreInfoClick={handleMoreInfoClick}
                    />
                  ))}

                <TableEmptyRows
                  height={77}
                  emptyRows={emptyRows(page, rowsPerPage, bills.length)}
                />

                {notFound && <TableNoData query={filterName} />}
              </TableBody>
            </Table>
          </TableContainer>
        </Scrollbar>

        <TablePagination
          page={page}
          component="div"
          count={totalRecords}
          rowsPerPage={rowsPerPage}
          onPageChange={handleChangePage}
          rowsPerPageOptions={[5, 10, 25]}
          onRowsPerPageChange={handleChangeRowsPerPage}
        />
      </Card>

      {selectedBill && (
        <BillDetailsDialog
          open={!!selectedBill}
          onClose={handleCloseBillDetails}
          bill={selectedBill}
        />
      )}
    </Container>
  );
}

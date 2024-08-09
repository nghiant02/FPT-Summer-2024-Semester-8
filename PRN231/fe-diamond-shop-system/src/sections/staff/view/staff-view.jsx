import { useState, useEffect } from 'react';
import axios from 'axios';
import Card from '@mui/material/Card';
import Stack from '@mui/material/Stack';
import Table from '@mui/material/Table';
import Button from '@mui/material/Button';
import Container from '@mui/material/Container';
import TableBody from '@mui/material/TableBody';
import Typography from '@mui/material/Typography';
import TableContainer from '@mui/material/TableContainer';
import TablePagination from '@mui/material/TablePagination';
import { useSnackbar } from 'notistack';
import Iconify from 'src/components/iconify';
import Scrollbar from 'src/components/scrollbar';

import TableNoData from '../table-no-data';
import UserTableRow from '../staff-table-row';
import StaffForm from '../create-staff-table';
import UserTableHead from '../staff-table-head';
import TableEmptyRows from '../table-empty-rows';
import UserTableToolbar from '../staff-table-toolbar';
import { emptyRows, applyFilter, getComparator } from '../utils';

// ----------------------------------------------------------------------

export default function StaffView() {
    const { enqueueSnackbar } = useSnackbar();
    const [staff, setStaff] = useState([]);

    useEffect(() => {
        const fetchStaff = async () => {
            const token = localStorage.getItem('TOKEN');
            const config = {
              headers: {
                Authorization: `Bearer ${token}`
              }
            };
            try {
                const response = await axios.get('http://localhost:5188/api/User/GetUsers', config);
                const filteredStaff = response.data.filter(user => user.roleName === 'Staff' || user.roleName === 'Manager');
                setStaff(filteredStaff);
            } catch (error) {
                console.error('Error fetching staff:', error);
            }
        };
        fetchStaff();
    }, []);

    const handleAddStaff = async (newStaffData) => {
        const token = localStorage.getItem('TOKEN');
        const config = {
            headers: {
                Authorization: `Bearer ${token}`
            }
        };
        try {
            const response = await axios.post('http://localhost:5188/api/User/AddUser', newStaffData, config);
            setStaff([...staff, response.data]);
            enqueueSnackbar('Staff added successfully', { variant: 'success' });
        } catch (error) {
            console.error('Error adding staff:', error);
            enqueueSnackbar('Failed to add staff', { variant: 'error' });
        }
    };

    const handleEditStaff = async (updatedStaffData) => {
        const token = localStorage.getItem('TOKEN');
        const config = {
            headers: {
                Authorization: `Bearer ${token}`
            }
        };
        try {
            await axios.put(`http://localhost:5188/api/User/UpdateUser/${updatedStaffData.userId}`, updatedStaffData, config);
            setStaff(staff.map(st => (st.userId === updatedStaffData.userId ? updatedStaffData : st)));
            enqueueSnackbar('Staff updated successfully', { variant: 'success' });
        } catch (error) {
            console.error('Error updating staff:', error);
            enqueueSnackbar('Failed to update staff', { variant: 'error' });
        }
    };

    const handleDeleteStaff = async (userId) => {
        const token = localStorage.getItem('TOKEN');
        const config = {
            headers: {
                Authorization: `Bearer ${token}`
            }
        };
        try {
            await axios.delete(`http://localhost:5188/api/User/DeleteUser/${userId}`, config);
            setStaff(staff.filter(st => st.userId !== userId));
            enqueueSnackbar('Staff deleted successfully', { variant: 'success' });
        } catch (error) {
            console.error('Error deleting staff:', error);
            enqueueSnackbar('Failed to delete staff', { variant: 'error' });
        }
    };

    const [page, setPage] = useState(0);
    const [order, setOrder] = useState('asc');
    const [selected, setSelected] = useState([]);
    const [orderBy, setOrderBy] = useState('name');
    const [filterName, setFilterName] = useState('');
    const [rowsPerPage, setRowsPerPage] = useState(5);
    const [showStaffForm, setShowStaffForm] = useState(false);

    const handleSort = (event, id) => {
        const isAsc = orderBy === id && order === 'asc';
        setOrder(isAsc ? 'desc' : 'asc');
        setOrderBy(id);
    };

    const handleSelectAllClick = (event) => {
        if (event.target.checked) {
            const newSelecteds = staff.map((n) => n.username);
            setSelected(newSelecteds);
            return;
        }
        setSelected([]);
    };

    const handleClick = (event, username) => {
        const selectedIndex = selected.indexOf(username);
        let newSelected = [];
        if (selectedIndex === -1) {
            newSelected = newSelected.concat(selected, username);
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
        setPage(0);
        setRowsPerPage(parseInt(event.target.value, 10));
    };

    const handleFilterByName = (event) => {
        setPage(0);
        setFilterName(event.target.value);
    };

    const dataFiltered = applyFilter({
        inputData: staff,
        comparator: getComparator(order, orderBy),
        filterName,
    });

    const notFound = !dataFiltered.length && !!filterName;

    const handleCloseStaffForm = () => {
        setShowStaffForm(false);
    };

    return (
        <Container>
            <Stack direction="row" alignItems="center" justifyContent="space-between" mb={5}>
                <Typography variant="h4">Staff</Typography>

                <Button
                    onClick={() => setShowStaffForm(true)}
                    variant="contained" color="inherit" startIcon={<Iconify icon="eva:plus-fill" />}>
                    New Staff
                </Button>
            </Stack>

            <StaffForm open={showStaffForm} onClose={handleCloseStaffForm}
                onSubmit={handleAddStaff}
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
                                rowCount={staff.length}
                                numSelected={selected.length}
                                onRequestSort={handleSort}
                                onSelectAllClick={handleSelectAllClick}
                                headLabel={[
                                    { id: 'username', label: 'Username' },
                                    { id: 'fullName', label: 'Full Name' },
                                    { id: 'email', label: 'Email' },
                                    { id: 'gender', label: 'Gender' },
                                    { id: 'phoneNumber', label: 'Phone Number' },
                                    { id: 'roleName', label: 'Role Name' },
                                    { id: 'status', label: 'Status' },
                                    { id: '' },
                                ]}
                            />
                            <TableBody>
                                {dataFiltered
                                    .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                                    .map((row) => (
                                        <UserTableRow
                                            key={row.userId}
                                            userId={row.userId}
                                            username={row.username}
                                            fullName={row.fullName}
                                            email={row.email}
                                            gender={row.gender}
                                            phoneNumber={row.phoneNumber}
                                            roleName={row.roleName}
                                            status={row.status}
                                            selected={selected.indexOf(row.username) !== -1}
                                            handleClick={(event) => handleClick(event, row.username)}
                                            onDelete={handleDeleteStaff}
                                            onEdit={handleEditStaff}
                                        />
                                    ))}

                                <TableEmptyRows
                                    height={77}
                                    emptyRows={emptyRows(page, rowsPerPage, staff.length)}
                                />

                                {notFound && <TableNoData query={filterName} />}
                            </TableBody>
                        </Table>
                    </TableContainer>
                </Scrollbar>

                <TablePagination
                    page={page}
                    component="div"
                    count={staff.length}
                    rowsPerPage={rowsPerPage}
                    onPageChange={handleChangePage}
                    rowsPerPageOptions={[5, 10, 25]}
                    onRowsPerPageChange={handleChangeRowsPerPage}
                />
            </Card>
        </Container>
    );
}

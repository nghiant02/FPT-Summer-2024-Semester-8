import { useState } from 'react';
import PropTypes from 'prop-types';
import axios from 'axios';
import { toast } from 'react-toastify';

import Grid from '@mui/material/Grid';
import Button from '@mui/material/Button';
import Dialog from '@mui/material/Dialog';
import Popover from '@mui/material/Popover';
import Checkbox from '@mui/material/Checkbox';
import MenuItem from '@mui/material/MenuItem';
import TableRow from '@mui/material/TableRow';
import TableCell from '@mui/material/TableCell';
import IconButton from '@mui/material/IconButton';
import Typography from '@mui/material/Typography';
import DialogTitle from '@mui/material/DialogTitle';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';

import Iconify from 'src/components/iconify';

import CustomerEditForm from './customer-edit-model';
import CustomerDeleteForm from './customer-del-model';

// ----------------------------------------------------------------------

export default function UserTableRow({
  selected,
  CusID,
  userName,
  fullName,
  email,
  phone,
  gender,
  address,
  point,
  handleClick,
}) {
  const [open, setOpen] = useState(null);
  const [dialogOpen, setDialogOpen] = useState(false);
  const [editOpen, setEditOpen] = useState(false);
  const [deleteOpen, setDeleteOpen] = useState(false);

  const handleOpenMenu = (event) => {
    setOpen(event.currentTarget);
  };

  const handleCloseMenu = () => {
    setOpen(null);
  };

  const handleDialogOpen = () => {
    setDialogOpen(true);
  };

  const handleDialogClose = () => {
    setDialogOpen(false);
  };

  const handleEditOpen = () => {
    setEditOpen(true);
    handleCloseMenu();
  };

  const handleEditClose = () => {
    setEditOpen(false);
  };

  const onSubmit = async (updatedData) => {
    const token = localStorage.getItem('TOKEN');
    const config = {
        headers: {
            Authorization: `Bearer ${token}`
        }
    };
    try {
      const res = await axios.put(`http://localhost:5188/api/Customer/UpdateCustomer/${CusID}`, updatedData, config);
      if (res.status === 200) {
        toast.success("Edit customer success");
        handleEditClose();
      } else {
        toast.error('Edit customer failed');
      }
    } catch (e) {
      toast.error('Error response');
    }
  };

  const handleDeleteOpen = () => {
    setDeleteOpen(true);
    handleCloseMenu();
  };

  const handleDeleteClose = () => {
    setDeleteOpen(false);
  };

  const onDelete = async () => {
    const token = localStorage.getItem('TOKEN');
    const config = {
        headers: {
            Authorization: `Bearer ${token}`
        }
    };
    try {
      const res = await axios.delete(`http://localhost:5188/api/Customer/DeleteCustomer/${CusID}`, config);
      if (res.status === 200) {
        toast.success("Delete success");
        handleDeleteClose();
      } else {
        toast.error("Delete failed");
      }
    } catch (e) {
      toast.error("Error response");
    }
  };

  return (
    <>
      <TableRow hover tabIndex={-1} role="checkbox" selected={selected}>
        <TableCell padding="checkbox">
          <Checkbox disableRipple checked={selected} onChange={handleClick} />
        </TableCell>

        <TableCell>{userName}</TableCell>
        <TableCell>{fullName}</TableCell>
        <TableCell>{email}</TableCell>
        <TableCell>{phone}</TableCell>
        <TableCell>{gender}</TableCell>
        <TableCell>{address}</TableCell>
        <TableCell>{point}</TableCell>

        <TableCell align="right">
          <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
            <Button variant="outlined" onClick={handleDialogOpen}>
              More Info
            </Button>
            <IconButton onClick={handleOpenMenu}>
              <Iconify icon="eva:more-vertical-fill" />
            </IconButton>
          </div>
        </TableCell>
      </TableRow>

      <Dialog open={dialogOpen} onClose={handleDialogClose}>
        <DialogTitle>Customer</DialogTitle>
        <DialogContent>
          <Grid container spacing={2}>
            <Grid item xs={12}>
              <Typography variant="h6">Username:</Typography>
              <Typography>{userName}</Typography>
            </Grid>
            <Grid item xs={12}>
              <Typography variant="h6">Full Name:</Typography>
              <Typography>{fullName}</Typography>
            </Grid>
            <Grid item xs={12}>
              <Typography variant="h6">Email:</Typography>
              <Typography>{email}</Typography>
            </Grid>
            <Grid item xs={12}>
              <Typography variant="h6">Phone:</Typography>
              <Typography>{phone}</Typography>
            </Grid>
            <Grid item xs={12}>
              <Typography variant="h6">Gender:</Typography>
              <Typography>{gender}</Typography>
            </Grid>
            <Grid item xs={12}>
              <Typography variant="h6">Address:</Typography>
              <Typography>{address}</Typography>
            </Grid>
            <Grid item xs={12}>
              <Typography variant="h6">Point:</Typography>
              <Typography>{point}</Typography>
            </Grid>
          </Grid>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleDialogClose}>
            Close
          </Button>
        </DialogActions>
      </Dialog>

      <Popover
        open={!!open}
        anchorEl={open}
        onClose={handleCloseMenu}
        anchorOrigin={{ vertical: 'top', horizontal: 'left' }}
        transformOrigin={{ vertical: 'top', horizontal: 'right' }}
        PaperProps={{
          sx: { width: 140 },
        }}
      >
        <MenuItem onClick={handleEditOpen}>
          <Iconify icon="eva:edit-fill" sx={{ mr: 2 }} />
          Edit
        </MenuItem>

        <MenuItem onClick={handleDeleteOpen} sx={{ color: 'error.main' }}>
          <Iconify icon="eva:trash-2-outline" sx={{ mr: 2 }} />
          Delete
        </MenuItem>
      </Popover>

      <CustomerEditForm
        open={editOpen}
        onClose={handleEditClose}
        customer={{ CusID, userName, fullName, email, phone, gender, address, point }}
        onSubmit={onSubmit}
      />

      <CustomerDeleteForm
        open={deleteOpen}
        onClose={handleDeleteClose}
        onDelete={onDelete}
        customer={{ CusID, userName, fullName, email, phone, gender, address, point }}
      />
    </>
  );
}

UserTableRow.propTypes = {
  CusID: PropTypes.any,
  userName: PropTypes.string,
  fullName: PropTypes.string,
  email: PropTypes.string,
  phone: PropTypes.string,
  gender: PropTypes.string,
  address: PropTypes.string,
  point: PropTypes.any,
  handleClick: PropTypes.func,
  selected: PropTypes.bool,
};

// staff-table-row.jsx

import { useState } from 'react';
import PropTypes from 'prop-types';

import Popover from '@mui/material/Popover';
import TableRow from '@mui/material/TableRow';
import Checkbox from '@mui/material/Checkbox';
import MenuItem from '@mui/material/MenuItem';
import TableCell from '@mui/material/TableCell';
import IconButton from '@mui/material/IconButton';
import {
  Grid,
  Button,
  Dialog,
  Typography,
  DialogTitle,
  DialogContent,
  DialogActions,
} from '@mui/material';

import Label from 'src/components/label';
import Iconify from 'src/components/iconify';

import StaffEditForm from './staff-edit-modal';
import StaffDeleteForm from './staff-del-modal';

// ----------------------------------------------------------------------

export default function UserTableRow({
  selected,
  userId,
  username,
  fullName,
  email,
  gender,
  phoneNumber,
  roleName,
  status,
  handleClick,
  onEdit,
  onDelete,
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

  const onSubmit = (updatedData) => {
    onEdit(updatedData);
    handleEditClose();
  };

  const handleDeleteOpen = () => {
    setDeleteOpen(true);
    handleCloseMenu();
  };

  const handleDeleteClose = () => {
    setDeleteOpen(false);
    handleCloseMenu();
  };

  const onDeleteConfirm = () => {
    onDelete(userId);
    handleDeleteClose();
  };

  return (
    <>
      <TableRow hover tabIndex={-1} role="checkbox" selected={selected}>
        <TableCell padding="checkbox">
          <Checkbox disableRipple checked={selected} onChange={handleClick} />
        </TableCell>

        <TableCell>{username}</TableCell>
        <TableCell>{fullName}</TableCell>
        <TableCell>{email}</TableCell>
        <TableCell>{gender}</TableCell>
        <TableCell>{phoneNumber}</TableCell>
        <TableCell>
          <Label
            color={
              (roleName === 'Admin' && 'primary') ||
              (roleName === 'Manager' && 'secondary') ||
              (roleName === 'Staff' && 'info') ||
              'success'
            }
          >
            {roleName}
          </Label>
        </TableCell>
        <TableCell>
          <Label color={status ? 'success' : 'error'}>{status ? 'active' : 'inactive'}</Label>
        </TableCell>

        <TableCell align="right">
          <Button variant="outlined" onClick={handleDialogOpen}>
            More Info
          </Button>
          <IconButton onClick={handleOpenMenu}>
            <Iconify icon="eva:more-vertical-fill" />
          </IconButton>
        </TableCell>
      </TableRow>

      <Dialog open={dialogOpen} onClose={handleDialogClose}>
        <DialogTitle>Staff</DialogTitle>
        <DialogContent>
          <Grid container spacing={2}>
            <Grid item xs={12}>
              <Typography variant="h6">ID:</Typography>
              <Typography>{userId}</Typography>
            </Grid>
            <Grid item xs={12}>
              <Typography variant="h6">User Name:</Typography>
              <Typography>{username}</Typography>
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
              <Typography variant="h6">Gender:</Typography>
              <Typography>{gender}</Typography>
            </Grid>
            <Grid item xs={12}>
              <Typography variant="h6">Phone Number:</Typography>
              <Typography>{phoneNumber}</Typography>
            </Grid>
            <Grid item xs={12}>
              <Typography variant="h6">Role Name:</Typography>
              <Label
                color={
                  (roleName === 'Admin' && 'primary') ||
                  (roleName === 'Manager' && 'secondary') ||
                  (roleName === 'Staff' && 'info') ||
                  'success'
                }
              >
                {roleName}
              </Label>
            </Grid>
            <Grid item xs={12}>
              <Typography variant="h6">Status:</Typography>
              <Typography>
                <Label color={status ? 'success' : 'error'}>{status ? 'active' : 'inactive'}</Label>
              </Typography>
            </Grid>
          </Grid>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleDialogClose}>Close</Button>
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

      <StaffEditForm
        open={editOpen}
        onClose={handleEditClose}
        staff={{
          userId,
          username,
          fullName,
          email,
          gender,
          phoneNumber,
          roleName,
          status,
        }}
        onSubmit={onSubmit}
      />

      <StaffDeleteForm
        open={deleteOpen}
        onClose={handleDeleteClose}
        onDelete={onDeleteConfirm}
        staff={{
          userId,
          username,
          fullName,
          email,
          gender,
          phoneNumber,
          roleName,
          status,
        }}
      />
    </>
  );
}

UserTableRow.propTypes = {
  userId: PropTypes.string,
  username: PropTypes.string,
  fullName: PropTypes.string,
  email: PropTypes.string,
  gender: PropTypes.string,
  phoneNumber: PropTypes.string,
  roleName: PropTypes.string,
  status: PropTypes.bool,
  handleClick: PropTypes.func,
  onEdit: PropTypes.func,
  onDelete: PropTypes.func,
  selected: PropTypes.any,
};

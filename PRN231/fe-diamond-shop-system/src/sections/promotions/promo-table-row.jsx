import axios from 'axios';
import { useState } from 'react';
import { toast } from 'react-toastify';
import PropTypes from 'prop-types';

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

import PromotionEditForm from './promo-edit-modal';
import PromotionDeleteForm from './promo-del-modal';

export default function UserTableRow({
  selected,
  promotionId,
  type,
  description,
  discountRate,
  startDate,
  endDate,
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
      const res = await axios.put(`http://localhost:5188/api/Promotion/UpdatePromotion?id=${promotionId}`, updatedData, config);
      if (res.status === 200) {
        toast.success("Edit promotion success");
      } else {
        toast.error('Edit promotion fail');
      }
      handleEditClose();
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
    handleCloseMenu();
  };

  const onDelete = async () => {
    const token = localStorage.getItem('TOKEN');
    const config = {
      headers: {
        Authorization: `Bearer ${token}`
      }
    };
    try {
      const res = await axios.delete(`http://localhost:5188/api/Promotion/DeletePromotion?id=${promotionId}`, config);
      if (res.data === 1) {
        toast.success("Delete success");
      } else {
        toast.error("Delete fail");
      }
      handleDeleteClose();
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
        
        <TableCell>{type}</TableCell>
        <TableCell>{`${discountRate}%`}</TableCell>
        <TableCell>{startDate}</TableCell>
        <TableCell>{endDate}</TableCell>

        <TableCell align='right'>
          <Button variant="outlined" onClick={handleDialogOpen}>
            More Info
          </Button>
          <IconButton onClick={handleOpenMenu}>
            <Iconify icon="eva:more-vertical-fill" />
          </IconButton>
        </TableCell>
      </TableRow>

      <Dialog open={dialogOpen} onClose={handleDialogClose}>
        <DialogTitle>Promotion</DialogTitle>
        <DialogContent>
          <Grid container spacing={2}>
            <Grid item xs={12}>
              <Typography variant="h6">Promotion ID:</Typography>
              <Typography>{promotionId}</Typography>
            </Grid>
            <Grid item xs={12}>
              <Typography variant="h6">Type:</Typography>
              <Typography>{type}</Typography>
            </Grid>
            <Grid item xs={12}>
              <Typography variant="h6">DiscountRate:</Typography>
              <Typography>{`${discountRate}%`}</Typography>
            </Grid>
            <Grid item xs={12}>
              <Typography variant="h6">Start Date:</Typography>
              <Typography>{startDate}</Typography>
            </Grid>
            <Grid item xs={12}>
              <Typography variant="h6">End Date:</Typography>
              <Typography>{endDate}</Typography>
            </Grid>
            <Grid item xs={12}>
              <Typography variant="h6">Description:</Typography>
              <Typography>{description}</Typography>
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

      <PromotionEditForm
        open={editOpen}
        onClose={handleEditClose}
        promotion={{ promotionId, type, discountRate, startDate, endDate, description }}
        onSubmit={onSubmit}
      />

      <PromotionDeleteForm
        open={deleteOpen}
        onClose={handleDeleteClose}
        onDelete={onDelete}
        promotion={{ promotionId, type, discountRate, startDate, endDate, description }}
      />
    </>
  );
}

UserTableRow.propTypes = {
  promotionId: PropTypes.any,
  type: PropTypes.string,
  description: PropTypes.string,
  handleClick: PropTypes.func,
  discountRate: PropTypes.number,
  startDate: PropTypes.string,
  endDate: PropTypes.string,
  selected: PropTypes.any,
};

import { useState } from 'react';
import PropTypes from 'prop-types';
import Button from 'react-bootstrap/Button';
import Stack from '@mui/material/Stack';
import Popover from '@mui/material/Popover';
import TableRow from '@mui/material/TableRow';
import Checkbox from '@mui/material/Checkbox';
import MenuItem from '@mui/material/MenuItem';
import TableCell from '@mui/material/TableCell';
import Typography from '@mui/material/Typography';
import IconButton from '@mui/material/IconButton';
import Label from 'src/components/label';
import Iconify from 'src/components/iconify';
import InfoModal from './user-modal';
import DelModal from './user-del-modal';
import EditModal from './user-edit-modal';

export default function UserTableRow({ userData, selected, handleClick, onDelete, onUpdate }) {
  const [open, setOpen] = useState(null);
  const [showDel, setShowDel] = useState(false);
  const [showEd, setShowEd] = useState(false);
  const [show, setShow] = useState(false);

  const handleCloseDel = () => setShowDel(false);
  const handleShowDel = () => setShowDel(true);
  const handleCloseEd = () => setShowEd(false);
  const handleShowEd = () => setShowEd(true);
  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);

  const handleOpenMenu = (event) => {
    setOpen(event.currentTarget);
  };

  const handleCloseMenu = () => {
    setOpen(null);
  };

  return (
    <>
      <TableRow hover tabIndex={-1} role="checkbox" selected={selected}>
        <TableCell padding="checkbox">
          <Checkbox disableRipple checked={selected} onChange={handleClick} />
        </TableCell>
        <TableCell component="th" scope="row" padding="none">
          <Stack direction="row" alignItems="center" spacing={2}>
            <Typography variant="subtitle2" noWrap>
              {userData.username}
            </Typography>
          </Stack>
        </TableCell>
        <TableCell>{userData.email}</TableCell>
        <TableCell>{userData.counterNumber}</TableCell>
        <TableCell>
          <Label
            color={
              (userData.roleName === 'Admin' && 'primary') ||
              (userData.roleName === 'Manager' && 'secondary') ||
              (userData.roleName === 'Staff' && 'info') ||
              'success'
            }
          >
            {userData.roleName}
          </Label>
        </TableCell>
        <TableCell>
          <Label color={userData.status ? 'success' : 'error'}>
            {userData.status ? 'Active' : 'Inactive'}
          </Label>
        </TableCell>
        <TableCell align="right">
          <Button variant="outline-primary" onClick={handleShow}>
            More Info
          </Button>
          {/* {userData.roleName !== 'Admin' && ( */}
            <IconButton onClick={handleOpenMenu}>
              <Iconify icon="eva:more-vertical-fill" />
            </IconButton>
          {/* )} */}
        </TableCell>
      </TableRow>

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
        <MenuItem
          onClick={() => {
            handleCloseMenu();
            handleShowEd();
          }}
          disabled={userData.roleName === 'Admin'}
        >
          <Iconify icon="eva:edit-fill" sx={{ mr: 2 }} />
          Edit
        </MenuItem>

        <MenuItem
          onClick={() => {
            handleCloseMenu();
            handleShowDel();
          }}
          sx={{ color: 'error.main' }}
          disabled={userData.roleName === 'Admin'}
        >
          <Iconify icon="eva:trash-2-outline" sx={{ mr: 2 }} />
          Delete
        </MenuItem>
      </Popover>

      <DelModal
        show={showDel}
        handleClose={handleCloseDel}
        userData={userData}
        onDelete={onDelete}
      />

      <InfoModal show={show} handleClose={handleClose} userData={userData} />
      
      <EditModal
        show={showEd}
        handleClose={handleCloseEd}
        userData={userData}
        onUpdate={onUpdate}
      />
    </>
  );
}

UserTableRow.propTypes = {
  userData: PropTypes.shape({
    userId: PropTypes.string,
    username: PropTypes.string,
    email: PropTypes.string,
    counterNumber: PropTypes.number,
    roleName: PropTypes.string,
    status: PropTypes.bool,
  }).isRequired,
  selected: PropTypes.bool,
  handleClick: PropTypes.func.isRequired,
  onDelete: PropTypes.func.isRequired,
  onUpdate: PropTypes.func.isRequired,
};

import { useState } from 'react';
import PropTypes from 'prop-types';
import Button from 'react-bootstrap/Button';

import Stack from '@mui/material/Stack';
import Popover from '@mui/material/Popover';
import TableRow from '@mui/material/TableRow';
import Checkbox from '@mui/material/Checkbox';
import MenuItem from '@mui/material/MenuItem';
import TableCell from '@mui/material/TableCell';
//import Typography from '@mui/material/Typography';
import IconButton from '@mui/material/IconButton';
import Iconify from 'src/components/iconify';
import Label from 'src/components/label';

import InfoModal from './jew-modal';
import DelModal from './jew-del-modal';
import EditModal from './jew-edit-modal';

// ----------------------------------------------------------------------

export default function UserTableRow({
  id,
  selected,
  name,
  imageUrl,
  type,
  barcode,
  laborCost,
  jewelryPrice,
  isSold,
  handleClick,
  onDelete,
  onUpdate,
}) {
  const [open, setOpen] = useState(null);
  const [showDel, setShowDel] = useState(false);

  const handleCloseDel = () => setShowDel(false);
  const handleShowDel = () => setShowDel(true);

  const [showEd, setShowEd] = useState(false);

  const handleCloseEd = () => setShowEd(false);
  const handleShowEd = () => setShowEd(true);

  const [show, setShow] = useState(false);

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
            <img src={imageUrl} alt={name} width={150} height={150} />
            {/* <Typography variant="subtitle2" noWrap>
              {name}
            </Typography> */}
          </Stack>
        </TableCell>

        <TableCell>{name}</TableCell>
        <TableCell >{type}</TableCell>
        {/* <TableCell>{laborCost}</TableCell> */}
        <TableCell>{barcode}</TableCell>
        <TableCell>
          <Label color={isSold ? 'error' : 'success'}>{isSold ? 'Sold out' : 'Availability'}</Label>
          {/* {isSold ? 'Available' : 'Sold out'} */}
        </TableCell>

        <TableCell align="right">
          <Button variant="outline-primary" onClick={handleShow}>More Info</Button>
          <IconButton onClick={handleOpenMenu}>
            <Iconify icon="eva:more-vertical-fill" />
          </IconButton>
        </TableCell>
      </TableRow>

      <Popover
        open={!!open}
        anchorEl={open}
        onClose={handleCloseMenu}
        anchorOrigin={{ vertical: 'top', horizontal: 'left' }}
        transformOrigin={{ vertical: 'top', horizontal: 'right' }}
        PaperProps={{ sx: { width: 140 } }}
      >
        <MenuItem onClick={() => { handleCloseMenu(); handleShowEd(); }}>
          <Iconify icon="eva:edit-fill" sx={{ mr: 2 }} />
          Edit
        </MenuItem>

        <MenuItem onClick={() => { handleCloseMenu(); handleShowDel(); }} sx={{ color: 'error.main' }}>
          <Iconify icon="eva:trash-2-outline" sx={{ mr: 2 }} />
          Delete
        </MenuItem>
      </Popover>

      <InfoModal show={show} handleClose={handleClose} name={name} laborCost={laborCost} jewelryPrice={jewelryPrice} barcode={barcode} imageUrl={imageUrl} type={type} isSold={isSold} />

      <DelModal show={showDel} handleClose={handleCloseDel} name={name} onDelete={onDelete} />

      <EditModal show={showEd} handleClose={handleCloseEd} id={id} name={name} laborCost={laborCost} jewelryPrice={jewelryPrice} barcode={barcode} onUpdate={onUpdate} />
    </>
  );
}

UserTableRow.propTypes = {
  id: PropTypes.any,
  name: PropTypes.any,
  imageUrl: PropTypes.string,
  type: PropTypes.any,
  barcode: PropTypes.any,
  laborCost: PropTypes.any,
  jewelryPrice: PropTypes.any,
  isSold: PropTypes.bool,
  handleClick: PropTypes.func,
  onDelete: PropTypes.func,
  onUpdate: PropTypes.func,
  selected: PropTypes.any,
};

import { useState } from 'react';
import PropTypes from 'prop-types';
//import Popover from '@mui/material/Popover';
import TableRow from '@mui/material/TableRow';
import Checkbox from '@mui/material/Checkbox';
//import MenuItem from '@mui/material/MenuItem';
import TableCell from '@mui/material/TableCell';
//import IconButton from '@mui/material/IconButton';
import Button from '@mui/material/Button';
//import Iconify from 'src/components/iconify';
import BillDetailsDialog from './bill-details-dialog';

export default function BillTableRow({ selected, bill, handleClick }) {
  //const [open, setOpen] = useState(null);
  const [showDetails, setShowDetails] = useState(false);

  // const handleOpenMenu = (event) => {
  //   setOpen(event.currentTarget);
  // };

  // const handleCloseMenu = () => {
  //   setOpen(null);
  // };

  const handleDetailsClose = () => {
    setShowDetails(false);
  };

  const handleDetailsShow = () => {
    setShowDetails(true);
  };

  return (
    <>
      <TableRow hover tabIndex={-1} role="checkbox" selected={selected}>
        <TableCell padding="checkbox">
          <Checkbox disableRipple checked={selected} onChange={handleClick} />
        </TableCell>
        <TableCell>{bill.staffName}</TableCell>
        <TableCell>{bill.customerName}</TableCell>
        <TableCell>{bill.totalAmount}</TableCell>
        <TableCell>{bill.totalDiscount} %</TableCell>
        <TableCell>{new Date(bill.saleDate).toLocaleString()}</TableCell>
        <TableCell>{bill.items.map(item => item.name).join(', ')}</TableCell>
        <TableCell>{bill.finalAmount}</TableCell>
        <TableCell align="right">
          <Button variant="outlined" onClick={handleDetailsShow}>
            More Info
          </Button>
          {/* <IconButton onClick={handleOpenMenu}>
            <Iconify icon="eva:more-vertical-fill" />
          </IconButton> */}
        </TableCell>
      </TableRow>

      {/* <Popover
        open={!!open}
        anchorEl={open}
        onClose={handleCloseMenu}
        anchorOrigin={{ vertical: 'top', horizontal: 'left' }}
        transformOrigin={{ vertical: 'top', horizontal: 'right' }}
        PaperProps={{
          sx: { width: 140 },
        }}
      >
        <MenuItem onClick={handleCloseMenu}>
          <Iconify icon="eva:edit-fill" sx={{ mr: 2 }} />
          Edit
        </MenuItem>

        <MenuItem onClick={handleCloseMenu} sx={{ color: 'error.main' }}>
          <Iconify icon="eva:trash-2-outline" sx={{ mr: 2 }} />
          Delete
        </MenuItem>
      </Popover> */}

      <BillDetailsDialog
        show={showDetails}
        handleClose={handleDetailsClose}
        bill={bill}
      />
    </>
  );
}

BillTableRow.propTypes = {
  bill: PropTypes.shape({
    billId: PropTypes.string.isRequired,
    customerName: PropTypes.string.isRequired,
    staffName: PropTypes.string.isRequired,
    totalAmount: PropTypes.number.isRequired,
    totalDiscount: PropTypes.number.isRequired,
    saleDate: PropTypes.string.isRequired,
    items: PropTypes.arrayOf(
      PropTypes.shape({
        name: PropTypes.string.isRequired,
      })
    ).isRequired,
    finalAmount: PropTypes.number.isRequired,
  }).isRequired,
  selected: PropTypes.bool,
  handleClick: PropTypes.func.isRequired,
};

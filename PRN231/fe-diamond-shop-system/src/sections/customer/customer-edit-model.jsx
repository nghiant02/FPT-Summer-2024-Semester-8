import React from 'react';
import PropTypes from 'prop-types';

import Dialog from '@mui/material/Dialog';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import DialogTitle from '@mui/material/DialogTitle';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';


function CustomerEditForm({ open, onClose, onSubmit, customer }) {
  const [formState, setFormState] = React.useState({
    userName: customer ? customer.userName : '',
    fullName: customer ? customer.fullName : '',
    email: customer ? customer.email : '',
    phone: customer ? customer.phone : '',
    gender: customer ? customer.gender : '',
    address: customer ? customer.address : '',
    point: customer ? customer.point : 0,
  });

  React.useEffect(() => {
    if (customer) {
      setFormState({
        userName: customer.userName,
        fullName: customer.fullName,
        email: customer.email,
        phone: customer.phone,
        gender: customer.gender,
        address: customer.address,
        point: customer.point,
      });
    }
  }, [customer]);

  const handleChange = (event) => {
    setFormState({ ...formState, [event.target.name]: event.target.value });
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    onSubmit(formState);
    onClose();
  };

  return (
    <Dialog open={open} onClose={onClose} aria-labelledby="form-dialog-title">
      <DialogTitle id="form-dialog-title">Edit Customer</DialogTitle>
      <DialogContent>
        <TextField
          margin="dense"
          name="userName"
          label="Username"
          value={formState.userName}
          type="text"
          fullWidth
          onChange={handleChange}
          InputProps={{ style: { marginBottom: 10 } }}
        />
        <TextField
          margin="dense"
          name="fullName"
          label="Full Name"
          value={formState.fullName}
          type="text"
          fullWidth
          onChange={handleChange}
          InputProps={{ style: { marginBottom: 10 } }}
        />
        <TextField
          margin="dense"
          name="email"
          label="Email"
          value={formState.email}
          type="text"
          fullWidth
          onChange={handleChange}
          InputProps={{ style: { marginBottom: 10 } }}
        />
        <TextField
          margin="dense"
          name="phone"
          label="Phone"
          value={formState.phone}
          type="text"
          fullWidth
          onChange={handleChange}
          InputProps={{ style: { marginBottom: 10 } }}
        />
        <TextField
          margin="dense"
          name="gender"
          label="Gender"
          value={formState.gender}
          type="text"
          fullWidth
          onChange={handleChange}
          InputProps={{ style: { marginBottom: 10 } }}
        />
        <TextField
          margin="dense"
          name="address"
          label="Address"
          value={formState.address}
          type="text"
          fullWidth
          onChange={handleChange}
          InputProps={{ style: { marginBottom: 10 } }}
        />
        <TextField
          margin="dense"
          name="point"
          label="Point"
          value={formState.point}
          type="number"
          fullWidth
          onChange={handleChange}
          InputProps={{ style: { marginBottom: 10 } }}
        />
      </DialogContent>
      <DialogActions>
        <Button onClick={onClose}>Cancel</Button>
        <Button onClick={handleSubmit}>Submit</Button>
      </DialogActions>
    </Dialog>
  );
}

CustomerEditForm.propTypes = {
  open: PropTypes.bool.isRequired,
  onClose: PropTypes.func.isRequired,
  onSubmit: PropTypes.func.isRequired,
  customer: PropTypes.shape({
    userName: PropTypes.string,
    fullName: PropTypes.string,
    email: PropTypes.string,
    phone: PropTypes.string,
    gender: PropTypes.string,
    address: PropTypes.string,
    point: PropTypes.number,
  }),
};

export default CustomerEditForm;

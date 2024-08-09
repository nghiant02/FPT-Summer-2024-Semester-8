import React, { useState } from 'react';
import PropTypes from 'prop-types';
import Dialog from '@mui/material/Dialog';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import DialogTitle from '@mui/material/DialogTitle';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import Autocomplete from '@mui/material/Autocomplete';

function PromotionForm({ open, onClose, onSubmit, managers }) {
  const initialFormState = {
    type: '',
    discountRate: '',
    startDate: '',
    endDate: '',
    userId: '', // This will hold the userId of the selected manager
    description: '',
  };

  const [formState, setFormState] = useState(initialFormState);

  const handleChange = (e) => {
    setFormState({ ...formState, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    onSubmit(formState);
    setFormState(initialFormState);
    onClose();
  };

  return (
    <Dialog open={open} onClose={onClose} aria-labelledby="form-dialog-title">
      <DialogTitle id="form-dialog-title">New Promotion</DialogTitle>
      <DialogContent>
        <TextField
          margin="dense"
          name="type"
          label="Type"
          type="text"
          fullWidth
          onChange={handleChange}
          value={formState.type}
          InputProps={{ style: { marginBottom: 10 } }}
        />
        <TextField
          margin="dense"
          name="discountRate"
          label="Discount Rate"
          type="number"
          fullWidth
          onChange={handleChange}
          value={formState.discountRate}
          InputProps={{ style: { marginBottom: 10 } }}
        />
        <TextField
          margin="dense"
          name="startDate"
          label="Start Date"
          type="date"
          fullWidth
          InputLabelProps={{ shrink: true }}
          onChange={handleChange}
          value={formState.startDate}
          InputProps={{ style: { marginBottom: 10 } }}
        />
        <TextField
          margin="dense"
          name="endDate"
          label="End Date"
          type="date"
          fullWidth
          InputLabelProps={{ shrink: true }}
          onChange={handleChange}
          value={formState.endDate}
          InputProps={{ style: { marginBottom: 10 } }}
        />
        <Autocomplete
          options={managers}
          getOptionLabel={(option) => option.username}
          onChange={(event, value) => setFormState({ ...formState, userId: value ? value.userId : '' })}
          renderInput={(params) => (
            <TextField
              {...params}
              margin="dense"
              name="userId"
              label="User ID"
              type="text"
              fullWidth
              InputProps={{ ...params.InputProps, style: { marginBottom: 10 } }}
            />
          )}
        />
        <TextField
          margin="dense"
          name="description"
          label="Description"
          type="text"
          fullWidth
          onChange={handleChange}
          value={formState.description}
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

PromotionForm.propTypes = {
  open: PropTypes.bool.isRequired,
  onClose: PropTypes.func.isRequired,
  onSubmit: PropTypes.func.isRequired,
  managers: PropTypes.array.isRequired,
};

export default PromotionForm;

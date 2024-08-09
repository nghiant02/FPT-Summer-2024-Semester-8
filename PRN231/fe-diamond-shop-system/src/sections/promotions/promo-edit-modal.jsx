import React from 'react';
import PropTypes from 'prop-types';

import Dialog from '@mui/material/Dialog';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import DialogTitle from '@mui/material/DialogTitle';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';

function PromotionEditForm({ open, onClose, onSubmit, promotion }) {
  const [formState, setFormState] = React.useState({
    type: promotion ? promotion.type : '',
    discountRate: promotion ? promotion.discountRate : '',
    startDate: promotion.startDate ? new Date(promotion.startDate).toISOString().split('T')[0] : '',
    endDate:promotion.endDate ? new Date(promotion.endDate).toISOString().split('T')[0] : '',
    description: promotion ? promotion.description : '',
  });

  React.useEffect(() => {
    if (promotion) {
      setFormState({
        type: promotion.type,
        discountRate: promotion.discountRate,
        startDate: promotion.startDate ? new Date(promotion.startDate).toISOString().split('T')[0] : '',
        endDate: promotion.endDate ? new Date(promotion.endDate).toISOString().split('T')[0] : '',
        description: promotion.description,
      });
    }
  }, [promotion]);

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
      <DialogTitle id="form-dialog-title">Edit Promotion</DialogTitle>
      <DialogContent>
        <TextField
          margin="dense"
          name="type"
          label="Type"
          value={formState.type}
          type="text"
          fullWidth
          onChange={handleChange}
          InputProps={{ style: { marginBottom: 10 } }}
        />
        <TextField
          margin="dense"
          name="discountRate"
          label="Discount Rate"
          value={formState.discountRate}
          type="text"
          fullWidth
          onChange={handleChange}
          InputProps={{ style: { marginBottom: 10 } }}
        />
        <TextField
          margin="dense"
          name="startDate"
          label="Start Date"
          value={formState.startDate}
          type="date"
          fullWidth
          onChange={handleChange}
          InputProps={{ style: { marginBottom: 10 } }}
        />
        <TextField
          margin="dense"
          name="endDate"
          value={formState.endDate}
          label="End Date"
          type="date"
          fullWidth
          onChange={handleChange}
          InputProps={{ style: { marginBottom: 10 } }}
        />
        <TextField
          margin="dense"
          name="description"
          label="Description"
          value={formState.description}
          type="text"
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

PromotionEditForm.propTypes = {
  open: PropTypes.bool.isRequired,
  onClose: PropTypes.func.isRequired,
  onSubmit: PropTypes.func.isRequired,
  promotion: PropTypes.shape({
    promotionId: PropTypes.any,
    type: PropTypes.string,
    discountRate: PropTypes.number,
    startDate: PropTypes.string,
    endDate: PropTypes.string,
    description: PropTypes.string,
  }),
};

export default PromotionEditForm;

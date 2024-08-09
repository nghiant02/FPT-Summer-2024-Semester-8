import React, { useState } from 'react';
import PropTypes from 'prop-types';
import Dialog from '@mui/material/Dialog';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import DialogTitle from '@mui/material/DialogTitle';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import { InputLabel, FormControl, Select, MenuItem } from '@mui/material';

function StaffForm({ open, onClose, onSubmit }) {
    const initialFormState = {
        username: '',
        fullName: '',
        email: '',
        gender: '',
        password: '',
        roleId: ''
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
            <DialogTitle id="form-dialog-title">New Staff</DialogTitle>
            <DialogContent>
                <TextField
                    margin="dense"
                    name="username"
                    label="User Name"
                    type="text"
                    fullWidth
                    onChange={handleChange}
                    value={formState.username}
                />
                <TextField
                    margin="dense"
                    name="fullName"
                    label="Full Name"
                    type="text"
                    fullWidth
                    onChange={handleChange}
                    value={formState.fullName}
                />
                <TextField
                    margin="dense"
                    name="email"
                    label="Email"
                    type="email"
                    fullWidth
                    onChange={handleChange}
                    value={formState.email}
                />
                <TextField
                    margin="dense"
                    name="gender"
                    label="Gender"
                    type="text"
                    fullWidth
                    onChange={handleChange}
                    value={formState.gender}
                />
                <TextField
                    margin="dense"
                    name="password"
                    label="Password"
                    type="password"
                    fullWidth
                    onChange={handleChange}
                    value={formState.password}
                />
                <FormControl fullWidth margin="dense">
                    <InputLabel id="role-label">Role ID</InputLabel>
                    <Select
                        labelId="role-label"
                        name="roleId"
                        label="Role ID"
                        value={formState.roleId}
                        onChange={handleChange}
                    >
                        <MenuItem value="2">Manager</MenuItem>
                        <MenuItem value="3">Staff</MenuItem>
                    </Select>
                </FormControl>
            </DialogContent>
            <DialogActions>
                <Button onClick={onClose}>Cancel</Button>
                <Button onClick={handleSubmit}>Submit</Button>
            </DialogActions>
        </Dialog>
    );
}

StaffForm.propTypes = {
    open: PropTypes.bool.isRequired,
    onClose: PropTypes.func.isRequired,
    onSubmit: PropTypes.func.isRequired,
};

export default StaffForm;

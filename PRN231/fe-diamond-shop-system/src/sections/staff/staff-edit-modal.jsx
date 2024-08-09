import React from 'react';
import PropTypes from 'prop-types';
import { useState, useEffect } from 'react';
import Dialog from '@mui/material/Dialog';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import DialogTitle from '@mui/material/DialogTitle';
import DialogActions from '@mui/material/DialogActions';
import DialogContent from '@mui/material/DialogContent';
import { InputLabel, FormControl, Select, MenuItem } from '@mui/material';

function UserEditForm({ open, onClose, onSubmit, user }) {
    const [formState, setFormState] = useState({
        username: '',
        fullName: '',
        email: '',
        gender: '',
        status: false,
        roleId: 1
    });

    useEffect(() => {
        if (user) {
            setFormState({
                username: user.username,
                fullName: user.fullName,
                email: user.email,
                gender: user.gender,
                status: user.status,
                roleId: user.roleId
            });
        }
    }, [user]);

    const handleChange = (event) => {
        setFormState({ ...formState, [event.target.name]: event.target.value });
    };

    const handleSubmit = (event) => {
        event.preventDefault();
        onSubmit(user.userId, formState); // Assuming userId is the unique identifier
        onClose();
    };

    return (
        <Dialog open={open} onClose={onClose} aria-labelledby="form-dialog-title">
            <DialogTitle id="form-dialog-title">Edit User</DialogTitle>
            <DialogContent>
                <TextField
                    margin="dense"
                    name="username"
                    label="User Name"
                    type="text"
                    fullWidth
                    value={formState.username}
                    onChange={handleChange}
                />
                <TextField
                    margin="dense"
                    name="fullName"
                    label="Full Name"
                    type="text"
                    fullWidth
                    value={formState.fullName}
                    onChange={handleChange}
                />
                <TextField
                    margin="dense"
                    name="email"
                    label="Email"
                    type="email"
                    fullWidth
                    value={formState.email}
                    onChange={handleChange}
                />
                <TextField
                    margin="dense"
                    name="gender"
                    label="Gender"
                    type="text"
                    fullWidth
                    value={formState.gender}
                    onChange={handleChange}
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
                        {/* <MenuItem value={1}>Admin</MenuItem> */}
                        <MenuItem value={2}>Manager</MenuItem>
                        <MenuItem value={3}>Staff</MenuItem>
                    </Select>
                </FormControl>
                <FormControl fullWidth margin="dense">
                    <InputLabel id="status-label">Status</InputLabel>
                    <Select
                        labelId="status-label"
                        name="status"
                        label="Status"
                        value={formState.status}
                        onChange={handleChange}
                    >
                        <MenuItem value={true}>Active</MenuItem>
                        <MenuItem value={false}>Inactive</MenuItem>
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

UserEditForm.propTypes = {
    open: PropTypes.bool.isRequired,
    onClose: PropTypes.func.isRequired,
    onSubmit: PropTypes.func.isRequired,
    user: PropTypes.shape({
        userId: PropTypes.string.isRequired,
        username: PropTypes.string,
        fullName: PropTypes.string,
        email: PropTypes.string,
        gender: PropTypes.string,
        status: PropTypes.bool,
        roleId: PropTypes.number,
    }).isRequired,
};

export default UserEditForm;

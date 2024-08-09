import React from 'react';
import PropTypes from 'prop-types';

import { Grid, Button, Dialog, Typography, DialogTitle, DialogContent, DialogActions } from '@mui/material';

export default function StaffDeleteForm({ open, onClose, onDelete, staff }) {
    const handleDeleteClick = () => {
        onDelete(staff.userId);
    };

    return (
        <Dialog open={open} onClose={onClose}>
            <DialogTitle>Delete Staff</DialogTitle>
            <DialogContent>
                <Grid container spacing={2}>
                    <Grid item xs={12}>
                        <Typography variant="h6">User ID:</Typography>
                        <Typography>{staff.userId}</Typography>
                    </Grid>
                    <Grid item xs={12}>
                        <Typography variant="h6">Username:</Typography>
                        <Typography>{staff.username}</Typography>
                    </Grid>
                    <Grid item xs={12}>
                        <Typography variant="h6">Full Name:</Typography>
                        <Typography>{staff.fullName}</Typography>
                    </Grid>
                    <Grid item xs={12}>
                        <Typography variant="h6">Email:</Typography>
                        <Typography>{staff.email}</Typography>
                    </Grid>
                    <Grid item xs={12}>
                        <Typography variant="h6">Gender:</Typography>
                        <Typography>{staff.gender}</Typography>
                    </Grid>
                    <Grid item xs={12}>
                        <Typography variant="h6">Password:</Typography>
                        <Typography>{staff.password}</Typography>
                    </Grid>
                    <Grid item xs={12}>
                        <Typography variant="h6">Role ID:</Typography>
                        <Typography>{staff.roleId}</Typography>
                    </Grid>
                </Grid>
            </DialogContent>
            <DialogActions>
                <Button variant="secondary" onClick={onClose}>
                    Cancel
                </Button>
                <Button variant='contained' onClick={handleDeleteClick} color="error">
                    Delete
                </Button>
            </DialogActions>
        </Dialog>
    );
}

StaffDeleteForm.propTypes = {
    open: PropTypes.bool.isRequired,
    onClose: PropTypes.func.isRequired,
    onDelete: PropTypes.func.isRequired,
    staff: PropTypes.shape({
        userId: PropTypes.string,
        username: PropTypes.string,
        fullName: PropTypes.string,
        email: PropTypes.string,
        gender: PropTypes.string,
        password: PropTypes.string,
        roleId: PropTypes.string
    }).isRequired,
};

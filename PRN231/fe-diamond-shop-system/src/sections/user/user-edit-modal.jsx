import PropTypes from 'prop-types';
import React, { useState, useEffect } from 'react';
import Form from 'react-bootstrap/Form';
import InputGroup from 'react-bootstrap/InputGroup';
import { Row, Col, Modal, Button } from 'react-bootstrap';
import TextField from '@mui/material/TextField';
import MenuItem from '@mui/material/MenuItem';
import Select from '@mui/material/Select';
import FormControl from '@mui/material/FormControl';
import InputLabel from '@mui/material/InputLabel';

export default function EditModal({ show, handleClose, userData, onUpdate }) {
    const [formData, setFormData] = useState({
        roleId: '',
        username: '',
        fullName: '',
        gender: '',
        email: '',
        status: true,
    });

    useEffect(() => {
        setFormData({
            roleId: userData.roleId || '',
            username: userData.username || '',
            fullName: userData.fullName || '',
            gender: userData.gender || '',
            email: userData.email || '',
            status: userData.status || true,
        });
    }, [userData]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData(prevState => ({
            ...prevState,
            [name]: value
        }));
    };

    const handleSubmit = (event) => {
        event.preventDefault();
        onUpdate(userData.userId, formData);
        handleClose();
    };

    return (
        <Modal size="lg" show={show} onHide={handleClose} aria-labelledby="contained-modal-title-vcenter" centered>
            <Modal.Header closeButton>
                <Modal.Title>Edit User</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form onSubmit={handleSubmit}>
                    <Row>
                        <Col md={6} className="me-5 ms-3">
                            <InputGroup className="mb-4 mt-4">
                                <TextField 
                                    id="fullWidth" 
                                    label="Full Name" 
                                    variant="outlined" 
                                    name='fullName' 
                                    value={formData.fullName} 
                                    onChange={handleChange} 
                                    sx={{
                                        width: 300,
                                        '& .MuiOutlinedInput-root': {
                                            '& fieldset': {
                                                borderColor: 'gray',
                                            },
                                        }
                                    }} 
                                />
                            </InputGroup>
                            <InputGroup className="mb-4 mt-4">
                                <TextField 
                                    id="fullWidth" 
                                    label="Username" 
                                    variant="outlined" 
                                    name='username' 
                                    value={formData.username} 
                                    onChange={handleChange} 
                                    sx={{
                                        width: 300,
                                        '& .MuiOutlinedInput-root': {
                                            '& fieldset': {
                                                borderColor: 'gray',
                                            },
                                        }
                                    }} 
                                />
                            </InputGroup>
                            <InputGroup className="mb-4 mt-4">
                                <TextField 
                                    id="fullWidth" 
                                    label="Email" 
                                    variant="outlined" 
                                    name='email' 
                                    value={formData.email} 
                                    onChange={handleChange} 
                                    sx={{
                                        width: 300,
                                        '& .MuiOutlinedInput-root': {
                                            '& fieldset': {
                                                borderColor: 'gray',
                                            },
                                        }
                                    }} 
                                />
                            </InputGroup>
                        </Col>
                        <Col md={6}>
                            <InputGroup className="mb-4 mt-4">
                                <TextField 
                                    id="fullWidth" 
                                    label="Gender" 
                                    variant="outlined" 
                                    name='gender' 
                                    value={formData.gender} 
                                    onChange={handleChange} 
                                    sx={{
                                        width: 300,
                                        '& .MuiOutlinedInput-root': {
                                            '& fieldset': {
                                                borderColor: 'gray',
                                            },
                                        }
                                    }} 
                                />
                            </InputGroup>
                            <InputGroup className="mb-4 mt-4">
                                <FormControl sx={{ width: 300 }}>
                                    <InputLabel>Role</InputLabel>
                                    <Select
                                        label="Role"
                                        name="roleId"
                                        value={formData.roleId}
                                        onChange={handleChange}
                                    >
                                        <MenuItem value={1}>Admin</MenuItem>
                                        <MenuItem value={2}>Manager</MenuItem>
                                        <MenuItem value={3}>Staff</MenuItem>
                                    </Select>
                                </FormControl>
                            </InputGroup>
                            <InputGroup className="mb-4 mt-4">
                                <FormControl sx={{ width: 300 }}>
                                    <InputLabel>Status</InputLabel>
                                    <Select
                                        label="Status"
                                        name="status"
                                        value={formData.status}
                                        onChange={handleChange}
                                    >
                                        <MenuItem value={true}>Active</MenuItem>
                                        <MenuItem value={false}>Inactive</MenuItem>
                                    </Select>
                                </FormControl>
                            </InputGroup>
                        </Col>
                    </Row>
                </Form>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="secondary" onClick={handleClose}>
                    Close
                </Button>
                <Button variant="primary" onClick={handleSubmit}>Save changes</Button>
            </Modal.Footer>
        </Modal>
    );
}

EditModal.propTypes = {
    show: PropTypes.bool.isRequired,
    handleClose: PropTypes.func.isRequired,
    userData: PropTypes.shape({
        userId: PropTypes.string,
        username: PropTypes.string,
        fullName: PropTypes.string,
        gender: PropTypes.string,
        email: PropTypes.string,
        roleId: PropTypes.number,
        status: PropTypes.bool,
    }).isRequired,
    onUpdate: PropTypes.func.isRequired,
};

import React from 'react';
// import * as Yup from 'yup';
import PropTypes from 'prop-types';
import { useFormik } from 'formik';
import Form from 'react-bootstrap/Form';
import InputGroup from 'react-bootstrap/InputGroup';
import { Row, Col, Modal, Button } from 'react-bootstrap';

import Radio from '@mui/material/Radio';
import TextField from '@mui/material/TextField';
import FormLabel from '@mui/material/FormLabel';
import RadioGroup from '@mui/material/RadioGroup';
import FormControl from '@mui/material/FormControl';
import FormControlLabel from '@mui/material/FormControlLabel';

export default function NewModal({ show, handleClose, createUser }) {
  // const validationSchem = Yup.object().shape({
  //   username: Yup.string()
  //     .matches(
  //       /^[A-Za-z0-9]{8,20}$/,
  //       'Username must be 8-20 characters long, contain no special characters.'
  //     )
  //     .required('Username is required.'),
  //   password: Yup.string()
  //     .min(8, 'Password must be at least 8 characters long.')
  //     .matches(/[A-Z]/, 'Password must contain at least one uppercase letter.')
  //     .matches(/[a-z]/, 'Password must contain at least one lowercase letter.')
  //     .matches(/\d/, 'Password must contain at least one number.')
  //     .matches(/[!@#$%^&*(),.?":{}|<>]/, 'Password must contain at least one special character.')
  //     .required('Password is required.'),
  //   email: Yup.string()
  //     .matches(/^[\w-.]+@([\w-]+\.)+[\w-]{2,4}$/, 'Invalid email address.')
  //     .email('Invalid email address.')
  //     .required('Email is required.'),
  //   roleId: Yup.string().required('Role ID is required.'),
  //   counterId: Yup.string().required('Counter ID is required.'),
  //   role: Yup.string().required('Role is required.'),
  // });

  const formik = useFormik({
    initialValues: {
      fullname: '',
      roleId: '',
      email: '',
      password: '',
      gender:'',
    },
    onSubmit: (values) => {
      createUser(values);
      formik.resetForm(); // Clear the form after submission
      handleClose();
    },
  });

  return (
    <>
      <style type="text/css">
        {`
                    .custom-range::-webkit-slider-runnable-track {
                        background: gray; /* Black track */
                    }

                    .custom-range::-webkit-slider-thumb {
                        -webkit-appearance: none;
                        width: 16px;
                        height: 16px;
                        background: blue; /*  thumb */
                        cursor: pointer;
                        border-radius: 50%;
                        margin-top: -4px; /* Adjust based on thumb height */
                    }
                `}
      </style>
      <Modal
        size="lg"
        show={show}
        onHide={handleClose}
        aria-labelledby="contained-modal-title-vcenter"
        centered
      >
        <Form onSubmit={formik.handleSubmit}>
          <Modal.Header closeButton>
            <Modal.Title>Add New User</Modal.Title>
          </Modal.Header>
          <Modal.Body>
            <Row>
              <Col md={6} className="">
                <InputGroup className="mb-4 mt-4">
                  <TextField
                    id="fullWidth"
                    label="Fullname"
                    variant="outlined"
                    name="fullname"
                    value={formik.values.fullname}
                    onChange={formik.handleChange}
                    onBlur={formik.handleBlur}
                    // error={formik.touched.username && Boolean(formik.errors.username)}
                    // helperText={formik.touched.username && formik.errors.username}
                    required
                    sx={{
                      width: 300,
                      '& .MuiOutlinedInput-root': {
                        '& fieldset': {
                          borderColor: 'gray', // sets the border color
                        },
                      },
                    }}
                  />
                </InputGroup>

                <InputGroup className="mb-4 mt-4">
                  <TextField
                    id="fullWidth"
                    label="Password"
                    variant="outlined"
                    name="password"
                    type="password"
                    value={formik.values.password}
                    onChange={formik.handleChange}
                    onBlur={formik.handleBlur}
                    error={formik.touched.password && Boolean(formik.errors.password)}
                    helperText={formik.touched.password && formik.errors.password}
                    required
                    sx={{
                      width: 300,
                      '& .MuiOutlinedInput-root': {
                        '& fieldset': {
                          borderColor: 'gray', // sets the border color
                        },
                      },
                    }}
                  />
                </InputGroup>

                <InputGroup className="mb-4 mt-4">
                  <TextField
                    id="fullWidth"
                    label="Email"
                    variant="outlined"
                    name="email"
                    value={formik.values.email}
                    onChange={formik.handleChange}
                    onBlur={formik.handleBlur}
                    error={formik.touched.email && Boolean(formik.errors.email)}
                    helperText={formik.touched.email && formik.errors.email}
                    required
                    sx={{
                      width: 300,
                      '& .MuiOutlinedInput-root': {
                        '& fieldset': {
                          borderColor: 'gray', // sets the border color
                        },
                      },
                    }}
                  />
                </InputGroup>
              </Col>

              <Col md={6}>
                <InputGroup className="mb-4 mt-3 ms-5">
                  <FormControl>
                    <FormLabel>Role</FormLabel>
                    <RadioGroup
                      name="roleId"
                      value={formik.values.roleId}
                      onChange={formik.handleChange}
                      onBlur={formik.handleBlur}
                      style={{ flexDirection: 'row' }}
                    >
                      <FormControlLabel value="1" control={<Radio />} label="Admin" />
                      <FormControlLabel value="2" control={<Radio />} label="Manager" />
                      <FormControlLabel value="3" control={<Radio />} label="Staff" />
                    </RadioGroup>
                    {formik.touched.role && formik.errors.role && (
                      <p style={{ color: 'red' }}>{formik.errors.role}</p>
                    )}
                  </FormControl>
                  <FormControl>
                    <FormLabel id="demo-row-radio-buttons-group-label">Gender</FormLabel>
                    <RadioGroup
                      row
                      aria-labelledby="demo-row-radio-buttons-group-label"
                      name="gender"
                      value={formik.values.gender}
                      onChange={formik.handleChange}
                      onBlur={formik.onBlur}
                    >
                      <FormControlLabel value="female" control={<Radio />} label="Female" />
                      <FormControlLabel value="male" control={<Radio />} label="Male" />
                    </RadioGroup>
                  </FormControl>
                </InputGroup>
              </Col>
            </Row>
          </Modal.Body>

          <Modal.Footer>
            <Button variant="secondary" onClick={handleClose}>
              Close
            </Button>
            <Button variant="primary" type="submit">
              Add
            </Button>
          </Modal.Footer>
        </Form>
      </Modal>
    </>
  );
}

NewModal.propTypes = {
  show: PropTypes.bool.isRequired,
  handleClose: PropTypes.func.isRequired,
  createUser: PropTypes.func.isRequired,
};

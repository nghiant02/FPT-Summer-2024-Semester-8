import React, { useEffect, useState } from 'react';
import axios from 'axios';
import PropTypes from 'prop-types';
import { useFormik } from 'formik';
import Form from 'react-bootstrap/Form';
import InputGroup from 'react-bootstrap/InputGroup';
import { Row, Col, Modal, Button } from 'react-bootstrap';
import { Autocomplete } from '@mui/material';
import TextField from '@mui/material/TextField';
import InputAdornment from '@mui/material/InputAdornment';
import FormControl from '@mui/material/FormControl';

export default function EditModal({
    show,
    handleClose,
    name,
    goldweight,
    laborCost,
    barcode,
    onUpdate,
    goldprice,
    gemPrice,
    jewelryPrice,
    goldType,
    gemType,
    gemweight,
    totalPrice,
    type, // Nếu bạn sử dụng prop này
    jewelleryType, // Nếu bạn sử dụng prop này
}) {
    const [goldtype, setGoldtype] = useState([]);
    const [gemtype, setGemtype] = useState([]);
    const [jewelryTypes, setJewelryTypes] = useState([]);

    useEffect(() => {
        getGoldPrices();
        getGemPrices();
        getJewelleryTypes();
    }, []);

    const getGoldPrices = async () => {
        try {
            const response = await axios.get('http://localhost:5188/api/Price/GetGoldPrices');
            const types = response.data.map((item) => ({ label: item.type, value: item.goldId }));
            setGoldtype(types);
        } catch (error) {
            console.error('Error fetching gold prices:', error);
        }
    };

    const getGemPrices = async () => {
        try {
            const response = await axios.get('http://localhost:5188/api/Price/GetGemPrices');
            const types = response.data.map((item) => ({ label: item.type, value: item.gemId }));
            setGemtype(types);
        } catch (error) {
            console.error('Error fetching gem prices:', error);
        }
    };

    const getJewelleryTypes = async () => {
        try {
            const response = await axios.get('http://localhost:5188/api/JewelryType/GetJewelryTypes');
            const jewelryOptions = response.data.map((item) => ({ label: item.name, value: item.jewelryTypeId }));
            setJewelryTypes(jewelryOptions);
        } catch (error) {
            console.error('Error fetching jewellery types:', error);
        }
    };

    const formik = useFormik({
        initialValues: {
            jewelryTypeId: '',
            name: name || '',
            jewelryMaterial: {
                gemId: '',
                goldId: '',
                goldQuantity: goldweight || 50,
                gemQuantity: gemweight || 50,
            },
            barcode: barcode || '',
            laborCost: laborCost || '',
        },
        onSubmit: (values) => {
            onUpdate({
                ...values,
                jewelryMaterial: {
                    ...values.jewelryMaterial,
                    goldId: values.jewelryMaterial.goldId.value,
                    gemId: values.jewelryMaterial.gemId.value,
                },
            });
            handleClose();
        },
    });

    return (
        <Modal size="lg" show={show} onHide={handleClose}>
            <Modal.Header closeButton>
                <Modal.Title>Update New Jewellery</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form onSubmit={formik.handleSubmit}>
                    <Row>
                        <Col md={6}>
                            <InputGroup className="mt-4">
                                <TextField
                                    label="Jewellery Name"
                                    variant="outlined"
                                    name="name"
                                    value={formik.values.name}
                                    onChange={formik.handleChange}
                                    onBlur={formik.handleBlur}
                                    sx={{
                                        width: 300,
                                        '& .MuiOutlinedInput-root': {
                                            '& fieldset': {
                                                borderColor: 'gray',
                                            },
                                        },
                                    }}
                                />
                            </InputGroup>
                            <InputGroup className="mb-4 mt-5">
                                <FormControl fullWidth sx={{}} variant="standard">
                                    <TextField
                                        label="Labor Cost"
                                        InputProps={{
                                            startAdornment: <InputAdornment position="start">$</InputAdornment>,
                                        }}
                                        name="laborCost"
                                        value={formik.values.laborCost}
                                        onChange={formik.handleChange}
                                        onBlur={formik.handleBlur}
                                        sx={{
                                            maxWidth: 300,
                                            '& .MuiOutlinedInput-root': {
                                                '& fieldset': {
                                                    borderColor: 'gray',
                                                },
                                            },
                                        }}
                                    />
                                </FormControl>
                            </InputGroup>
                        </Col>
                        <Col md={6}>
                            <InputGroup className="mb-4 mt-3">
                                <FormControl fullWidth sx={{ m: 1 }} variant="standard">
                                    <TextField
                                        label="Barcode"
                                        InputProps={{
                                            startAdornment: <InputAdornment position="start">$</InputAdornment>,
                                        }}
                                        name="barcode"
                                        value={formik.values.barcode}
                                        onChange={formik.handleChange}
                                        onBlur={formik.handleBlur}
                                    />
                                </FormControl>
                            </InputGroup>
                        </Col>

                        <Col md={6}>
                            <InputGroup className="mb-4 mt-3">
                                <Form.Label>Gold Weight: {formik.values.jewelryMaterial.goldQuantity} grams</Form.Label>
                                <Form.Range
                                    className="custom-range"
                                    name="jewelryMaterial.goldQuantity"
                                    min={0}
                                    max={2000}
                                    step={1}
                                    value={formik.values.jewelryMaterial.goldQuantity}
                                    onChange={formik.handleChange}
                                    onBlur={formik.handleBlur}
                                    style={{ width: '100%' }}
                                />
                            </InputGroup>
                            <InputGroup className="mb-4 mt-3">
                                <Autocomplete
                                    disablePortal
                                    id="goldtype-autocomplete"
                                    options={goldtype}
                                    value={goldtype.find((option) => option.value === formik.values.jewelryMaterial.goldId)}
                                    onChange={(event, value) =>
                                        formik.setFieldValue('jewelryMaterial.goldId', value ? value.value : '')
                                    }
                                    onBlur={formik.handleBlur}
                                    sx={{ width: 300 }}
                                    renderInput={(params) => <TextField {...params} label="Gold Type" />}
                                />
                            </InputGroup>
                        </Col>

                        <Col md={6}>
                            <InputGroup className="mb-4 mt-3">
                                <Form.Label>Gem Weight: {formik.values.jewelryMaterial.gemQuantity} grams</Form.Label>
                                <Form.Range
                                    className="custom-range"
                                    name="jewelryMaterial.gemQuantity"
                                    min={0}
                                    max={2000}
                                    step={1}
                                    value={formik.values.jewelryMaterial.gemQuantity}
                                    onChange={formik.handleChange}
                                    onBlur={formik.handleBlur}
                                    style={{ width: '100%' }}
                                />
                            </InputGroup>
                            <InputGroup className="mb-4 mt-3">
                                <Autocomplete
                                    disablePortal
                                    id="gemtype-autocomplete"
                                    options={gemtype}
                                    value={gemtype.find((option) => option.value === formik.values.jewelryMaterial.gemId)}
                                    onChange={(event, value) =>
                                        formik.setFieldValue('jewelryMaterial.gemId', value ? value.value : '')
                                    }
                                    onBlur={formik.handleBlur}
                                    sx={{ width: 300 }}
                                    renderInput={(params) => <TextField {...params} label="Gem Type" />}
                                />
                            </InputGroup>
                        </Col>

                        <Col md={{ span: 6, offset: 6 }}>
                            <InputGroup className="mb-4 mt-3">
                                <Autocomplete
                                    disablePortal
                                    id="jewellerytype-autocomplete"
                                    options={jewelryTypes}
                                    value={jewelryTypes.find((option) => option.value === formik.values.jewelryTypeId) || null}
                                    onChange={(event, value) =>
                                        formik.setFieldValue('jewelryTypeId', value ? value.value : '')
                                    }
                                    onBlur={formik.handleBlur}
                                    sx={{ width: 300 }}
                                    renderInput={(params) => <TextField {...params} label="Jewellery Type" />}
                                />
                            </InputGroup>
                        </Col>
                    </Row>

                    <Modal.Footer>
                        <Button variant="secondary" onClick={handleClose}>
                            Close
                        </Button>
                        <Button variant="primary" type="submit">
                            Update
                        </Button>
                    </Modal.Footer>
                </Form>
            </Modal.Body>
        </Modal>
    );
}

EditModal.propTypes = {
    show: PropTypes.bool.isRequired,
    handleClose: PropTypes.func.isRequired,
    name: PropTypes.string, // Thay đổi thành string nếu name là string
    goldweight: PropTypes.number, // Thay đổi thành number nếu goldweight là number
    laborCost: PropTypes.number, // Thay đổi thành number nếu laborCost là number
    barcode: PropTypes.string, // Thay đổi thành string nếu barcode là string
    onUpdate: PropTypes.func.isRequired,
    goldprice: PropTypes.number,
    gemPrice: PropTypes.number,
    jewelryPrice: PropTypes.number,
    goldType: PropTypes.string,
    gemType: PropTypes.string,
    gemweight: PropTypes.number,
    totalPrice: PropTypes.number,
    type: PropTypes.string, // Nếu bạn sử dụng prop này
    jewelleryType: PropTypes.arrayOf(
        PropTypes.shape({
            label: PropTypes.string.isRequired,
            value: PropTypes.any.isRequired,
        }))}

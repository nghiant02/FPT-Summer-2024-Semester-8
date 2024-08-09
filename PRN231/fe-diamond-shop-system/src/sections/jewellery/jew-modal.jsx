import React from 'react';
import PropTypes from 'prop-types';
import { Row, Col, Modal, Button } from 'react-bootstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import {  faMoneyBill, faDollarSign, faWeightHanging} from '@fortawesome/free-solid-svg-icons'

export default function InfoModal({ show, handleClose, name, laborCost,goldprice,goldType,goldweight,gemweight,gemPrice,gemType,totalPrice,jewelryPrice,barcode }) {


  return (
    <Modal size="md" show={show} onHide={handleClose} aria-labelledby="contained-modal-title-vcenter" centered>
      <Modal.Header closeButton>
        <Modal.Title>{name}</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        
      <Row>
            <Col md={5}>
            <p><FontAwesomeIcon icon={faMoneyBill} /> &nbsp; jewelryPrice: {jewelryPrice}</p>
            <p><FontAwesomeIcon icon={faDollarSign} /> &nbsp; Labor Cost: {laborCost}</p>
            <p><FontAwesomeIcon icon={faDollarSign} /> &nbsp; Barcode: {barcode}</p>
            <p><FontAwesomeIcon icon={faWeightHanging}/> &nbsp; GoldWeight: {goldweight}</p>
            <p><FontAwesomeIcon icon={faWeightHanging}/> &nbsp; GemWeight: {gemweight}</p>
           
            </Col>
            <Col md={7}>
            <p><FontAwesomeIcon icon={faDollarSign} /> &nbsp; GoldType: {goldType}</p>
            <p><FontAwesomeIcon icon={faDollarSign} /> &nbsp; GoldPrice: {goldprice}</p>
            <p><FontAwesomeIcon icon={faDollarSign} /> &nbsp; GemType: {gemType}</p>
            <p><FontAwesomeIcon icon={faDollarSign} /> &nbsp; GemPrice: {gemPrice}</p>
            <p><FontAwesomeIcon icon={faDollarSign} /> &nbsp; TotalPrice: {totalPrice}</p>
           
            </Col>
          </Row>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={handleClose}>
          Close
        </Button>
      </Modal.Footer>
    </Modal>
  );
}

InfoModal.propTypes = {
  show: PropTypes.bool.isRequired,
  handleClose: PropTypes.func.isRequired,
  name: PropTypes.string.isRequired,
  laborCost: PropTypes.any.isRequired,
  goldType: PropTypes.string,
  goldprice: PropTypes.any,
  goldweight: PropTypes.any,
  gemType: PropTypes.string,
  gemweight: PropTypes.number,
  gemPrice: PropTypes.number,
  totalPrice: PropTypes.number,
  jewelryPrice: PropTypes.number,
  barcode: PropTypes.string,
};



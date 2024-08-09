import React from 'react';
import PropTypes from 'prop-types';
import { Row, Col, Modal, Button } from 'react-bootstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import {faBarcode, faMoneyBill, faDollarSign, faWeightHanging, faTriangleExclamation} from '@fortawesome/free-solid-svg-icons'

export default function DelModal({ show, handleClose, name, weight, laborCost, price, gemCost, typeID, warrantyID, onDelete}) {


  return (
    <Modal size="md" show={show} onHide={handleClose} aria-labelledby="contained-modal-title-vcenter" centered>
      <Modal.Header closeButton>
        <Modal.Title> <FontAwesomeIcon icon={faTriangleExclamation} style={{ color: 'red' }} /> &nbsp;Confirm Delete</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        
      <Row>
        <h5 className='mb-4'> {name} </h5>
            <Col md={5}>
                
            <p><FontAwesomeIcon icon={faWeightHanging}/> &nbsp; Weight: {weight}</p>
            <p><FontAwesomeIcon icon={faMoneyBill} /> &nbsp; Price: {price}</p>
            <p><FontAwesomeIcon icon={faDollarSign} /> &nbsp; Labor Cost: {laborCost}</p>
            <p><FontAwesomeIcon icon={faDollarSign} /> &nbsp; Gem Cost: {gemCost}</p>
            </Col>
            <Col md={7}>
            <p><FontAwesomeIcon icon={faBarcode} /> &nbsp; Type ID: {typeID}</p>
            <p><FontAwesomeIcon icon={faBarcode} /> &nbsp; Warranty ID: {warrantyID}</p>
            </Col>
          </Row>
      </Modal.Body>
      <Modal.Footer>
        <Button variant="secondary" onClick={handleClose}>
          Close
        </Button>
        <Button variant="danger" onClick={() => {handleClose(); onDelete()}} >
          Delete
        </Button>
      </Modal.Footer>
    </Modal>





  );

  
}

DelModal.propTypes = {
    show: PropTypes.bool.isRequired,
    handleClose: PropTypes.func.isRequired,
    name: PropTypes.string.isRequired,
    weight: PropTypes.any.isRequired,
    laborCost: PropTypes.any.isRequired,
    gemCost: PropTypes.any.isRequired,
    price: PropTypes.any.isRequired,
    typeID: PropTypes.string.isRequired,
    warrantyID: PropTypes.string.isRequired,
    onDelete: PropTypes.func.isRequired,

  };
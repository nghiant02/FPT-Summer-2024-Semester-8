import React from 'react';
import PropTypes from 'prop-types';
import { Row, Col, Modal, Button } from 'react-bootstrap';

export default function BillDetailsDialog({ show, handleClose, bill }) {
  return (
    <Modal size="md" show={show} onHide={handleClose} aria-labelledby="contained-modal-title-vcenter" centered>
      <Modal.Header closeButton>
        <Modal.Title>Bill Details</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Row>
          <Col>
            <p><strong>Bill ID:</strong> {bill.billId}</p>
            <p><strong>Customer Name:</strong> {bill.customerName}</p>
            <p><strong>Staff Name:</strong> {bill.staffName}</p>
            <p><strong>Total Amount:</strong> {bill.totalAmount}</p>
            <p><strong>Total Discount:</strong> {bill.totalDiscount}</p>
            <p><strong>Sale Date:</strong> {new Date(bill.saleDate).toLocaleString()}</p>
            <p><strong>Items:</strong></p>
            {bill.items.map((item, index) => (
              <p key={index}>
                - Name: {item.name} <br/>
                - Jewelry Price: {item.jewelryPrice} <br/>
                - Labor Cost: {item.laborCost} <br/>
                - Total Price: {item.totalPrice}
              </p>
            ))}
            <p><strong>Promotions:</strong></p>
            {bill.promotions.map((promotion, index) => (
              <p key={index}>
                - Promotion Discount: {promotion.discount}%
              </p>
            ))}
            <p><strong>Additional Discount:</strong> {bill.additionalDiscount}</p>
            <p><strong>Points Used:</strong> {bill.pointsUsed}</p>
            <p><strong>Final Amount:</strong> {bill.finalAmount}</p>
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

BillDetailsDialog.propTypes = {
  show: PropTypes.bool.isRequired,
  handleClose: PropTypes.func.isRequired,
  bill: PropTypes.shape({
    billId: PropTypes.string,
    customerName: PropTypes.string,
    staffName: PropTypes.string,
    totalAmount: PropTypes.number,
    totalDiscount: PropTypes.number,
    saleDate: PropTypes.string,
    items: PropTypes.arrayOf(
      PropTypes.shape({
        jewelryId: PropTypes.string,
        name: PropTypes.string,
        jewelryPrice: PropTypes.number,
        laborCost: PropTypes.number,
        totalPrice: PropTypes.number
      })
    ),
    promotions: PropTypes.arrayOf(
      PropTypes.shape({
        promotionId: PropTypes.string,
        discount: PropTypes.number
      })
    ),
    additionalDiscount: PropTypes.number,
    pointsUsed: PropTypes.number,
    finalAmount: PropTypes.number
  }).isRequired
};

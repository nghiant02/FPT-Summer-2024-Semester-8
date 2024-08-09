import React from 'react';
import PropTypes from 'prop-types';
import { Row, Col, Modal, Button } from 'react-bootstrap';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUser, faEnvelope, faPhone, faTag, faGenderless, faHashtag, faBriefcase, faCheckCircle, faTimesCircle } from '@fortawesome/free-solid-svg-icons';

export default function InfoModal({ show, handleClose, userData }) {
  return (
    <Modal size="md" show={show} onHide={handleClose} aria-labelledby="contained-modal-title-vcenter" centered>
      <Modal.Header closeButton>
        <Modal.Title>User Details</Modal.Title>
      </Modal.Header>
      <Modal.Body>
        <Row>
          <Col>
            <p><FontAwesomeIcon icon={faHashtag} /><strong> &nbsp; User ID: &nbsp; </strong> {userData.userId}</p>
            <p><FontAwesomeIcon icon={faUser} /><strong> &nbsp; Username: &nbsp; </strong> {userData.username}</p>
            <p><FontAwesomeIcon icon={faUser} /><strong> &nbsp; Full Name: &nbsp; </strong> {userData.fullName}</p>
            <p><FontAwesomeIcon icon={faGenderless} /><strong> &nbsp; Gender: &nbsp; </strong> {userData.gender}</p>
            <p><FontAwesomeIcon icon={faPhone} /><strong> &nbsp; Phone Number: &nbsp; </strong> {userData.phoneNumber}</p>
            <p><FontAwesomeIcon icon={faEnvelope} /><strong> &nbsp; Email: &nbsp; </strong> {userData.email}</p>
            <p><FontAwesomeIcon icon={faTag} /><strong> &nbsp; Counter Number: &nbsp; </strong> {userData.counterNumber}</p>
            <p><FontAwesomeIcon icon={faBriefcase} /><strong> &nbsp; Role: &nbsp; </strong> {userData.roleName}</p>
            <p><FontAwesomeIcon icon={userData.status ? faCheckCircle : faTimesCircle} /><strong> &nbsp; Status: &nbsp; </strong> {userData.status ? 'Active' : 'Inactive'}</p>
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
  userData: PropTypes.shape({
    userId: PropTypes.string,
    username: PropTypes.string,
    fullName: PropTypes.string,
    gender: PropTypes.string,
    phoneNumber: PropTypes.string,
    email: PropTypes.string,
    counterNumber: PropTypes.number,
    roleName: PropTypes.string,
    status: PropTypes.bool,
  }).isRequired,
};

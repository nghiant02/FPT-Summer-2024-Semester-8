import axios from 'axios';

// export const users = [
//   {
//     username: 'admin1',
//     email: 'admin1@example.com',
//     password: 'password123',
//     role: {
//       roleId: '1',
//       roleName: 'Admin1',
//     },
//   },
//   {
//     username: 'admin2',
//     email: 'admin2@example.com',
//     password: 'password123',
//     role: {
//       roleId: '2',
//       roleName: 'Admin2',
//     },
//   },
//   {
//     username: 'admin3',
//     email: 'admin3@example.com',
//     password: 'password123',
//     role: {
//       roleId: '3',
//       roleName: 'Admin3',
//     },
//   },
//   {
//     username: 'admin4',
//     email: 'admin4@example.com',
//     password: 'password123',
//     role: {
//       roleId: '4',
//       roleName: 'Admin4',
//     },
//   },
//   {
//     username: 'admin5',
//     email: 'admin5@example.com',
//     password: 'password123',
//     role: {
//       roleId: '5',
//       roleName: 'Admin5',
//     },
//   },
//   {
//     username: 'admin6',
//     email: 'admin6@example.com',
//     password: 'password123',
//     role: {
//       roleId: '6',
//       roleName: 'Admin6',
//     },
//   },
//   {
//     username: 'admin7',
//     email: 'admin7@example.com',
//     password: 'password123',
//     role: {
//       roleId: '7',
//       roleName: 'Admin7',
//     },
//   },
//   {
//     username: 'admin8',
//     email: 'admin8@example.com',
//     password: 'password123',
//     role: {
//       roleId: '8',
//       roleName: 'Admin8',
//     },
//   },
// ];



const fetchAllUsers = () => 
  axios.get("http://localhost:5188/api/User/GetUsers");

export { fetchAllUsers };

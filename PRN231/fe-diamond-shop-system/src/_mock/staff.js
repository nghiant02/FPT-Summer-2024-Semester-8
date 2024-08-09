export const staff = [
  {
    staffId: '1',
    userName: 'admin',
    email: 'jdoe@example.com',
    password: 'admin',
    roleId: 'Staff',
    counterId: 1,
    status: 'active',
  },
  {
    staffId: '2',
    userName: 'manager',
    email: 'asmith@example.com',
    password: 'manager',
    roleId: 'Manager',
    counterId: 2,
    status: 'active',
  },
  {
    staffId: '3',
    userName: 'mjones',
    email: 'mjones@example.com',
    password: 'securepassword3',
    roleId: 'Admin',
    counterId: 3,
    status: 'inactive',
  },
  {
    staffId: '4',
    userName: 'bclark',
    email: 'bclark@example.com',
    password: 'securepassword4',
    roleId: 'Staff',
    counterId: 4,
    status: 'active',
  },
  {
    staffId: '5',
    userName: 'ewilson',
    email: 'ewilson@example.com',
    password: 'securepassword5',
    roleId: 'Manager',
    counterId: 5,
    status: 'inactive',
  },
  {
    staffId: '6',
    userName: 'djohnson',
    email: 'djohnson@example.com',
    password: 'securepassword6',
    roleId: 'Admin',
    counterId: 6,
    status: 'active',
  },
  {
    staffId: '7',
    userName: 'kroberts',
    email: 'kroberts@example.com',
    password: 'securepassword7',
    roleId: 'Staff',
    counterId: 7,
    status: 'inactive',
  },
];

export function addStaff(newStaffData) {
  staff.push(newStaffData);
}


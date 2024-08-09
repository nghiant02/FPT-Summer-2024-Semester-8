export const customer =[
  {
    CusID: '1',
    name: 'David Tran',
    address: '32 Western Street',
    phoneNumber:'(84) 453-7283',
    point: ' 43 ',
    gender: 'male'
    
  },

  {
    CusID: '2',
    name: 'John Smith',
    address: '4324 Las Vegas ',
    phoneNumber:'(31) 987-4311',
    point: ' 21 ',
    gender: 'male'
  },

  {
    CusID: '3',
    name: 'Wiliams Nguyen',
    address: '12 Califonia',
    phoneNumber:'(84) 453-7283',
    point: ' 43 ',
    gender: 'male'
  },

  {
    CusID: '4',
    name: 'Olivia Johnson',
    address: '452 Los Angeles',
    phoneNumber:'(64) 422-5632',
    point: ' 100 ',
    gender:' female'
  },

  {
    CusID: '5',
    name: 'Mia Garcia ',
    address: '452 Los Angeles',
    phoneNumber:'(23) 543-1892',
    point: ' 65 ',
    gender: 'female'
  },
]

export function addCustomer(newCustomerData) {
  customer.push(newCustomerData);
}

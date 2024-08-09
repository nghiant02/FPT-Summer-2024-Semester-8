export const bill = [
    {
      billId: 101,
      staffId: 1,
      customerId: 1001,
      totalAmount: 250.75,
      saleDate: '2023-01-15'
    },
    {
      staffId: 2,
      billId: 102,
      customerId: 1002,
      totalAmount: 175.50,
      saleDate: '2023-01-16'
    },
    {
      staffId: 3,
      billId: 103,
      customerId: 1003,
      totalAmount: 320.00,
      saleDate: '2023-01-17'
    },
    {
      staffId: 4,
      billId: 104,
      customerId: 1004,
      totalAmount: 450.00,
      saleDate: '2023-01-18'
    },
    {
      staffId: 1,
      billId: 105,
      customerId: 1005,
      totalAmount: 60.00,
      saleDate: '2023-01-19'
    },
    {
      staffId: 2,
      billId: 106,
      customerId: 1006,
      totalAmount: 980.25,
      saleDate: '2023-01-20'
    },
    {
      staffId: 3,
      billId: 107,
      customerId: 1007,
      totalAmount: 200.00,
      saleDate: '2023-01-21'
    },
    {
      staffId: 4,
      billId: 108,
      customerId: 1008,
      totalAmount: 150.75,
      saleDate: '2023-01-22'
    },
    {
      staffId: 1,
      billId: 109,
      customerId: 1009,
      totalAmount: 300.50,
      saleDate: '2023-01-23'
    },
    {
      staffId: 2,
      billId: 110,
      customerId: 1010,
      totalAmount: 400.00,
      saleDate: '2023-01-24'
    }
  ];

  export function addBill(newBillData) {
    bill.push(newBillData);
  } 
  

export const promotion = [
  {
    promotionId: '1',
    type: 'Type1',
    approveManager: 'John Doe',
    description: 'Special discount for summer season.',
    discountRate: 20,
    startDate: '2023-06-01',
    endDate: '2023-09-01',
  },
  {
    promotionId: '2',
    type: 'Type2',
    approveManager: 'Jane Smith',
    description: 'Black Friday Sale.',
    discountRate: 50,
    startDate: '2023-11-24',
    endDate: '2023-11-30',
  },
  {
    promotionId: '3',
    type: 'Type3',
    approveManager: 'Mike Johnson',
    description: 'New Year Celebration Discount.',
    discountRate: 30,
    startDate: '2023-12-25',
    endDate: '2024-01-05',
  },
  {
    promotionId: '4',
    type: 'Type1',
    approveManager: 'Emily Davis',
    description: 'Back to School Promotion.',
    discountRate: 25,
    startDate: '2023-08-01',
    endDate: '2023-09-15',
  },
  {
    promotionId: '5',
    type: 'Type2',
    approveManager: 'William Brown',
    description: 'Spring Sale.',
    discountRate: 15,
    startDate: '2023-03-20',
    endDate: '2023-04-30',
  },
  {
    promotionId: '6',
    type: 'Type3',
    approveManager: 'Jessica Wilson',
    description: 'Holiday Season Discount.',
    discountRate: 40,
    startDate: '2023-12-01',
    endDate: '2023-12-31',
  },
  {
    promotionId: '7',
    type: 'Type1',
    approveManager: 'David Lee',
    description: 'Clearance Sale.',
    discountRate: 60,
    startDate: '2023-07-15',
    endDate: '2023-08-15',
  },
];

export function addPromotion(newPromotionData) {
  promotion.push(newPromotionData);
}

import SvgColor from 'src/components/svg-color';

// ----------------------------------------------------------------------

const icon = (name) => (
  <SvgColor src={`/assets/icons/navbar/${name}.svg`} sx={{ width: 1, height: 1 }} />
);

const navConfig = [
  {
    title: 'dashboard',
    path: '/dashboard',
    icon: icon('ic_analytics'),
  },
  {
    title: 'user',
    path: '/user',
    icon: icon('ic_user'),
  },
  // {
  //   title: 'product',
  //   path: '/products',
  //   icon: icon('ic_cart'),
  // },
  // {
  //   title: 'blog',
  //   path: '/blog',
  //   icon: icon('ic_blog'),
  // },
  // {
  //   title: 'login',
  //   path: '/login',
  //   icon: icon('ic_lock'),
  // },
  // {
  //   title: 'Not found',
  //   path: '/404',
  //   icon: icon('ic_disabled'),
  // },
  {
    title: 'promotion',
    path: '/promotion',
    icon: icon('ic_promotion'),
  },
  {
    title: 'customer',
    path: '/customer',
    icon: icon('ic_customer'),
  },
  {
    title: 'Jewellery',
    path: '/jewellery',
    icon: icon('ic_jew'),
  },
  {
    title: 'Staff',
    path: '/staff',
    icon: icon('ic_staff'),
  },
  {
    title: 'Bill',
    path: '/bill',
    icon: icon('ic_bill'),
  },
  {
    title: 'GoldPrice',
    path: '/goldprice',
    icon: icon('ic_goldprice'),
  },
  {
    title: 'Logout',
    path: '/login',
    icon: icon('ic_logout'),
  },
];

export default navConfig;

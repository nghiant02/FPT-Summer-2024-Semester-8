import { lazy, Suspense } from 'react';
import { Outlet, Navigate, useRoutes } from 'react-router-dom';

import DashboardLayout from 'src/layouts/dashboard';

export const IndexPage = lazy(() => import('src/pages/app'));
export const UserPage = lazy(() => import('src/pages/user'));
export const LoginPage = lazy(() => import('src/pages/login'));
export const Promotion = lazy(() => import('src/pages/promotion'));
export const Customer = lazy(() => import('src/pages/customer'));
export const Jewellery = lazy(() => import('src/pages/jewellery'));
export const Staff = lazy(() => import('src/pages/staff'));
export const Bill = lazy(() => import('src/pages/bill'));
export const GoldPrice = lazy(() => import ('src/pages/goldprice'))
// ----------------------------------------------------------------------

export default function Router() {
  const isAuthenticated = false;
  const routes = useRoutes([
    {
      path: '/',
      element: isAuthenticated ? <Navigate to="/dashboard" /> : <Navigate to="/login" replace />,
    },
    {
      path: 'login',
      element: <LoginPage />,
    },
    {
      element: (
        <DashboardLayout>
          <Suspense>
            <Outlet />
          </Suspense>
        </DashboardLayout>
      ),
      children: [
        { element: <IndexPage />},
        { path: 'user', element: <UserPage /> },
        { path: 'promotion', element: <Promotion /> },
        { path: 'customer', element: <Customer /> },
        { path: 'jewellery', element: <Jewellery /> },
        { path: 'staff', element: <Staff /> },
        { path: 'bill', element: <Bill /> },
        { path: 'goldprice', element: <GoldPrice /> },
        { path: 'dashboard', element: <IndexPage /> },  
      ],
    },
  
    {
      path: '*',
      element: <Navigate to="/404" replace />,
    },
  ]);

  return routes;
}

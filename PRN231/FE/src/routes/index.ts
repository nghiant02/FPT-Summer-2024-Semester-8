import { RoleType } from '../enums';
import DefaultLayout from '../layouts/DefaultLayout';
import DefaultManagerLayout from '../layouts/managers/DefaultManagerLayout';
import Home from '../pages/Home';
import BuyBackPage from '../pages/buyBackPage/BuyBackPage';
import GoldPricePage from '../pages/goldPricePage/GoldPricePage';
import LoginPage from '../pages/loginPage/LoginPage';
import NotFoundPage from '../pages/notFoundPage/NotFoundPage';
import SellingPage from '../pages/sellingPage/SellingPage';

export interface LayoutProps {
    childen: React.ReactNode;
    requireRole?: RoleType[];
    whenRoleUnMatchNavTo?: string;
}

interface RouteProps {
    path: string;
    component: () => JSX.Element;
    layout: ({ childen, requireRole, whenRoleUnMatchNavTo }: LayoutProps) => JSX.Element;
}

const publicRoutes: RouteProps[] = [
    // { path: '/', component: HomePage, layout: DefaultLayout },
    { path: '/login', component: LoginPage, layout: DefaultLayout },
    { path: '/', component: Home, layout: DefaultLayout },
    { path: '/*', component: NotFoundPage, layout: DefaultLayout },
];

const managerRoutes: RouteProps[] = [
    { path: '/manager/selling', component: SellingPage, layout: DefaultManagerLayout },
    { path: '/manager/gold-price', component: GoldPricePage, layout: DefaultManagerLayout },
    { path: '/manager/buy-back', component: BuyBackPage, layout: DefaultManagerLayout },
];

export { publicRoutes, managerRoutes };

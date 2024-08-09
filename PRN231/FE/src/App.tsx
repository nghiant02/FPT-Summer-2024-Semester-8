import './App.css';
import { Route, Routes } from 'react-router-dom';
import { managerRoutes, publicRoutes } from './routes';

function App() {
    return (
        <>
            <div>
                <Routes>
                    {publicRoutes.map(({ layout, component, path }, index) => {
                        const Layout = layout;
                        const Component = component;

                        return (
                            <Route
                                key={index}
                                path={path}
                                element={<Layout childen={<Component />} />}
                            />
                        );
                    })}
                    {managerRoutes.map(({ layout, component, path }, index) => {
                        const Layout = layout;
                        const Component = component;

                        return (
                            <Route
                                key={index}
                                path={path}
                                element={
                                    <Layout
                                        childen={<Component />}
                                        requireRole={['Manager', 'Staff']}
                                        whenRoleUnMatchNavTo="/login"
                                    />
                                }
                            />
                        );
                    })}
                </Routes>
            </div>
        </>
    );
}

export default App;

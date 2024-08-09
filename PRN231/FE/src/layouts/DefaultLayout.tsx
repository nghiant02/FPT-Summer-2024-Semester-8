const DefaultLayout = ({ childen }: { childen: React.ReactNode }) => {
    return (
        <>
            <div className="min-h-screen">{childen}</div>
        </>
    );
};

export default DefaultLayout;

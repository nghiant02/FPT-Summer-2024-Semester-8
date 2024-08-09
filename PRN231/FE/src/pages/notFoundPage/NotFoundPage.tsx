import { Button, Result } from 'antd';
import { Link } from 'react-router-dom';

const NotFoundPage = () => {
    return (
        <div>
            <Result
                status="404"
                title="404"
                subTitle="Xin lỗi! Trang bạn truy cập không tồn tại!"
                extra={
                    <Link to={'/manager/selling'}>
                        <Button
                            type="primary"
                            size="large"
                            className="rounded-sm bg-secondary hover:!bg-secondary-LIGHT"
                        >
                            Quay lại trang chủ
                        </Button>
                    </Link>
                }
            />
        </div>
    );
};

export default NotFoundPage;

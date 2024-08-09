import { Modal } from 'antd';
interface CustomerModelProps {
    open: boolean;
    body: React.ReactNode;
    title?: string;
}

const CustomerModel = ({ open, body, title }: CustomerModelProps) => {
    return (
        <Modal
            width={300}
            style={{ padding: 0 }}
            closable={false}
            footer={false}
            title={
                title ? (
                    <div className="bg-primary p-2">
                        <p className="w-full text-center text-lg uppercase text-white">{title}</p>
                    </div>
                ) : undefined
            }
            open={open}
            styles={{
                content: {
                    padding: 0,
                },
            }}
            className="min-w-fit"
        >
            {body}
        </Modal>
    );
};

export default CustomerModel;

import { useRef } from 'react';
import useIframeURLChange from '../hooks';

interface IframeComponentProps {
    url: string;
    onUrlChange: (url: string) => void;
}

const IframeComponent = ({ onUrlChange, url }: IframeComponentProps) => {
    const iframeRef = useRef<HTMLIFrameElement>(null);
    useIframeURLChange(iframeRef, (newUrl: string) => {
        onUrlChange(newUrl);
    });

    return (
        <div>
            <iframe ref={iframeRef} src={url} width="400" height="700" title="Payment"></iframe>
        </div>
    );
};

export default IframeComponent;

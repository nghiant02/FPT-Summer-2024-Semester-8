import { useEffect } from 'react';

const useIframeURLChange = (
    iframeRef: React.RefObject<HTMLIFrameElement>,
    callback: (url: string) => void,
) => {
    useEffect(() => {
        const iframe = iframeRef.current;
        if (!iframe) return;

        let lastDispatched: string | undefined = undefined;

        const dispatchChange = () => {
            try {
                const isSameUrl = iframe.contentWindow;
                if (isSameUrl) {
                    const newHref = iframe.contentWindow?.location?.href;
                    if (newHref !== lastDispatched) {
                        callback(newHref!);
                        lastDispatched = newHref;
                    }
                }
            } catch (error) {
                console.error('Error accessing iframe content:', error);
            }
        };

        const unloadHandler = () => {
            setTimeout(dispatchChange, 0);
        };

        const attachUnload = () => {
            iframe.contentWindow?.removeEventListener('unload', unloadHandler);
            iframe.contentWindow?.addEventListener('unload', unloadHandler);
        };

        iframe.addEventListener('load', () => {
            attachUnload();
            dispatchChange();
        });

        attachUnload();

        return () => {
            iframe.removeEventListener('load', dispatchChange);
            iframe.contentWindow?.removeEventListener('unload', unloadHandler);
        };
    }, [iframeRef, callback]);
};

export default useIframeURLChange;

/** @type {import('tailwindcss').Config} */
export default {
    content: ['./index.html', './src/**/*.{js,ts,jsx,tsx}'],
    theme: {
        extend: {
            colors: {
                primary: {
                    DEFAULT: '#26A4DD',
                    TEXT: '#0B87C9',
                },
                secondary: {
                    DEFAULT: '#f0ad4e',
                    B: '#eea236',
                    DARK: '#dcb721',
                    LIGHT: '#ffb752',
                },
                gray: {
                    DEFAULT: '#f0f0f0',
                },
                green: {
                    OUTLINE: '#5DA19F',
                },
                purple: {
                    DEFAULT: '#5c6ac4',
                },
            },
        },
    },
    plugins: [],
};

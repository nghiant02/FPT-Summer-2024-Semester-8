import numeral from 'numeral';

export const formatNumber = (num: string) => {
    // Loại bỏ dấu phẩy từ chuỗi số đầu vào
    const sanitizedNum = num.replace(/[^0-9.]/g, '');

    // Sử dụng numeral để định dạng lại số
    return numeral(sanitizedNum).format('0,0.[00]');
};

export const parseFormattedNumber = (formattedNumber: string) => {
    // Loại bỏ dấu phẩy từ chuỗi số đầu vào
    const sanitizedNumber = formattedNumber.replace(/,/g, '');

    // Chuyển đổi chuỗi thành số thập phân
    return parseFloat(sanitizedNumber);
};

export const formatVNDate = (date: Date) => {
    const options: Intl.DateTimeFormatOptions = { day: 'numeric', month: 'long', year: 'numeric' };
    return date.toLocaleDateString('vi-VN', options);
};

export const toMoney = (number:number) => {
    return formatNumber(""+number)+" ₫";
}

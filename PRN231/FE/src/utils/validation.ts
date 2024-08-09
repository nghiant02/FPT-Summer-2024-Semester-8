export const isValidateEmail = (email: string) => {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
};
export const isValidatePhoneNumber = (phone: string) => {
    const phoneRegex = /^\d{10,11}$/;
    return phoneRegex.test(phone);
};

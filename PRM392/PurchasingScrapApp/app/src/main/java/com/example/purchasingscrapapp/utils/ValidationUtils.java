package com.example.purchasingscrapapp.utils;

import android.widget.EditText;

public class ValidationUtils {

    public static boolean isValidEmail(EditText emailEditText) {
        String email = emailEditText.getText().toString().trim();
        return email.contains("@") && email.contains(".");
    }

    public static boolean isValidPassword(EditText passwordEditText) {
        String password = passwordEditText.getText().toString().trim();
        return password.length() >= 6;
    }

    public static boolean isNotEmpty(EditText editText) {
        return editText.getText().toString().trim().length() > 0;
    }

    public static boolean isValidPhone(EditText phoneEditText) {
        String phone = phoneEditText.getText().toString().trim();
        return phone.matches("^0\\d{9}$");
    }

    public static boolean doPasswordsMatch(EditText passwordEditText, EditText confirmPasswordEditText) {
        String password = passwordEditText.getText().toString().trim();
        String confirmPassword = confirmPasswordEditText.getText().toString().trim();
        return password.equals(confirmPassword);
    }
}

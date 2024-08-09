package com.example.purchasingscrapapp.activity;

import android.content.Intent;
import android.os.Bundle;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ProgressBar;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;
import androidx.lifecycle.ViewModelProvider;

import com.example.purchasingscrapapp.R;
import com.example.purchasingscrapapp.utils.ValidationUtils;
import com.example.purchasingscrapapp.viewmodel.UserViewModel;

public class ForgotPasswordActivity extends AppCompatActivity {

    private EditText emailEditText;
    private Button resetPasswordButton;
    private ProgressBar progressBar;
    private UserViewModel userViewModel;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_forgot_password);

        emailEditText = findViewById(R.id.editTextEmail);
        resetPasswordButton = findViewById(R.id.buttonResetPassword);
        progressBar = findViewById(R.id.progressBar);

        userViewModel = new ViewModelProvider(this).get(UserViewModel.class);

        resetPasswordButton.setOnClickListener(v -> {
            if (ValidationUtils.isValidEmail(emailEditText)) {
                String email = emailEditText.getText().toString().trim();

                userViewModel.resetPassword(this, email, progressBar).observe(this, aVoid -> {
                    Toast.makeText(ForgotPasswordActivity.this, "Error sending password reset email.", Toast.LENGTH_SHORT).show();
                });
            } else {
                Toast.makeText(ForgotPasswordActivity.this, "Please enter a valid email.", Toast.LENGTH_SHORT).show();
            }
        });
    }
}

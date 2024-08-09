package com.example.purchasingscrapapp.activity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ProgressBar;
import android.widget.TextView;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;
import androidx.lifecycle.ViewModelProvider;

import com.example.purchasingscrapapp.R;
import com.example.purchasingscrapapp.viewmodel.UserViewModel;
import com.example.purchasingscrapapp.utils.ValidationUtils;
import com.example.purchasingscrapapp.model.User;
import com.google.firebase.Timestamp;

public class RegisterActivity extends AppCompatActivity {

    private EditText emailEditText, passwordEditText, confirmPasswordEditText, nameEditText, phoneEditText;
    private Button registerButton;
    private TextView loginButton;
    private ProgressBar progressBar;
    private UserViewModel userViewModel;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_register);

        emailEditText = findViewById(R.id.editTextEmail);
        passwordEditText = findViewById(R.id.editTextPassword);
        confirmPasswordEditText = findViewById(R.id.editTextConfirmPassword);
        nameEditText = findViewById(R.id.editTextName);
        phoneEditText = findViewById(R.id.editTextPhone);
        registerButton = findViewById(R.id.buttonRegister);
        loginButton = findViewById(R.id.textViewLogin);
        progressBar = findViewById(R.id.progressBar);

        userViewModel = new ViewModelProvider(this).get(UserViewModel.class);

        registerButton.setOnClickListener(v -> {
            if (validateInputs()) {
                String email = emailEditText.getText().toString().trim();
                String password = passwordEditText.getText().toString().trim();
                String name = nameEditText.getText().toString().trim();
                String phone = phoneEditText.getText().toString().trim();

                progressBar.setVisibility(View.VISIBLE);
                userViewModel.registerUser(this, email, password, name, phone, progressBar).observe(this, authResult -> {
                    progressBar.setVisibility(View.GONE);
                    if (authResult != null) {
                        String userId = authResult.getUser().getUid();
                        User newUser = new User(userId, email, password, name, phone, "", "", "user", "inactive", Timestamp.now(), Timestamp.now());
                        userViewModel.createUserInFirestore(newUser);

                        Toast.makeText(RegisterActivity.this, "Registration successful. Please check your email for verification.", Toast.LENGTH_LONG).show();
                        startActivity(new Intent(RegisterActivity.this, LoginActivity.class));
                    } else {
                        Toast.makeText(RegisterActivity.this, "Registration failed.", Toast.LENGTH_SHORT).show();
                    }
                });
            }
        });

        loginButton.setOnClickListener(v -> startActivity(new Intent(RegisterActivity.this, LoginActivity.class)));
    }

    private boolean validateInputs() {
        if (!ValidationUtils.isNotEmpty(nameEditText)) {
            nameEditText.setError("Name is required");
            nameEditText.requestFocus();
            return false;
        }

        if (!ValidationUtils.isValidEmail(emailEditText)) {
            emailEditText.setError("Invalid email address");
            emailEditText.requestFocus();
            return false;
        }

        if (!ValidationUtils.isValidPhone(phoneEditText)) {
            phoneEditText.setError("Invalid phone number. Must be 10 digits and start with a 0.");
            phoneEditText.requestFocus();
            return false;
        }

        if (!ValidationUtils.isValidPassword(passwordEditText)) {
            passwordEditText.setError("Password must be at least 6 characters, contain at least one digit, one uppercase letter, and one special character");
            passwordEditText.requestFocus();
            return false;
        }

        if (!ValidationUtils.doPasswordsMatch(passwordEditText, confirmPasswordEditText)) {
            confirmPasswordEditText.setError("Passwords do not match");
            confirmPasswordEditText.requestFocus();
            return false;
        }

        return true;
    }
}

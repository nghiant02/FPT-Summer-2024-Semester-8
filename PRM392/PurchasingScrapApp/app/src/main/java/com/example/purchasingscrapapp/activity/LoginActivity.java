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
import com.example.purchasingscrapapp.utils.FirebaseUtils;
import com.example.purchasingscrapapp.utils.ValidationUtils;
import com.example.purchasingscrapapp.viewmodel.UserViewModel;
import com.google.firebase.auth.FirebaseUser;
import com.google.firebase.firestore.DocumentSnapshot;
import com.google.firebase.firestore.FirebaseFirestore;

public class LoginActivity extends AppCompatActivity {

    private EditText emailEditText, passwordEditText;
    private Button loginButton;
    private TextView registerButton, forgotPasswordButton;
    private ProgressBar progressBar;
    private UserViewModel userViewModel;
    private FirebaseFirestore db;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        emailEditText = findViewById(R.id.editTextEmail);
        passwordEditText = findViewById(R.id.editTextPassword);
        loginButton = findViewById(R.id.buttonLogin);
        registerButton = findViewById(R.id.textViewRegister);
        forgotPasswordButton = findViewById(R.id.textViewForgotPassword);
        progressBar = findViewById(R.id.progressBar);

        userViewModel = new ViewModelProvider(this).get(UserViewModel.class);
        db = FirebaseFirestore.getInstance();

        loginButton.setOnClickListener(v -> {
            if (ValidationUtils.isValidEmail(emailEditText) && ValidationUtils.isValidPassword(passwordEditText)) {
                String email = emailEditText.getText().toString().trim();
                String password = passwordEditText.getText().toString().trim();

                progressBar.setVisibility(View.VISIBLE);
                userViewModel.loginUser(this, email, password, progressBar).observe(this, authResult -> {
                    progressBar.setVisibility(View.GONE);
                    if (authResult != null) {
                        FirebaseUser user = authResult.getUser();
                        if (user != null) {
                            FirebaseUtils.updateUserStatus(user.getUid(), "active");
                            handleUserLogin(user);
                        } else {
                            Toast.makeText(this, "Login failed.", Toast.LENGTH_SHORT).show();
                        }
                    } else {
                        Toast.makeText(this, "Login failed.", Toast.LENGTH_SHORT).show();
                    }
                });
            } else {
                Toast.makeText(this, "Please enter valid email and password.", Toast.LENGTH_SHORT).show();
            }
        });

        registerButton.setOnClickListener(v -> startActivity(new Intent(this, RegisterActivity.class)));
        forgotPasswordButton.setOnClickListener(v -> startActivity(new Intent(this, ForgotPasswordActivity.class)));
    }

    private void handleUserLogin(FirebaseUser user) {
        db.collection("users").document(user.getUid()).get().addOnCompleteListener(task -> {
            if (task.isSuccessful()) {
                DocumentSnapshot document = task.getResult();
                if (document.exists()) {
                    String status = document.getString("status");
                    String role = document.getString("role");
                    if ("active".equals(status)) {
                        Intent intent;
                        switch (role) {
                            case "admin":
                                intent = new Intent(this, AdminDashboardActivity.class);
                                Toast.makeText(this, "Welcome Admin!", Toast.LENGTH_SHORT).show();
                                break;
                            case "staff":
                                intent = new Intent(this, StaffDashboardActivity.class);
                                Toast.makeText(this, "Welcome Staff!", Toast.LENGTH_SHORT).show();
                                break;
                            default:
                                intent = new Intent(this, MainActivity.class);
                                Toast.makeText(this, "Welcome User!", Toast.LENGTH_SHORT).show();
                                break;
                        }
                        startActivity(intent);
                        finish();
                    } else {
                        Toast.makeText(this, "Your account is not active.", Toast.LENGTH_LONG).show();
                    }
                } else {
                    Toast.makeText(this, "User data not found.", Toast.LENGTH_SHORT).show();
                }
            } else {
                Toast.makeText(this, "Failed to retrieve user data.", Toast.LENGTH_SHORT).show();
            }
        });
    }
}

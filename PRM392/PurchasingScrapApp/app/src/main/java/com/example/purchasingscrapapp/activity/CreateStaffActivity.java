package com.example.purchasingscrapapp.activity;

import android.os.Bundle;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ProgressBar;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;
import androidx.lifecycle.ViewModelProvider;

import com.example.purchasingscrapapp.R;
import com.example.purchasingscrapapp.model.User;
import com.example.purchasingscrapapp.viewmodel.UserViewModel;
import com.google.firebase.Timestamp;

public class CreateStaffActivity extends AppCompatActivity {

    private EditText editTextName, editTextEmail, editTextPassword, editTextPhone;
    private Button buttonCreateStaff;
    private UserViewModel userViewModel;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_create_staff);

        editTextName = findViewById(R.id.editTextStaffName);
        editTextEmail = findViewById(R.id.editTextStaffEmail);
        editTextPassword = findViewById(R.id.editTextStaffPassword);
        editTextPhone = findViewById(R.id.editTextStaffPhone);
        buttonCreateStaff = findViewById(R.id.buttonCreateStaff);

        userViewModel = new ViewModelProvider(this).get(UserViewModel.class);

        buttonCreateStaff.setOnClickListener(v -> {
            String name = editTextName.getText().toString().trim();
            String email = editTextEmail.getText().toString().trim();
            String password = editTextPassword.getText().toString().trim();
            String phone = editTextPhone.getText().toString().trim();

            if (validateInputs(name, email, password, phone)) {
                createStaffAccount(name, email, password, phone);
            }
        });
    }

    private boolean validateInputs(String name, String email, String password, String phone) {
        if (name.isEmpty() || email.isEmpty() || password.isEmpty() || phone.isEmpty()) {
            Toast.makeText(this, "All fields are required", Toast.LENGTH_SHORT).show();
            return false;
        }
        return true;
    }

    private void createStaffAccount(String name, String email, String password, String phone) {
        ProgressBar progressBar = new ProgressBar(this);
        progressBar.setVisibility(ProgressBar.VISIBLE);

        userViewModel.registerStaff(this, email, password, name, phone, progressBar).observe(this, authResult -> {
            progressBar.setVisibility(ProgressBar.GONE);
            if (authResult != null) {
                String userId = authResult.getUser().getUid();
                User newUser = new User(userId, email, password, name, phone, "", "", "staff", "active", Timestamp.now(), Timestamp.now());
                userViewModel.createUserInFirestore(newUser);

                Toast.makeText(CreateStaffActivity.this, "Staff account created successfully.", Toast.LENGTH_LONG).show();
                finish();
            } else {
                Toast.makeText(CreateStaffActivity.this, "Failed to create staff account.", Toast.LENGTH_SHORT).show();
            }
        });
    }
}

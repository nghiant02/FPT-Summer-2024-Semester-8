package com.example.purchasingscrapapp.utils;

import android.content.Context;
import android.content.Intent;
import android.view.View;
import android.widget.ProgressBar;
import android.widget.Toast;

import com.example.purchasingscrapapp.activity.AdminDashboardActivity;
import com.example.purchasingscrapapp.activity.LoginActivity;
import com.example.purchasingscrapapp.activity.MainActivity;
import com.example.purchasingscrapapp.activity.StaffDashboardActivity;
import com.example.purchasingscrapapp.model.User;
import com.google.android.gms.tasks.OnCompleteListener;
import com.google.firebase.auth.AuthResult;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseUser;
import com.google.firebase.auth.UserProfileChangeRequest;
import com.google.firebase.firestore.DocumentSnapshot;
import com.google.firebase.firestore.FirebaseFirestore;
import com.google.firebase.Timestamp;

import java.util.Date;

public class FirebaseUtils {

    private static FirebaseAuth mAuth = FirebaseAuth.getInstance();

    public static void registerUser(Context context, String email, String password, String name, String phone, ProgressBar progressBar, OnCompleteListener<AuthResult> listener) {
        if (email.isEmpty() || password.isEmpty() || name.isEmpty() || phone.isEmpty()) {
            Toast.makeText(context, "All fields are required", Toast.LENGTH_SHORT).show();
            return;
        }
        progressBar.setVisibility(View.VISIBLE);
        mAuth.createUserWithEmailAndPassword(email, password)
                .addOnCompleteListener(listener)
                .addOnCompleteListener(task -> {
                    progressBar.setVisibility(View.GONE);
                    if (task.isSuccessful()) {
                        FirebaseUser firebaseUser = mAuth.getCurrentUser();
                        if (firebaseUser != null) {
                            UserProfileChangeRequest profileUpdates = new UserProfileChangeRequest.Builder()
                                    .setDisplayName(name)
                                    .build();
                            firebaseUser.updateProfile(profileUpdates).addOnCompleteListener(updateTask -> {
                                if (updateTask.isSuccessful()) {
                                    sendVerificationEmail(context);
                                    User user = new User(firebaseUser.getUid(), email, password, name, phone, "", "", "user", "inactive", new Timestamp(new Date()), new Timestamp(new Date()));
                                    createUserInFirestore(user);
                                    Toast.makeText(context, "Registration successful. Please check your email for verification.", Toast.LENGTH_LONG).show();
                                    context.startActivity(new Intent(context, LoginActivity.class));
                                }
                            });
                        }
                    } else {
                        Toast.makeText(context, "Registration failed: " + task.getException().getMessage(), Toast.LENGTH_LONG).show();
                    }
                });
    }

    public static void registerStaff(Context context, String email, String password, String name, String phone, ProgressBar progressBar, OnCompleteListener<AuthResult> listener) {
        if (email.isEmpty() || password.isEmpty() || name.isEmpty() || phone.isEmpty()) {
            Toast.makeText(context, "All fields are required", Toast.LENGTH_SHORT).show();
            return;
        }
        progressBar.setVisibility(View.VISIBLE);
        mAuth.createUserWithEmailAndPassword(email, password)
                .addOnCompleteListener(listener)
                .addOnCompleteListener(task -> {
                    progressBar.setVisibility(View.GONE);
                    if (task.isSuccessful()) {
                        FirebaseUser firebaseUser = mAuth.getCurrentUser();
                        if (firebaseUser != null) {
                            UserProfileChangeRequest profileUpdates = new UserProfileChangeRequest.Builder()
                                    .setDisplayName(name)
                                    .build();
                            firebaseUser.updateProfile(profileUpdates).addOnCompleteListener(updateTask -> {
                                if (updateTask.isSuccessful()) {
                                    User user = new User(firebaseUser.getUid(), email, password, name, phone, "", "", "staff", "active", new Timestamp(new Date()), new Timestamp(new Date()));
                                    createUserInFirestore(user);
                                    Toast.makeText(context, "Staff account created successfully.", Toast.LENGTH_LONG).show();
                                }
                            });
                        }
                    } else {
                        Toast.makeText(context, "Registration failed: " + task.getException().getMessage(), Toast.LENGTH_LONG).show();
                    }
                });
    }

    public static void loginUser(Context context, String email, String password, ProgressBar progressBar, OnCompleteListener<AuthResult> listener) {
        if (email.isEmpty() || password.isEmpty()) {
            Toast.makeText(context, "Email and password are required", Toast.LENGTH_SHORT).show();
            return;
        }
        progressBar.setVisibility(View.VISIBLE);
        mAuth.signInWithEmailAndPassword(email, password)
                .addOnCompleteListener(listener)
                .addOnCompleteListener(task -> {
                    progressBar.setVisibility(View.GONE);
                    if (task.isSuccessful()) {
                        FirebaseUser user = mAuth.getCurrentUser();
                        if (user != null) {
                            FirebaseFirestore db = FirebaseFirestore.getInstance();
                            db.collection("users").document(user.getUid()).get().addOnCompleteListener(task1 -> {
                                if (task1.isSuccessful()) {
                                    DocumentSnapshot document = task1.getResult();
                                    if (document.exists()) {
                                        String role = document.getString("role");
                                        if ("admin".equals(role) || "staff".equals(role)) {
                                            updateUserStatus(user.getUid(), "active");
                                            handleUserLogin(context, user);
                                        } else if (user.isEmailVerified()) {
                                            updateUserStatus(user.getUid(), "active");
                                            handleUserLogin(context, user);
                                        } else {
                                            sendVerificationEmail(context);
                                            Toast.makeText(context, "Please verify your email before logging in.", Toast.LENGTH_LONG).show();
                                        }
                                    } else {
                                        Toast.makeText(context, "User data not found.", Toast.LENGTH_SHORT).show();
                                    }
                                } else {
                                    Toast.makeText(context, "Failed to retrieve user data.", Toast.LENGTH_SHORT).show();
                                }
                            });
                        }
                    } else {
                        Toast.makeText(context, "Login failed: " + task.getException().getMessage(), Toast.LENGTH_LONG).show();
                    }
                });
    }

    private static void handleUserLogin(Context context, FirebaseUser user) {
        FirebaseFirestore db = FirebaseFirestore.getInstance();
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
                                intent = new Intent(context, AdminDashboardActivity.class);
                                Toast.makeText(context, "Welcome Admin!", Toast.LENGTH_SHORT).show();
                                break;
                            case "staff":
                                intent = new Intent(context, StaffDashboardActivity.class);
                                Toast.makeText(context, "Welcome Staff!", Toast.LENGTH_SHORT).show();
                                break;
                            default:
                                intent = new Intent(context, MainActivity.class);
                                Toast.makeText(context, "Welcome User!", Toast.LENGTH_SHORT).show();
                                break;
                        }
                        context.startActivity(intent);
                        ((LoginActivity) context).finish();
                    } else {
                        Toast.makeText(context, "Your account is not active.", Toast.LENGTH_LONG).show();
                    }
                } else {
                    Toast.makeText(context, "User data not found.", Toast.LENGTH_SHORT).show();
                }
            } else {
                Toast.makeText(context, "Failed to retrieve user data.", Toast.LENGTH_SHORT).show();
            }
        });
    }

    public static void resetPassword(Context context, String email, ProgressBar progressBar, OnCompleteListener<Void> listener) {
        if (email.isEmpty()) {
            Toast.makeText(context, "Email is required", Toast.LENGTH_SHORT).show();
            return;
        }
        progressBar.setVisibility(View.VISIBLE);
        mAuth.sendPasswordResetEmail(email)
                .addOnCompleteListener(listener)
                .addOnCompleteListener(task -> {
                    progressBar.setVisibility(View.GONE);
                    if (task.isSuccessful()) {
                        Toast.makeText(context, "Password reset email sent.", Toast.LENGTH_SHORT).show();
                        context.startActivity(new Intent(context, LoginActivity.class));
                    } else {
                        Toast.makeText(context, "Error: " + task.getException().getMessage(), Toast.LENGTH_LONG).show();
                    }
                });
    }

    public static void createUserInFirestore(User user) {
        FirebaseFirestore db = FirebaseFirestore.getInstance();
        db.collection("users").document(user.getId()).set(user)
                .addOnSuccessListener(aVoid -> {
                    // User added successfully
                })
                .addOnFailureListener(e -> {
                    // Handle failure
                });
    }

    public static void sendVerificationEmail(Context context) {
        FirebaseUser user = mAuth.getCurrentUser();
        if (user != null) {
            user.sendEmailVerification()
                    .addOnCompleteListener(task -> {
                        if (task.isSuccessful()) {
                            Toast.makeText(context, "Verification email sent.", Toast.LENGTH_SHORT).show();
                        } else {
                            Toast.makeText(context, "Failed to send verification email.", Toast.LENGTH_SHORT).show();
                        }
                    });
        }
    }

    public static void updateUserStatus(String userId, String newStatus) {
        FirebaseFirestore db = FirebaseFirestore.getInstance();
        db.collection("users").document(userId)
                .update("status", newStatus)
                .addOnSuccessListener(aVoid -> {
                    // Successfully updated user status
                })
                .addOnFailureListener(e -> {
                    // Handle the error
                });
    }
}

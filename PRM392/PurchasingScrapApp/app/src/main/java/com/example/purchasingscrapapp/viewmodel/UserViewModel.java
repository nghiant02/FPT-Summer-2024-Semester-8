package com.example.purchasingscrapapp.viewmodel;

import android.content.Context;
import android.widget.ProgressBar;

import androidx.lifecycle.LiveData;
import androidx.lifecycle.ViewModel;

import com.example.purchasingscrapapp.model.User;
import com.example.purchasingscrapapp.repository.UserRepository;
import com.google.firebase.auth.AuthResult;

import java.util.List;

public class UserViewModel extends ViewModel {
    private UserRepository userRepository;

    public UserViewModel() {
        userRepository = new UserRepository();
    }

    public LiveData<AuthResult> registerUser(Context context, String email, String password, String name, String phone, ProgressBar progressBar) {
        return userRepository.registerUser(context, email, password, name, phone, progressBar);
    }

    public LiveData<AuthResult> registerStaff(Context context, String email, String password, String name, String phone, ProgressBar progressBar) {
        return userRepository.registerStaff(context, email, password, name, phone, progressBar);
    }

    public LiveData<AuthResult> loginUser(Context context, String email, String password, ProgressBar progressBar) {
        return userRepository.loginUser(context, email, password, progressBar);
    }

    public LiveData<Void> resetPassword(Context context, String email, ProgressBar progressBar) {
        return userRepository.resetPassword(context, email, progressBar);
    }

    public void createUserInFirestore(User user) {
        userRepository.createUserInFirestore(user);
    }

    public LiveData<List<User>> getAllUsers() {
        return userRepository.getAllUsers();
    }

    public void updateUserStatus(User user) {
        userRepository.updateUserStatus(user);
    }
}

package com.example.purchasingscrapapp.repository;

import android.content.Context;
import android.widget.ProgressBar;

import androidx.lifecycle.LiveData;
import androidx.lifecycle.MutableLiveData;

import com.example.purchasingscrapapp.model.User;
import com.example.purchasingscrapapp.utils.FirebaseUtils;
import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.auth.AuthResult;
import com.google.firebase.firestore.FirebaseFirestore;
import com.google.firebase.firestore.QueryDocumentSnapshot;

import org.checkerframework.checker.nullness.qual.NonNull;

import java.util.ArrayList;
import java.util.List;

public class UserRepository {

    public LiveData<AuthResult> registerUser(Context context, String email, String password, String name, String phone, ProgressBar progressBar) {
        MutableLiveData<AuthResult> resultLiveData = new MutableLiveData<>();
        FirebaseUtils.registerUser(context, email, password, name, phone, progressBar, new OnCompleteListener<AuthResult>() {
            @Override
            public void onComplete(@NonNull Task<AuthResult> task) {
                if (task.isSuccessful()) {
                    resultLiveData.setValue(task.getResult());
                } else {
                    resultLiveData.setValue(null);
                }
            }
        });
        return resultLiveData;
    }

    public LiveData<AuthResult> registerStaff(Context context, String email, String password, String name, String phone, ProgressBar progressBar) {
        MutableLiveData<AuthResult> resultLiveData = new MutableLiveData<>();
        FirebaseUtils.registerStaff(context, email, password, name, phone, progressBar, new OnCompleteListener<AuthResult>() {
            @Override
            public void onComplete(@NonNull Task<AuthResult> task) {
                if (task.isSuccessful()) {
                    resultLiveData.setValue(task.getResult());
                } else {
                    resultLiveData.setValue(null);
                }
            }
        });
        return resultLiveData;
    }

    public LiveData<AuthResult> loginUser(Context context, String email, String password, ProgressBar progressBar) {
        MutableLiveData<AuthResult> resultLiveData = new MutableLiveData<>();
        FirebaseUtils.loginUser(context, email, password, progressBar, new OnCompleteListener<AuthResult>() {
            @Override
            public void onComplete(@NonNull Task<AuthResult> task) {
                if (task.isSuccessful()) {
                    resultLiveData.setValue(task.getResult());
                } else {
                    resultLiveData.setValue(null);
                }
            }
        });
        return resultLiveData;
    }

    public LiveData<Void> resetPassword(Context context, String email, ProgressBar progressBar) {
        MutableLiveData<Void> resultLiveData = new MutableLiveData<>();
        FirebaseUtils.resetPassword(context, email, progressBar, new OnCompleteListener<Void>() {
            @Override
            public void onComplete(@NonNull Task<Void> task) {
                if (task.isSuccessful()) {
                    resultLiveData.setValue(task.getResult());
                } else {
                    resultLiveData.setValue(null);
                }
            }
        });
        return resultLiveData;
    }

    public void createUserInFirestore(User user) {
        FirebaseUtils.createUserInFirestore(user);
    }

    public LiveData<List<User>> getAllUsers() {
        MutableLiveData<List<User>> usersLiveData = new MutableLiveData<>();
        FirebaseFirestore db = FirebaseFirestore.getInstance();
        db.collection("users").get().addOnCompleteListener(task -> {
            if (task.isSuccessful()) {
                List<User> userList = new ArrayList<>();
                for (QueryDocumentSnapshot document : task.getResult()) {
                    User user = document.toObject(User.class);
                    user.setId(document.getId());
                    userList.add(user);
                }
                usersLiveData.setValue(userList);
            } else {
                usersLiveData.setValue(null);
            }
        });
        return usersLiveData;
    }

    public void updateUserStatus(User user) {
        FirebaseFirestore db = FirebaseFirestore.getInstance();
        db.collection("users").document(user.getId())
                .update("status", user.getStatus())
                .addOnSuccessListener(aVoid -> {
                    // Successfully updated user status
                })
                .addOnFailureListener(e -> {
                    // Handle the error
                });
    }
}

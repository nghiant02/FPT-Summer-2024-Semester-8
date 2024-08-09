package com.example.purchasingscrapapp.utils;

import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseUser;
import com.google.firebase.firestore.DocumentSnapshot;
import com.google.firebase.firestore.FirebaseFirestore;
import com.example.purchasingscrapapp.model.User;

public class UserUtils {
    public static User getCurrentUser() {
        FirebaseUser firebaseUser = FirebaseAuth.getInstance().getCurrentUser();
        if (firebaseUser == null) {
            return null;
        }

        FirebaseFirestore db = FirebaseFirestore.getInstance();
        User[] currentUser = new User[1];
        db.collection("users").document(firebaseUser.getUid()).get().addOnCompleteListener(task -> {
            if (task.isSuccessful()) {
                DocumentSnapshot document = task.getResult();
                if (document.exists()) {
                    currentUser[0] = document.toObject(User.class);
                }
            }
        });

        return currentUser[0];
    }
}

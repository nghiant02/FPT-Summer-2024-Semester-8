package com.example.purchasingscrapapp.repository;

import androidx.lifecycle.LiveData;
import androidx.lifecycle.MutableLiveData;

import com.example.purchasingscrapapp.model.Task;
import com.example.purchasingscrapapp.model.User;
import com.google.firebase.Timestamp;
import com.google.firebase.firestore.CollectionReference;
import com.google.firebase.firestore.FirebaseFirestore;
import com.google.firebase.firestore.QueryDocumentSnapshot;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class TaskRepository {

    private FirebaseFirestore db = FirebaseFirestore.getInstance();
    private CollectionReference tasksRef = db.collection("tasks");
    private CollectionReference usersRef = db.collection("users");

    public LiveData<List<Task>> getTasksByAssignee(String assigneeId) {
        MutableLiveData<List<Task>> tasksData = new MutableLiveData<>();
        tasksRef.whereEqualTo("assigneeId", assigneeId)
                .addSnapshotListener((value, error) -> {
                    if (error != null) {
                        return;
                    }
                    List<Task> tasks = new ArrayList<>();
                    if (value != null) {
                        for (QueryDocumentSnapshot document : value) {
                            Task task = convertToTask(document);
                            tasks.add(task);
                        }
                    }
                    tasksData.setValue(tasks);
                });
        return tasksData;
    }

    public LiveData<Boolean> createTask(Task task) {
        MutableLiveData<Boolean> successData = new MutableLiveData<>();
        task.setCreatedAt(Timestamp.now());
        tasksRef.document(task.getId()).set(task).addOnCompleteListener(taskResult -> {
            boolean success = taskResult.isSuccessful();
            successData.setValue(success);
        });
        return successData;
    }

    public LiveData<Boolean> updateTask(Task task) {
        MutableLiveData<Boolean> successData = new MutableLiveData<>();
        task.setUpdatedAt(Timestamp.now());
        tasksRef.document(task.getId()).set(task).addOnCompleteListener(taskResult -> {
            boolean success = taskResult.isSuccessful();
            successData.setValue(success);
        });
        return successData;
    }

    public LiveData<List<User>> getAllStaff() {
        MutableLiveData<List<User>> staffData = new MutableLiveData<>();
        usersRef.whereEqualTo("role", "staff").addSnapshotListener((value, error) -> {
            if (error != null) {
                return;
            }
            List<User> staffList = new ArrayList<>();
            if (value != null) {
                for (QueryDocumentSnapshot document : value) {
                    User user = document.toObject(User.class);
                    user.setId(document.getId());
                    staffList.add(user);
                }
            }
            staffData.setValue(staffList);
        });
        return staffData;
    }

    private Task convertToTask(QueryDocumentSnapshot document) {
        Task task = document.toObject(Task.class);
        task.setId(document.getId());

        if (document.get("createdAt") instanceof Long) {
            task.setCreatedAt(new Timestamp(new Date((Long) document.get("createdAt"))));
        } else {
            task.setCreatedAt(document.getTimestamp("createdAt"));
        }

        if (document.get("updatedAt") instanceof Long) {
            task.setUpdatedAt(new Timestamp(new Date((Long) document.get("updatedAt"))));
        } else {
            task.setUpdatedAt(document.getTimestamp("updatedAt"));
        }

        return task;
    }
}

package com.example.purchasingscrapapp.viewmodel;

import androidx.lifecycle.LiveData;
import androidx.lifecycle.ViewModel;
import com.example.purchasingscrapapp.model.Task;
import com.example.purchasingscrapapp.model.User;
import com.example.purchasingscrapapp.repository.TaskRepository;

import java.util.List;

public class TaskViewModel extends ViewModel {

    private TaskRepository taskRepository;

    public TaskViewModel() {
        taskRepository = new TaskRepository();
    }

    public LiveData<List<Task>> getTasksByAssignee(String assigneeId) {
        return taskRepository.getTasksByAssignee(assigneeId);
    }

    public LiveData<Boolean> createTask(Task task) {
        return taskRepository.createTask(task);
    }

    public LiveData<Boolean> updateTask(Task task) {
        return taskRepository.updateTask(task);
    }

    public LiveData<List<User>> getAllStaff() {
        return taskRepository.getAllStaff();
    }
}

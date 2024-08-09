package com.example.feedbackmanagementsystem;

import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.feedbackmanagementsystem.Model.Trainee;
import com.example.feedbackmanagementsystem.api.TraineeAdapter;
import com.example.feedbackmanagementsystem.api.TraineeRepository;
import com.example.feedbackmanagementsystem.api.TraineeService;

import java.util.ArrayList;
import java.util.List;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

public class MainActivity extends AppCompatActivity {
    private TraineeService traineeService;
    private EditText etName, etEmail, etPhone, etGender;
    private Button btnSave;
    private RecyclerView recyclerView;
    private TraineeAdapter adapter;
    private List<Trainee> traineeList = new ArrayList<>();
    private Trainee currentTrainee;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        etName = findViewById(R.id.name);
        etEmail = findViewById(R.id.email);
        etPhone = findViewById(R.id.phone);
        etGender = findViewById(R.id.gender);
        btnSave = findViewById(R.id.btnSave);
        recyclerView = findViewById(R.id.recyclerView);

        traineeService = TraineeRepository.getTraineeService();
        recyclerView.setLayoutManager(new LinearLayoutManager(this));
        adapter = new TraineeAdapter(traineeList, this::editTrainee, this::deleteTrainee);
        recyclerView.setAdapter(adapter);

        btnSave.setOnClickListener(v -> saveOrUpdateTrainee());

        fetchTrainees();
    }

    private void saveOrUpdateTrainee() {
        String name = etName.getText().toString();
        String email = etEmail.getText().toString();
        String phone = etPhone.getText().toString();
        String gender = etGender.getText().toString();

        if (name.isEmpty() || email.isEmpty() || phone.isEmpty() || gender.isEmpty()) {
            Toast.makeText(MainActivity.this, "Please fill all fields", Toast.LENGTH_SHORT).show();
            return;
        }

        if (currentTrainee == null) {
            // Add new trainee
            Trainee trainee = new Trainee(name, email, phone, gender);
            traineeService.createTrainee(trainee).enqueue(new Callback<Trainee>() {
                @Override
                public void onResponse(Call<Trainee> call, Response<Trainee> response) {
                    if (response.isSuccessful()) {
                        Toast.makeText(MainActivity.this, "Trainee saved successfully!", Toast.LENGTH_SHORT).show();
                        traineeList.add(response.body());
                        adapter.notifyDataSetChanged();
                        clearFields();
                    }
                }

                @Override
                public void onFailure(Call<Trainee> call, Throwable t) {
                    Toast.makeText(MainActivity.this, "Failed to save trainee!", Toast.LENGTH_SHORT).show();
                }
            });
        } else {
            // Update existing trainee
            currentTrainee.setName(name);
            currentTrainee.setEmail(email);
            currentTrainee.setPhone(phone);
            currentTrainee.setGender(gender);

            traineeService.updateTrainee(currentTrainee.getId(), currentTrainee).enqueue(new Callback<Trainee>() {
                @Override
                public void onResponse(Call<Trainee> call, Response<Trainee> response) {
                    if (response.isSuccessful()) {
                        Toast.makeText(MainActivity.this, "Trainee updated successfully!", Toast.LENGTH_SHORT).show();
                        fetchTrainees();
                        clearFields();
                    }
                }

                @Override
                public void onFailure(Call<Trainee> call, Throwable t) {
                    Toast.makeText(MainActivity.this, "Failed to update trainee!", Toast.LENGTH_SHORT).show();
                }
            });
        }
    }

    private void fetchTrainees() {
        traineeService.getAllTrainees().enqueue(new Callback<List<Trainee>>() {
            @Override
            public void onResponse(Call<List<Trainee>> call, Response<List<Trainee>> response) {
                if (response.isSuccessful()) {
                    traineeList.clear();
                    traineeList.addAll(response.body());
                    adapter.notifyDataSetChanged();
                }
            }

            @Override
            public void onFailure(Call<List<Trainee>> call, Throwable t) {
                Toast.makeText(MainActivity.this, "Failed to fetch trainees!", Toast.LENGTH_SHORT).show();
            }
        });
    }

    private void editTrainee(Trainee trainee) {
        currentTrainee = trainee;
        etName.setText(trainee.getName());
        etEmail.setText(trainee.getEmail());
        etPhone.setText(trainee.getPhone());
        etGender.setText(trainee.getGender());
        btnSave.setText("CẬP NHẬT");
    }

    private void deleteTrainee(Trainee trainee) {
        traineeService.deleteTrainee(trainee.getId()).enqueue(new Callback<Void>() {
            @Override
            public void onResponse(Call<Void> call, Response<Void> response) {
                if (response.isSuccessful()) {
                    Toast.makeText(MainActivity.this, "Trainee deleted successfully!", Toast.LENGTH_SHORT).show();
                    traineeList.remove(trainee);
                    adapter.notifyDataSetChanged();
                }
            }

            @Override
            public void onFailure(Call<Void> call, Throwable t) {
                Toast.makeText(MainActivity.this, "Failed to delete trainee!", Toast.LENGTH_SHORT).show();
            }
        });
    }

    private void clearFields() {
        currentTrainee = null;
        etName.setText("");
        etEmail.setText("");
        etPhone.setText("");
        etGender.setText("");
        btnSave.setText("LƯU DATA");
    }
}

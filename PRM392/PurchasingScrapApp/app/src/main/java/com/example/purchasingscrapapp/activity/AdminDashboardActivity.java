package com.example.purchasingscrapapp.activity;

import android.content.Intent;
import android.os.Bundle;
import androidx.appcompat.app.AppCompatActivity;
import com.example.purchasingscrapapp.R;
import com.google.firebase.auth.FirebaseAuth;

public class AdminDashboardActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_admin_dashboard);

        findViewById(R.id.buttonManageUsers).setOnClickListener(v -> {
            startActivity(new Intent(AdminDashboardActivity.this, ManageUsersActivity.class));
        });

        findViewById(R.id.buttonCreateStaff).setOnClickListener(v -> {
            startActivity(new Intent(AdminDashboardActivity.this, CreateStaffActivity.class));
        });

        findViewById(R.id.buttonManageStaff).setOnClickListener(v -> {
            startActivity(new Intent(AdminDashboardActivity.this, ManageStaffActivity.class));
        });

        findViewById(R.id.buttonManageScrapPosts).setOnClickListener(v -> {
            startActivity(new Intent(AdminDashboardActivity.this, ManageScrapPostsActivity.class));
        });

        findViewById(R.id.buttonAssignTasks).setOnClickListener(v -> {
            startActivity(new Intent(AdminDashboardActivity.this, AssignTasksActivity.class));
        });

        findViewById(R.id.buttonViewReports).setOnClickListener(v -> {
            startActivity(new Intent(AdminDashboardActivity.this, ViewReportsActivity.class));
        });

        findViewById(R.id.buttonLogout).setOnClickListener(v -> {
            FirebaseAuth.getInstance().signOut();
            startActivity(new Intent(AdminDashboardActivity.this, LoginActivity.class));
            finish();
        });
    }
}

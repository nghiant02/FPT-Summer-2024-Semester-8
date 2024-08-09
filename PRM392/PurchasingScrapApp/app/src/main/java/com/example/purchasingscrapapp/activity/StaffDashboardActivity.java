package com.example.purchasingscrapapp.activity;

import android.content.Intent;
import android.os.Bundle;
import androidx.appcompat.app.AppCompatActivity;
import com.example.purchasingscrapapp.R;
import com.example.purchasingscrapapp.model.User;
import com.example.purchasingscrapapp.utils.Permission;
import com.example.purchasingscrapapp.utils.UserUtils;
import com.google.firebase.auth.FirebaseAuth;

public class StaffDashboardActivity extends AppCompatActivity {

    private User currentUser;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_staff_dashboard);

        currentUser = UserUtils.getCurrentUser();

        findViewById(R.id.button_view_scrap_posts).setOnClickListener(v -> {
            startActivity(new Intent(StaffDashboardActivity.this, ManageScrapPostsActivity.class));
        });

        findViewById(R.id.button_manage_tasks).setOnClickListener(v -> {
            startActivity(new Intent(StaffDashboardActivity.this, ManageTasksActivity.class));
        });

        findViewById(R.id.button_logout).setOnClickListener(v -> {
            FirebaseAuth.getInstance().signOut();
            startActivity(new Intent(StaffDashboardActivity.this, LoginActivity.class));
            finish();
        });
    }

    private boolean hasPermission(String permission) {
        if (currentUser == null) return false;
        if (currentUser.getRole().equals("admin")) return true;

        return false;
    }
}

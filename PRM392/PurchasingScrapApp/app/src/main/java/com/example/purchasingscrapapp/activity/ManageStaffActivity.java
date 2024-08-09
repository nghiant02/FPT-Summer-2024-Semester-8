package com.example.purchasingscrapapp.activity;

import android.os.Bundle;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;
import androidx.lifecycle.ViewModelProvider;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.purchasingscrapapp.R;
import com.example.purchasingscrapapp.adapter.UserAdapter;
import com.example.purchasingscrapapp.model.User;
import com.example.purchasingscrapapp.viewmodel.UserViewModel;

import java.util.List;
import java.util.stream.Collectors;

public class ManageStaffActivity extends AppCompatActivity {

    private UserViewModel userViewModel;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_manage_staff);

        RecyclerView recyclerView = findViewById(R.id.recyclerViewStaff);
        recyclerView.setLayoutManager(new LinearLayoutManager(this));
        recyclerView.setHasFixedSize(true);

        UserAdapter adapter = new UserAdapter();
        recyclerView.setAdapter(adapter);

        userViewModel = new ViewModelProvider(this).get(UserViewModel.class);
        userViewModel.getAllUsers().observe(this, users -> {
            List<User> staffOnlyList = users.stream().filter(user -> "staff".equals(user.getRole())).collect(Collectors.toList());
            adapter.setUsers(staffOnlyList);
        });

        adapter.setOnItemClickListener(new UserAdapter.OnItemClickListener() {
            @Override
            public void onBlockUnblockClick(User user) {
                String newStatus = user.getStatus().equals("active") ? "blocked" : "active";
                user.setStatus(newStatus);
                userViewModel.updateUserStatus(user);
                Toast.makeText(ManageStaffActivity.this, "Staff status updated to " + newStatus, Toast.LENGTH_SHORT).show();
                adapter.notifyItemChanged(adapter.getUsers().indexOf(user));
            }

            @Override
            public void onItemClick(User user) {
                // Handle staff details view
                // You can start a new activity to show staff details
            }
        });
    }
}

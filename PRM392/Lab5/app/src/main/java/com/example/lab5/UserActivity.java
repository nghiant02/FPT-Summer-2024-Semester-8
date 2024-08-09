package com.example.lab5;

import android.os.Bundle;

import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import java.util.ArrayList;

public class UserActivity extends AppCompatActivity {

    ArrayList<User> userList;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_user);

        RecyclerView rvUser = findViewById(R.id.rvUser);

        userList = new ArrayList<>();
        userList.add(new User("NguyenTT", "Tran Thanh Nguyen", "nguyentt@ftp.edu.vn"));
        userList.add(new User("Antv", "Tran Van An", "antv@gmail.com"));
        userList.add(new User("BangTT", "Tran Thanh Bang", "bangtt@gmail.com"));
        userList.add(new User("KhangTT", "Tran Thanh Khang", "khangtt@gmail.com"));
        userList.add(new User("BaoNT", "Nguyen Thanh Bao", "baott@gmail.com"));
        userList.add(new User("HungVH", "Vo Huy Hung", "hungvh@gmail.com"));

        UserAdapter adapter = new UserAdapter(userList);
        rvUser.setAdapter(adapter);
        rvUser.setLayoutManager(new LinearLayoutManager(this));
    }
}

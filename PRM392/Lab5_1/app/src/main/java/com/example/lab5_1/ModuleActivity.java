package com.example.lab5_1;

import android.os.Bundle;

import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import java.util.ArrayList;
import java.util.List;

public class ModuleActivity extends AppCompatActivity {

    List<Module> moduleList;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_module);

        RecyclerView rvModule = findViewById(R.id.rvModule);

        moduleList = new ArrayList<>();
        moduleList.add(new Module("ListView trong Android", "Listview trong Android là một thành phần dùng để nhóm nhiều mục (item) và...", "Android", R.drawable.ic_android));
        moduleList.add(new Module("Xử lý sự kiện trong iOS", "Xử lý sự kiện trong iOS. Sau khi các bạn đã biết cách thiết kế giao diện cho các ứng...", "iOS", R.drawable.ic_ios));

        ModuleAdapter adapter = new ModuleAdapter(moduleList);
        rvModule.setAdapter(adapter);
        rvModule.setLayoutManager(new LinearLayoutManager(this));
    }
}

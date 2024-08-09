package com.example.purchasingscrapapp.activity;

import android.content.Intent;
import android.os.Bundle;
import androidx.appcompat.app.AppCompatActivity;
import com.example.purchasingscrapapp.R;
import com.google.firebase.auth.FirebaseAuth;

public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        findViewById(R.id.buttonViewScrapList).setOnClickListener(v -> startActivity(new Intent(MainActivity.this, ScrapListActivity.class)));

        findViewById(R.id.buttonPostScrap).setOnClickListener(v -> startActivity(new Intent(MainActivity.this, PostScrapActivity.class)));

        findViewById(R.id.buttonSignOut).setOnClickListener(v -> {
            FirebaseAuth.getInstance().signOut();
            startActivity(new Intent(MainActivity.this, LoginActivity.class));
            finish();
        });

    }
}

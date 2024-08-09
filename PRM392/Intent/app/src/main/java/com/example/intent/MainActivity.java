package com.example.intent;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import androidx.appcompat.app.AppCompatActivity;

public class MainActivity extends AppCompatActivity {

    private static final String TAG = "MainActivity";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        Log.d(TAG, "onCreate");

        Button button = findViewById(R.id.button);
        button.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(MainActivity.this, SecondActivity.class);

                // Passing a string
                intent.putExtra("stringKey", "Hello, SecondActivity!");

                // Passing a number
                intent.putExtra("numberKey", 123);

                // Passing an array
                int[] intArray = {1, 2, 3, 4, 5};
                intent.putExtra("arrayKey", intArray);

                // Passing an object
                MyObject myObject = new MyObject("example", 42);
                intent.putExtra("objectKey", myObject);

                // Passing a bundle
                Bundle bundle = new Bundle();
                bundle.putString("bundleStringKey", "This is a string in a bundle");
                bundle.putInt("bundleNumberKey", 456);
                intent.putExtra("bundleKey", bundle);

                startActivity(intent);
            }
        });
    }

    @Override
    protected void onStart() {
        super.onStart();
        Log.d(TAG, "onStart");
    }

    @Override
    protected void onResume() {
        super.onResume();
        Log.d(TAG, "onResume");
    }

    @Override
    protected void onPause() {
        super.onPause();
        Log.d(TAG, "onPause");
    }

    @Override
    protected void onStop() {
        super.onStop();
        Log.d(TAG, "onStop");
    }

    @Override
    protected void onDestroy() {
        super.onDestroy();
        Log.d(TAG, "onDestroy");
    }

    public static class MyObject implements java.io.Serializable {
        String name;
        int value;

        public MyObject(String name, int value) {
            this.name = name;
            this.value = value;
        }
    }
}

package com.example.intent;

import android.content.Intent;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import androidx.appcompat.app.AppCompatActivity;

public class SecondActivity extends AppCompatActivity {

    private static final String TAG = "SecondActivity";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_second);
        Log.d(TAG, "onCreate");

        TextView receivedData = findViewById(R.id.receivedData);

        // Receiving a string
        String receivedString = getIntent().getStringExtra("stringKey");

        // Receiving a number
        int receivedNumber = getIntent().getIntExtra("numberKey", 0);

        // Receiving an array
        int[] receivedArray = getIntent().getIntArrayExtra("arrayKey");

        // Receiving an object
        MainActivity.MyObject receivedObject = (MainActivity.MyObject) getIntent().getSerializableExtra("objectKey");

        // Receiving a bundle
        Bundle receivedBundle = getIntent().getBundleExtra("bundleKey");
        String bundleString = receivedBundle.getString("bundleStringKey");
        int bundleNumber = receivedBundle.getInt("bundleNumberKey");

        // Displaying received data
        String displayText = "String: " + receivedString +
                "\nNumber: " + receivedNumber +
                "\nArray: " + java.util.Arrays.toString(receivedArray) +
                "\nObject: " + receivedObject.name + ", " + receivedObject.value +
                "\nBundle String: " + bundleString +
                "\nBundle Number: " + bundleNumber;

        receivedData.setText(displayText);

        Button button2 = findViewById(R.id.button2);
        button2.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(SecondActivity.this, MainActivity.class);
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
}

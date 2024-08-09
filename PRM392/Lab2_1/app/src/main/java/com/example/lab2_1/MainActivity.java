package com.example.lab2_1;

import android.os.Bundle;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import androidx.appcompat.app.AppCompatActivity;

import java.util.Random;

public class MainActivity extends AppCompatActivity {

    private EditText minEditText;
    private EditText maxEditText;
    private Button generateButton;
    private TextView resultTextView;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        minEditText = findViewById(R.id.editTextText);
        maxEditText = findViewById(R.id.editTextText2);
        generateButton = findViewById(R.id.button);
        resultTextView = findViewById(R.id.textView5);

        generateButton.setOnClickListener(v -> {
            String minStr = minEditText.getText().toString();
            String maxStr = maxEditText.getText().toString();

            if (!minStr.isEmpty() && !maxStr.isEmpty()) {
                int min = Integer.parseInt(minStr);
                int max = Integer.parseInt(maxStr);

                if (min <= max) {
                    int randomValue = new Random().nextInt((max - min) + 1) + min;
                    resultTextView.setText(String.valueOf(randomValue));
                } else {
                    resultTextView.setText("Invalid range");
                }
            } else {
                resultTextView.setText("Please enter both min and max values");
            }
        });
    }
}

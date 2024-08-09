package com.example.lab4;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;

public class MainActivity extends AppCompatActivity {
    private TextView selectedFoodDrinkTextView;
    private static final int REQUEST_FOOD = 1;
    private static final int REQUEST_DRINK = 2;
    private String selectedFood = "";
    private String selectedDrink = "";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        selectedFoodDrinkTextView = findViewById(R.id.selectedFoodDrinkTextView);

        Button selectFoodButton = findViewById(R.id.selectFoodButton);
        selectFoodButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(MainActivity.this, FoodActivity.class);
                startActivityForResult(intent, REQUEST_FOOD);
            }
        });

        Button selectDrinkButton = findViewById(R.id.selectDrinkButton);
        selectDrinkButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(MainActivity.this, DrinkActivity.class);
                startActivityForResult(intent, REQUEST_DRINK);
            }
        });

        Button exitButton = findViewById(R.id.exitButton);
        exitButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                finish();
            }
        });
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, @Nullable Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        if (resultCode == RESULT_OK && data != null) {
            if (requestCode == REQUEST_FOOD) {
                selectedFood = data.getStringExtra("selectedFood");
            } else if (requestCode == REQUEST_DRINK) {
                selectedDrink = data.getStringExtra("selectedDrink");
            }
            updateSelectedFoodDrinkTextView();
        }
    }

    private void updateSelectedFoodDrinkTextView() {
        String displayText = (selectedFood != null && !selectedFood.isEmpty() ? selectedFood : "No food selected") +
                " - " +
                (selectedDrink != null && !selectedDrink.isEmpty() ? selectedDrink : "No drink selected");
        selectedFoodDrinkTextView.setText(displayText);
    }
}

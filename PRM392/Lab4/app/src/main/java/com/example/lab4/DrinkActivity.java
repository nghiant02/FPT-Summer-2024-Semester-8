package com.example.lab4;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListView;
import androidx.appcompat.app.AppCompatActivity;

public class DrinkActivity extends AppCompatActivity {
    private String[] drinks = {"Pepsi", "Heineken", "Tiger", "Sài gòn Đỏ"};
    private String selectedDrink = "";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_drink);

        ListView drinkListView = findViewById(R.id.drinkListView);
        ArrayAdapter<String> adapter = new ArrayAdapter<>(this, android.R.layout.simple_list_item_single_choice, drinks);
        drinkListView.setAdapter(adapter);
        drinkListView.setChoiceMode(ListView.CHOICE_MODE_SINGLE);
        drinkListView.setOnItemClickListener((parent, view, position, id) -> selectedDrink = drinks[position]);

        Button orderDrinkButton = findViewById(R.id.orderDrinkButton);
        orderDrinkButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent resultIntent = new Intent();
                resultIntent.putExtra("selectedDrink", selectedDrink);
                setResult(RESULT_OK, resultIntent);
                finish();
            }
        });
    }
}

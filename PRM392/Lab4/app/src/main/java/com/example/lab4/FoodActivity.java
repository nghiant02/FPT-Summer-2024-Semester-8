package com.example.lab4;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListView;
import androidx.appcompat.app.AppCompatActivity;

public class FoodActivity extends AppCompatActivity {
    private String[] foods = {"Phở Hà Nội", "Bún Bò Huế", "Mì Quảng", "Hủ Tíu Sài Gòn"};
    private String selectedFood = "";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_food);

        ListView foodListView = findViewById(R.id.foodListView);
        ArrayAdapter<String> adapter = new ArrayAdapter<>(this, android.R.layout.simple_list_item_single_choice, foods);
        foodListView.setAdapter(adapter);
        foodListView.setChoiceMode(ListView.CHOICE_MODE_SINGLE);
        foodListView.setOnItemClickListener((parent, view, position, id) -> selectedFood = foods[position]);

        Button orderFoodButton = findViewById(R.id.orderFoodButton);
        orderFoodButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent resultIntent = new Intent();
                resultIntent.putExtra("selectedFood", selectedFood);
                setResult(RESULT_OK, resultIntent);
                finish();
            }
        });
    }
}

package com.example.lab3_2;

import android.os.Bundle;
import android.widget.ListView;

import androidx.appcompat.app.AppCompatActivity;

import java.util.ArrayList;

public class MainActivity extends AppCompatActivity {

    private ListView listView;
    private ArrayList<Player> playerList;
    private PlayerAdapter playerAdapter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        listView = findViewById(R.id.listView);
        playerList = new ArrayList<>();

        // Add sample data
        playerList.add(new Player("Pele", "October 23, 1940 (age 73)", R.drawable.pele, R.drawable.brazil_flag));
        playerList.add(new Player("Diego Maradona", "October 30, 1960 (age 52)", R.drawable.maradona, R.drawable.argentina_flag));
        playerList.add(new Player("Johan Cruyff", "April 25, 1947 (age 65)", R.drawable.cruyff, R.drawable.netherlands_flag));
        playerList.add(new Player("Franz Beckenbauer", "September 11, 1945 (age 67)", R.drawable.beckenbauer, R.drawable.germany_flag));
        playerList.add(new Player("Michel Platini", "June 21, 1955 (age 57)", R.drawable.platini, R.drawable.france_flag));
        playerList.add(new Player("Ronaldo De Lima", "September 22, 1976 (age 36)", R.drawable.ronaldo, R.drawable.brazil_flag));

        playerAdapter = new PlayerAdapter(this, playerList);
        listView.setAdapter(playerAdapter);
    }
}

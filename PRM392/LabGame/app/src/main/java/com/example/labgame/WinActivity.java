package com.example.labgame;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.TextView;
import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;

public class WinActivity extends AppCompatActivity {
    private int currentMoney;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_win);

        TextView winnerTextView = findViewById(R.id.winnerTextView);
        Button backButton = findViewById(R.id.backButton);

        int winner = getIntent().getIntExtra("winner", 0);
        currentMoney = getIntent().getIntExtra("currentMoney", 0);
        String raceResultMessage = getIntent().getStringExtra("raceResultMessage");
        winnerTextView.setText("Winner: Horse " + winner);

        backButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent();
                intent.putExtra("currentMoney", currentMoney);
                setResult(RESULT_OK, intent);
                finish();
            }
        });

        // Show race result message
        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setTitle("Race Result")
                .setMessage(raceResultMessage)
                .setPositiveButton("OK", null)
                .show();
    }
}

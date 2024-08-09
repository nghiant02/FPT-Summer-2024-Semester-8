package com.example.labgame;

import android.content.Intent;
import android.content.SharedPreferences;
import android.media.MediaPlayer;
import android.os.Bundle;
import android.os.Handler;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.SeekBar;
import android.widget.TextView;
import android.widget.Toast;
import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;
import java.util.Random;

public class MainActivity extends AppCompatActivity {

    private SeekBar horse1, horse2, horse3;
    private EditText betHorse1, betHorse2, betHorse3;
    private TextView moneyTextView;
    private Button startButton, resetButton, logoutButton, helpButton;
    private int money = 1000;
    private Handler handler = new Handler();
    private Random random = new Random();
    private boolean raceRunning = false;
    private boolean raceFinished = false;
    private MediaPlayer mediaPlayer;
    private static final int INITIAL_MONEY = 1000;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        horse1 = findViewById(R.id.horse1);
        horse2 = findViewById(R.id.horse2);
        horse3 = findViewById(R.id.horse3);
        betHorse1 = findViewById(R.id.betHorse1);
        betHorse2 = findViewById(R.id.betHorse2);
        betHorse3 = findViewById(R.id.betHorse3);
        moneyTextView = findViewById(R.id.moneyTextView);
        startButton = findViewById(R.id.startButton);
        resetButton = findViewById(R.id.resetButton);
        logoutButton = findViewById(R.id.logoutButton);
        helpButton = findViewById(R.id.helpButton);

        // Make SeekBars read-only
        horse1.setEnabled(false);
        horse2.setEnabled(false);
        horse3.setEnabled(false);

        // Restore money from SharedPreferences
        SharedPreferences prefs = getSharedPreferences("game_prefs", MODE_PRIVATE);
        money = prefs.getInt("currentMoney", INITIAL_MONEY);
        updateMoneyTextView();

        startButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (money <= 0) {
                    showNoMoneyDialog();
                    return;
                }
                if (!raceRunning && !raceFinished) {
                    startRace();
                } else {
                    Toast.makeText(MainActivity.this, "Please reset the race before starting a new one.", Toast.LENGTH_SHORT).show();
                }
            }
        });

        resetButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                resetRace();
            }
        });

        logoutButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                logout();
            }
        });

        helpButton.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                openHelp();
            }
        });

        // Ensure reset button is enabled initially
        resetButton.setEnabled(true);
    }

    private void startRace() {
        int bet1 = Integer.parseInt(betHorse1.getText().toString().isEmpty() ? "0" : betHorse1.getText().toString());
        int bet2 = Integer.parseInt(betHorse2.getText().toString().isEmpty() ? "0" : betHorse2.getText().toString());
        int bet3 = Integer.parseInt(betHorse3.getText().toString().isEmpty() ? "0" : betHorse3.getText().toString());
        int totalBet = bet1 + bet2 + bet3;

        if (totalBet <= 0) {
            Toast.makeText(this, "You must place a bet to start the race!", Toast.LENGTH_SHORT).show();
            return;
        }

        if (totalBet > money) {
            Toast.makeText(this, "You cannot bet more than your total money!", Toast.LENGTH_SHORT).show();
            return;
        }

        // Deduct bet amount from money when starting the race
        money -= totalBet;
        updateMoneyTextView();

        raceRunning = true;
        raceFinished = false;

        // Disable fields and buttons except Start button
        setFieldsAndButtonsEnabled(false);

        mediaPlayer = MediaPlayer.create(this, R.raw.race_sound);
        mediaPlayer.start();

        handler.postDelayed(new Runnable() {
            @Override
            public void run() {
                if (raceRunning) {
                    horse1.setProgress(horse1.getProgress() + random.nextInt(5));
                    horse2.setProgress(horse2.getProgress() + random.nextInt(5));
                    horse3.setProgress(horse3.getProgress() + random.nextInt(5));

                    if (horse1.getProgress() >= 100) {
                        endRace(1, bet1, bet2, bet3);
                    } else if (horse2.getProgress() >= 100) {
                        endRace(2, bet1, bet2, bet3);
                    } else if (horse3.getProgress() >= 100) {
                        endRace(3, bet1, bet2, bet3);
                    } else {
                        handler.postDelayed(this, 100);
                    }
                }
            }
        }, 100);
    }

    private void endRace(int winner, int bet1, int bet2, int bet3) {
        raceRunning = false;
        raceFinished = true;
        if (mediaPlayer != null && mediaPlayer.isPlaying()) {
            mediaPlayer.stop();
            mediaPlayer.release();
            mediaPlayer = null;
        }

        StringBuilder message = new StringBuilder("Horse " + winner + " wins!\n");
        int winAmount = 0;
        int lossAmount = 0;

        if (winner == 1) {
            winAmount += 2 * bet1;
            lossAmount += bet2 + bet3;
        } else if (winner == 2) {
            winAmount += 2 * bet2;
            lossAmount += bet1 + bet3;
        } else if (winner == 3) {
            winAmount += 2 * bet3;
            lossAmount += bet1 + bet2;
        }

        money += winAmount;
        updateMoneyTextView();

        if (winAmount > 0) {
            message.append("You win $").append(winAmount / 2).append(" from betting on horse ").append(winner).append("!\n");
        }
        if (lossAmount > 0) {
            message.append("You lose $").append(lossAmount).append(" from betting on other horses.");
        }

        // Pass message to WinActivity
        Intent intent = new Intent(this, WinActivity.class);
        intent.putExtra("winner", winner);
        intent.putExtra("currentMoney", money);
        intent.putExtra("raceResultMessage", message.toString());
        startActivityForResult(intent, 1);

        // Enable fields and buttons except Start button
        setFieldsAndButtonsEnabled(true);
    }

    private void resetRace() {
        raceRunning = false;
        raceFinished = false;
        if (mediaPlayer != null && mediaPlayer.isPlaying()) {
            mediaPlayer.stop();
            mediaPlayer.release();
            mediaPlayer = null;
        }
        horse1.setProgress(0);
        horse2.setProgress(0);
        horse3.setProgress(0);
        betHorse1.setText("");
        betHorse2.setText("");
        betHorse3.setText("");

        // Enable fields and buttons
        setFieldsAndButtonsEnabled(true);
    }

    private void showNoMoneyDialog() {
        AlertDialog.Builder builder = new AlertDialog.Builder(this);
        builder.setTitle("No Money Left")
                .setMessage("You have no money left. Would you like to reset your balance to $1000?")
                .setPositiveButton("Yes", (dialog, which) -> {
                    money = INITIAL_MONEY;
                    updateMoneyTextView();
                    resetRace();
                })
                .setNegativeButton("No", null)
                .show();
    }

    private void logout() {
        // Save money to SharedPreferences before logging out
        SharedPreferences.Editor editor = getSharedPreferences("game_prefs", MODE_PRIVATE).edit();
        editor.putInt("currentMoney", money);
        editor.apply();

        Intent intent = new Intent(MainActivity.this, LoginActivity.class);
        startActivity(intent);
        finish();
    }

    private void openHelp() {
        Intent intent = new Intent(MainActivity.this, HelpActivity.class);
        startActivity(intent);
    }

    private void updateMoneyTextView() {
        moneyTextView.setText("Money: $" + money);
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        if (requestCode == 1 && resultCode == RESULT_OK) {
            if (data != null) {
                money = data.getIntExtra("currentMoney", money);
                updateMoneyTextView();
                resetRace(); // Reset the race state when returning from the WinActivity
            }
        }
    }

    @Override
    protected void onPause() {
        super.onPause();
        // Save money to SharedPreferences
        SharedPreferences.Editor editor = getSharedPreferences("game_prefs", MODE_PRIVATE).edit();
        editor.putInt("currentMoney", money);
        editor.apply();
    }

    private void setFieldsAndButtonsEnabled(boolean enabled) {
        betHorse1.setEnabled(enabled);
        betHorse2.setEnabled(enabled);
        betHorse3.setEnabled(enabled);
        startButton.setEnabled(enabled);
        resetButton.setEnabled(enabled);
        logoutButton.setEnabled(enabled);
        helpButton.setEnabled(enabled);
    }
}

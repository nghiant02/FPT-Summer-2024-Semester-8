package com.example.lab3_2;

import android.app.Activity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.ImageView;
import android.widget.TextView;

import java.util.List;

public class PlayerAdapter extends ArrayAdapter<Player> {

    private Activity context;
    private List<Player> players;

    public PlayerAdapter(Activity context, List<Player> players) {
        super(context, R.layout.list_item, players);
        this.context = context;
        this.players = players;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        LayoutInflater inflater = context.getLayoutInflater();
        View listItemView = inflater.inflate(R.layout.list_item, null, true);

        ImageView imageView = listItemView.findViewById(R.id.imageView);
        TextView nameTextView = listItemView.findViewById(R.id.nameTextView);
        TextView detailTextView = listItemView.findViewById(R.id.detailTextView);
        ImageView flagImageView = listItemView.findViewById(R.id.flagImageView);

        Player currentPlayer = players.get(position);

        imageView.setImageResource(currentPlayer.getImageResourceId());
        nameTextView.setText(currentPlayer.getName());
        detailTextView.setText(currentPlayer.getDetails());
        flagImageView.setImageResource(currentPlayer.getFlagResourceId());

        return listItemView;
    }
}

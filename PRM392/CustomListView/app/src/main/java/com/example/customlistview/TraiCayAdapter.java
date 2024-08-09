package com.example.customlistview;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.ImageView;
import android.widget.TextView;

import java.util.List;

public class TraiCayAdapter extends BaseAdapter {
    private Context context;
    private int layout;
    private List<TraiCay> traicayList;

    public TraiCayAdapter(Context context, int layout, List<TraiCay> traicayList) {
        this.context = context;
        this.layout = layout;
        this.traicayList = traicayList;
    }

    @Override
    public int getCount() {
        return traicayList.size();
    }

    @Override
    public Object getItem(int i) {
        return traicayList.get(i);
    }

    @Override
    public long getItemId(int i) {
        return i;
    }

    @Override
    public View getView(int i, View view, ViewGroup viewGroup) {
        LayoutInflater inflater = (LayoutInflater) context.getSystemService(Context.LAYOUT_INFLATER_SERVICE);
        if (view == null) {
            view = inflater.inflate(layout, null);
        }

        // Find views by ID
        ImageView imgHinh = view.findViewById(R.id.imageviewHinh);
        TextView txtTen = view.findViewById(R.id.textviewTen);
        TextView txtMota = view.findViewById(R.id.textviewMoTa);

        // Assign values to views
        TraiCay traicay = traicayList.get(i);
        txtTen.setText(traicay.getTen());
        txtMota.setText(traicay.getMota());
        imgHinh.setImageResource(traicay.getHinh());

        return view;
    }
}

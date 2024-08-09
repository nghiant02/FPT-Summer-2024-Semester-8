package com.example.lab9;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;

import java.util.List;

public class CongViecAdapter extends ArrayAdapter<CongViec> {
    public CongViecAdapter(Context context, int resource, List<CongViec> objects) {
        super(context, resource, objects);
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        if (convertView == null) {
            convertView = LayoutInflater.from(getContext()).inflate(R.layout.dong_cong_viec, parent, false);
        }

        CongViec congviec = getItem(position);
        if (congviec != null) {
            TextView tvTenCV = convertView.findViewById(R.id.tvTenCV);
            TextView tvNoiDung = convertView.findViewById(R.id.tvNoiDung);

            tvTenCV.setText(congviec.getTenCV());
            tvNoiDung.setText(congviec.getNoiDung());
        }

        return convertView;
    }
}

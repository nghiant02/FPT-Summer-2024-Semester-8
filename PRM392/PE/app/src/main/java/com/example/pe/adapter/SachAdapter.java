package com.example.pe.adapter;

import android.app.AlertDialog;
import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.TextView;
import android.widget.Toast;

import com.example.pe.R;
import com.example.pe.activity.ManageSachActivity;
import com.example.pe.helper.DatabaseHelper;
import com.example.pe.model.Sach;

import java.util.List;

public class SachAdapter extends ArrayAdapter<Sach> {
    private DatabaseHelper dbHelper;

    public SachAdapter(Context context, List<Sach> sachs, DatabaseHelper dbHelper) {
        super(context, 0, sachs);
        this.dbHelper = dbHelper;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        Sach sach = getItem(position);

        if (convertView == null) {
            convertView = LayoutInflater.from(getContext()).inflate(R.layout.sach_item, parent, false);
        }

        TextView tvTenSach = convertView.findViewById(R.id.tvTenSach);
        TextView tvNgayXb = convertView.findViewById(R.id.tvNgayXb);
        TextView tvTheLoai = convertView.findViewById(R.id.tvTheLoai);
        TextView tvTacgia = convertView.findViewById(R.id.tvTacgia);

        tvTenSach.setText(sach.getTenSach());
        tvNgayXb.setText(sach.getNgayXb());
        tvTheLoai.setText(sach.getTheLoai());

        String tacgiaName = getTacgiaName(sach.getIdTacgia());
        tvTacgia.setText(tacgiaName);

        convertView.setOnClickListener(v -> {
            ((ManageSachActivity) getContext()).setSelectedSach(sach);
        });

        convertView.setOnLongClickListener(v -> {
            new AlertDialog.Builder(getContext())
                    .setTitle("Delete Sach")
                    .setMessage("Are you sure you want to delete this sách?")
                    .setPositiveButton("Yes", (dialog, which) -> {
                        deleteSach(sach);
                        Toast.makeText(getContext(), "Sách deleted", Toast.LENGTH_SHORT).show();
                    })
                    .setNegativeButton("No", null)
                    .show();
            return true;
        });

        return convertView;
    }

    private String getTacgiaName(int idTacgia) {
        SQLiteDatabase db = dbHelper.getReadableDatabase();
        Cursor cursor = db.query("Tacgia", new String[]{"tenTacgia"}, "idTacgia=?", new String[]{String.valueOf(idTacgia)}, null, null, null);

        if (cursor != null && cursor.moveToFirst()) {
            String tacgiaName = cursor.getString(cursor.getColumnIndexOrThrow("tenTacgia"));
            cursor.close();
            return tacgiaName;
        }

        return "Unknown Tacgia";
    }

    private void deleteSach(Sach sach) {
        SQLiteDatabase db = dbHelper.getWritableDatabase();
        db.delete("Sach", "idSach=?", new String[]{String.valueOf(sach.getIdSach())});
        remove(sach);
        notifyDataSetChanged();
    }
}

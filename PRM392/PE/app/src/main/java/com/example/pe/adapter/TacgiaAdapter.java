package com.example.pe.adapter;

import android.app.AlertDialog;
import android.content.Context;
import android.content.Intent;
import android.database.sqlite.SQLiteDatabase;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.TextView;
import android.widget.Toast;

import com.example.pe.R;
import com.example.pe.activity.ManageTacgiaActivity;
import com.example.pe.activity.MapActivity;
import com.example.pe.helper.DatabaseHelper;
import com.example.pe.model.Tacgia;

import java.util.List;

public class TacgiaAdapter extends ArrayAdapter<Tacgia> {
    private DatabaseHelper dbHelper;

    public TacgiaAdapter(Context context, List<Tacgia> tacgias, DatabaseHelper dbHelper) {
        super(context, 0, tacgias);
        this.dbHelper = dbHelper;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        Tacgia tacgia = getItem(position);

        if (convertView == null) {
            convertView = LayoutInflater.from(getContext()).inflate(R.layout.tacgia_item, parent, false);
        }

        TextView tvTenTacgia = convertView.findViewById(R.id.tvTenTacgia);
        TextView tvEmail = convertView.findViewById(R.id.tvEmail);
        TextView tvDiaChi = convertView.findViewById(R.id.tvDiaChi);
        TextView tvDienThoai = convertView.findViewById(R.id.tvDienThoai);
//        Button btnViewOnMap = convertView.findViewById(R.id.btnViewOnMap);

        tvTenTacgia.setText(tacgia.getTenTacgia());
        tvEmail.setText(tacgia.getEmail());
        tvDiaChi.setText(tacgia.getDiaChi());
        tvDienThoai.setText(tacgia.getDienThoai());

//        btnViewOnMap.setOnClickListener(v -> {
//            Intent intent = new Intent(getContext(), MapActivity.class);
//            intent.putExtra("tacgia", tacgia);
//            getContext().startActivity(intent);
//        });

        convertView.setOnClickListener(v -> {
            ((ManageTacgiaActivity) getContext()).setSelectedTacgia(tacgia);
        });

        convertView.setOnLongClickListener(v -> {
            new AlertDialog.Builder(getContext())
                    .setTitle("Delete Tacgia")
                    .setMessage("Are you sure you want to delete this tác giả?")
                    .setPositiveButton("Yes", (dialog, which) -> {
                        deleteTacgia(tacgia);
                        Toast.makeText(getContext(), "Tác Giả deleted", Toast.LENGTH_SHORT).show();
                    })
                    .setNegativeButton("No", null)
                    .show();
            return true;
        });

        return convertView;
    }

    private void deleteTacgia(Tacgia tacgia) {
        SQLiteDatabase db = dbHelper.getWritableDatabase();
        db.delete("Tacgia", "idTacgia=?", new String[]{String.valueOf(tacgia.getIdTacgia())});
        remove(tacgia);
        notifyDataSetChanged();
    }
}

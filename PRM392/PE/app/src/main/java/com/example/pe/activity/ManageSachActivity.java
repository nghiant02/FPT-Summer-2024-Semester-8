package com.example.pe.activity;

import android.content.ContentValues;
import android.content.Intent;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.Spinner;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;

import com.example.pe.R;
import com.example.pe.adapter.SachAdapter;
import com.example.pe.helper.DatabaseHelper;
import com.example.pe.model.Sach;
import com.example.pe.model.Tacgia;

import java.util.ArrayList;
import java.util.List;
import java.util.regex.Pattern;

public class ManageSachActivity extends AppCompatActivity {
    private DatabaseHelper dbHelper;
    private EditText etTenSach, etNgayXb, etTheLoai;
    private Spinner spinnerTacgia;
    private Button btnAddUpdateSach;
    private ListView lvSachs;
    private SachAdapter sachAdapter;
    private List<Sach> sachList;
    private Sach selectedSach = null;
    private List<Tacgia> tacgiaList;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_manage_sach);

        dbHelper = new DatabaseHelper(this);
        etTenSach = findViewById(R.id.etTenSach);
        etNgayXb = findViewById(R.id.etNgayXb);
        etTheLoai = findViewById(R.id.etTheLoai);
        spinnerTacgia = findViewById(R.id.spinnerTacgia);
        btnAddUpdateSach = findViewById(R.id.btnAddUpdateSach);
        lvSachs = findViewById(R.id.lvSachs);

        sachList = new ArrayList<>();
        tacgiaList = new ArrayList<>();

        sachAdapter = new SachAdapter(this, sachList, dbHelper);
        lvSachs.setAdapter(sachAdapter);

        loadSachs();
        loadTacgias();

        ArrayAdapter<String> tacgiaAdapter = new ArrayAdapter<>(this, android.R.layout.simple_spinner_item, getTacgiaNames());
        tacgiaAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        spinnerTacgia.setAdapter(tacgiaAdapter);

        btnAddUpdateSach.setOnClickListener(view -> {
            if (selectedSach == null) {
                addSach();
            } else {
                updateSach();
            }
        });
    }

    private void loadSachs() {
        sachList.clear();
        SQLiteDatabase db = dbHelper.getReadableDatabase();
        Cursor cursor = db.query("Sach", null, null, null, null, null, null);

        if (cursor.moveToFirst()) {
            do {
                int idSach = cursor.getInt(cursor.getColumnIndexOrThrow("idSach"));
                String tenSach = cursor.getString(cursor.getColumnIndexOrThrow("tenSach"));
                String ngayXb = cursor.getString(cursor.getColumnIndexOrThrow("ngayXb"));
                String theLoai = cursor.getString(cursor.getColumnIndexOrThrow("theLoai"));
                int idTacgia = cursor.getInt(cursor.getColumnIndexOrThrow("idTacgia"));

                Sach sach = new Sach(idSach, tenSach, ngayXb, theLoai, idTacgia);
                sachList.add(sach);
            } while (cursor.moveToNext());
        }
        cursor.close();
        sachAdapter.notifyDataSetChanged();
    }

    private void loadTacgias() {
        tacgiaList.clear();
        SQLiteDatabase db = dbHelper.getReadableDatabase();
        Cursor cursor = db.query("Tacgia", null, null, null, null, null, null);

        if (cursor.moveToFirst()) {
            do {
                int idTacgia = cursor.getInt(cursor.getColumnIndexOrThrow("idTacgia"));
                String tenTacgia = cursor.getString(cursor.getColumnIndexOrThrow("tenTacgia"));
                String email = cursor.getString(cursor.getColumnIndexOrThrow("email"));
                String diaChi = cursor.getString(cursor.getColumnIndexOrThrow("diaChi"));
                String dienThoai = cursor.getString(cursor.getColumnIndexOrThrow("dienThoai"));

                Tacgia tacgia = new Tacgia(idTacgia, tenTacgia, email, diaChi, dienThoai);
                tacgiaList.add(tacgia);
            } while (cursor.moveToNext());
        }
        cursor.close();
    }

    private List<String> getTacgiaNames() {
        List<String> tacgiaNames = new ArrayList<>();
        for (Tacgia tacgia : tacgiaList) {
            tacgiaNames.add(tacgia.getTenTacgia());
        }
        return tacgiaNames;
    }

    private void addSach() {
        String tenSach = etTenSach.getText().toString().trim();
        String ngayXb = etNgayXb.getText().toString().trim();
        String theLoai = etTheLoai.getText().toString().trim();
        int idTacgia = tacgiaList.get(spinnerTacgia.getSelectedItemPosition()).getIdTacgia();

        if (!validateInputs(tenSach, ngayXb, theLoai)) {
            return;
        }

        SQLiteDatabase db = dbHelper.getWritableDatabase();
        ContentValues values = new ContentValues();
        values.put("tenSach", tenSach);
        values.put("ngayXb", ngayXb);
        values.put("theLoai", theLoai);
        values.put("idTacgia", idTacgia);

        long result = db.insert("Sach", null, values);
        if (result != -1) {
            Toast.makeText(this, "Sách added successfully", Toast.LENGTH_SHORT).show();
            loadSachs();
            etTenSach.setText("");
            etNgayXb.setText("");
            etTheLoai.setText("");
            spinnerTacgia.setSelection(0);
        } else {
            Toast.makeText(this, "Failed to add sách", Toast.LENGTH_SHORT).show();
        }
    }

    private void updateSach() {
        String tenSach = etTenSach.getText().toString().trim();
        String ngayXb = etNgayXb.getText().toString().trim();
        String theLoai = etTheLoai.getText().toString().trim();
        int idTacgia = tacgiaList.get(spinnerTacgia.getSelectedItemPosition()).getIdTacgia();

        if (!validateInputs(tenSach, ngayXb, theLoai)) {
            return;
        }

        SQLiteDatabase db = dbHelper.getWritableDatabase();
        ContentValues values = new ContentValues();
        values.put("tenSach", tenSach);
        values.put("ngayXb", ngayXb);
        values.put("theLoai", theLoai);
        values.put("idTacgia", idTacgia);

        int result = db.update("Sach", values, "idSach=?", new String[]{String.valueOf(selectedSach.getIdSach())});
        if (result > 0) {
            Toast.makeText(this, "Sách updated successfully", Toast.LENGTH_SHORT).show();
            loadSachs();
            etTenSach.setText("");
            etNgayXb.setText("");
            etTheLoai.setText("");
            spinnerTacgia.setSelection(0);
            selectedSach = null;
            btnAddUpdateSach.setText("Add Sách");
        } else {
            Toast.makeText(this, "Failed to update sách", Toast.LENGTH_SHORT).show();
        }
    }

    private boolean validateInputs(String tenSach, String ngayXb, String theLoai) {
        if (tenSach.isEmpty()) {
            Toast.makeText(this, "Tên sách không được để trống", Toast.LENGTH_SHORT).show();
            return false;
        }

        if (ngayXb.isEmpty() || !Pattern.matches("\\d{2}/\\d{2}/\\d{4}", ngayXb)) {
            Toast.makeText(this, "Ngày xuất bản không hợp lệ", Toast.LENGTH_SHORT).show();
            return false;
        }

        if (theLoai.isEmpty()) {
            Toast.makeText(this, "Thể loại không được để trống", Toast.LENGTH_SHORT).show();
            return false;
        }

        return true;
    }

    private void deleteSach(Sach sach) {
        SQLiteDatabase db = dbHelper.getWritableDatabase();
        db.delete("Sach", "idSach=?", new String[]{String.valueOf(sach.getIdSach())});
        sachList.remove(sach);
        sachAdapter.notifyDataSetChanged();
    }

    public void setSelectedSach(Sach sach) {
        selectedSach = sach;
        etTenSach.setText(sach.getTenSach());
        etNgayXb.setText(sach.getNgayXb());
        etTheLoai.setText(sach.getTheLoai());
        spinnerTacgia.setSelection(getTacgiaPosition(sach.getIdTacgia()));
        btnAddUpdateSach.setText("Update Sách");
    }

    private int getTacgiaPosition(int idTacgia) {
        for (int i = 0; i < tacgiaList.size(); i++) {
            if (tacgiaList.get(i).getIdTacgia() == idTacgia) {
                return i;
            }
        }
        return 0;
    }
}

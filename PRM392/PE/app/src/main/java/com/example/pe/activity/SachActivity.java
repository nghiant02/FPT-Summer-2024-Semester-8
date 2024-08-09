package com.example.pe.activity;

import android.app.DatePickerDialog;
import android.content.ContentValues;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;

import com.example.pe.R;
import com.example.pe.helper.DatabaseHelper;
import com.example.pe.model.Sach;
import com.example.pe.model.Tacgia;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.List;

public class SachActivity extends AppCompatActivity {
    private DatabaseHelper dbHelper;
    private EditText etTenSach, etNgayXb, etTheLoai;
    private Spinner spinnerTacgia;
    private Button btnAddSach;
    private List<Tacgia> tacgiaList;
    private ArrayAdapter<String> tacgiaAdapter;
    private Sach sach;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_sach);

        dbHelper = new DatabaseHelper(this);
        etTenSach = findViewById(R.id.etTenSach);
        etNgayXb = findViewById(R.id.etNgayXb);
        etTheLoai = findViewById(R.id.etTheLoai);
        spinnerTacgia = findViewById(R.id.spinnerTacgia);
        btnAddSach = findViewById(R.id.btnAddSach);

        etNgayXb.setOnClickListener(view -> showDatePickerDialog());

        tacgiaList = new ArrayList<>();
        loadTacgias();

        tacgiaAdapter = new ArrayAdapter<>(this, android.R.layout.simple_spinner_item, getTacgiaNames());
        tacgiaAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        spinnerTacgia.setAdapter(tacgiaAdapter);

        sach = (Sach) getIntent().getSerializableExtra("sach");

        if (sach != null) {
            etTenSach.setText(sach.getTenSach());
            etNgayXb.setText(sach.getNgayXb());
            etTheLoai.setText(sach.getTheLoai());
            spinnerTacgia.setSelection(getTacgiaPosition(sach.getIdTacgia()));
            btnAddSach.setText("Cập Nhật Sách");
        }

        btnAddSach.setOnClickListener(view -> {
            if (sach == null) {
                addSach();
            } else {
                updateSach();
            }
        });
    }

    private void showDatePickerDialog() {
        final Calendar calendar = Calendar.getInstance();
        int year = calendar.get(Calendar.YEAR);
        int month = calendar.get(Calendar.MONTH);
        int day = calendar.get(Calendar.DAY_OF_MONTH);

        DatePickerDialog datePickerDialog = new DatePickerDialog(this,
                (view, selectedYear, selectedMonth, selectedDay) -> {
                    String date = selectedDay + "/" + (selectedMonth + 1) + "/" + selectedYear;
                    etNgayXb.setText(date);
                }, year, month, day);
        datePickerDialog.show();
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

    private int getTacgiaPosition(int idTacgia) {
        for (int i = 0; i < tacgiaList.size(); i++) {
            if (tacgiaList.get(i).getIdTacgia() == idTacgia) {
                return i;
            }
        }
        return 0;
    }

    private void addSach() {
        String tenSach = etTenSach.getText().toString().trim();
        String ngayXb = etNgayXb.getText().toString().trim();
        String theLoai = etTheLoai.getText().toString().trim();
        int idTacgia = tacgiaList.get(spinnerTacgia.getSelectedItemPosition()).getIdTacgia();

        if (tenSach.isEmpty() || ngayXb.isEmpty() || theLoai.isEmpty()) {
            Toast.makeText(this, "Please fill in all fields", Toast.LENGTH_SHORT).show();
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
            setResult(RESULT_OK);
            finish();
        } else {
            Toast.makeText(this, "Failed to add sách", Toast.LENGTH_SHORT).show();
        }
    }

    private void updateSach() {
        String tenSach = etTenSach.getText().toString().trim();
        String ngayXb = etNgayXb.getText().toString().trim();
        String theLoai = etTheLoai.getText().toString().trim();
        int idTacgia = tacgiaList.get(spinnerTacgia.getSelectedItemPosition()).getIdTacgia();

        if (tenSach.isEmpty() || ngayXb.isEmpty() || theLoai.isEmpty()) {
            Toast.makeText(this, "Please fill in all fields", Toast.LENGTH_SHORT).show();
            return;
        }

        SQLiteDatabase db = dbHelper.getWritableDatabase();
        ContentValues values = new ContentValues();
        values.put("tenSach", tenSach);
        values.put("ngayXb", ngayXb);
        values.put("theLoai", theLoai);
        values.put("idTacgia", idTacgia);

        int result = db.update("Sach", values, "idSach=?", new String[]{String.valueOf(sach.getIdSach())});
        if (result > 0) {
            Toast.makeText(this, "Sách updated successfully", Toast.LENGTH_SHORT).show();
            setResult(RESULT_OK);
            finish();
        } else {
            Toast.makeText(this, "Failed to update sách", Toast.LENGTH_SHORT).show();
        }
    }
}

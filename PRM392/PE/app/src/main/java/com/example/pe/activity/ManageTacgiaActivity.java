package com.example.pe.activity;

import android.content.ContentValues;
import android.content.Intent;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;

import com.example.pe.R;
import com.example.pe.adapter.TacgiaAdapter;
import com.example.pe.helper.DatabaseHelper;
import com.example.pe.model.Tacgia;

import java.util.ArrayList;
import java.util.List;
import java.util.regex.Pattern;

public class ManageTacgiaActivity extends AppCompatActivity {
    private DatabaseHelper dbHelper;
    private EditText etTenTacgia, etEmail, etDiaChi, etDienThoai;
    private Button btnAddUpdateTacgia;
    private ListView lvTacgias;
    private TacgiaAdapter tacgiaAdapter;
    private List<Tacgia> tacgiaList;
    private Tacgia selectedTacgia = null;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_manage_tacgia);

        dbHelper = new DatabaseHelper(this);
        etTenTacgia = findViewById(R.id.etTenTacgia);
        etEmail = findViewById(R.id.etEmail);
        etDiaChi = findViewById(R.id.etDiaChi);
        etDienThoai = findViewById(R.id.etDienThoai);
        btnAddUpdateTacgia = findViewById(R.id.btnAddUpdateTacgia);
        lvTacgias = findViewById(R.id.lvTacgias);

        tacgiaList = new ArrayList<>();
        tacgiaAdapter = new TacgiaAdapter(this, tacgiaList, dbHelper);
        lvTacgias.setAdapter(tacgiaAdapter);

        loadTacgias();

        btnAddUpdateTacgia.setOnClickListener(view -> {
            if (selectedTacgia == null) {
                addTacgia();
            } else {
                updateTacgia();
            }
        });
    }

    private void addTacgia() {
        String tenTacgia = etTenTacgia.getText().toString().trim();
        String email = etEmail.getText().toString().trim();
        String diaChi = etDiaChi.getText().toString().trim();
        String dienThoai = etDienThoai.getText().toString().trim();

        if (!validateInputs(tenTacgia, email, diaChi, dienThoai)) {
            return;
        }

        SQLiteDatabase db = dbHelper.getWritableDatabase();
        ContentValues values = new ContentValues();
        values.put("tenTacgia", tenTacgia);
        values.put("email", email);
        values.put("diaChi", diaChi);
        values.put("dienThoai", dienThoai);

        long result = db.insert("Tacgia", null, values);
        if (result != -1) {
            Toast.makeText(this, "Tác Giả added successfully", Toast.LENGTH_SHORT).show();
            loadTacgias();
            etTenTacgia.setText("");
            etEmail.setText("");
            etDiaChi.setText("");
            etDienThoai.setText("");
        } else {
            Toast.makeText(this, "Failed to add tác giả", Toast.LENGTH_SHORT).show();
        }
    }

    private void updateTacgia() {
        String tenTacgia = etTenTacgia.getText().toString().trim();
        String email = etEmail.getText().toString().trim();
        String diaChi = etDiaChi.getText().toString().trim();
        String dienThoai = etDienThoai.getText().toString().trim();

        if (!validateInputs(tenTacgia, email, diaChi, dienThoai)) {
            return;
        }

        SQLiteDatabase db = dbHelper.getWritableDatabase();
        ContentValues values = new ContentValues();
        values.put("tenTacgia", tenTacgia);
        values.put("email", email);
        values.put("diaChi", diaChi);
        values.put("dienThoai", dienThoai);

        int result = db.update("Tacgia", values, "idTacgia=?", new String[]{String.valueOf(selectedTacgia.getIdTacgia())});
        if (result > 0) {
            Toast.makeText(this, "Tác Giả updated successfully", Toast.LENGTH_SHORT).show();
            loadTacgias();
            etTenTacgia.setText("");
            etEmail.setText("");
            etDiaChi.setText("");
            etDienThoai.setText("");
            selectedTacgia = null;
            btnAddUpdateTacgia.setText("Add Tác Giả");
        } else {
            Toast.makeText(this, "Failed to update tác giả", Toast.LENGTH_SHORT).show();
        }
    }

    private boolean validateInputs(String tenTacgia, String email, String diaChi, String dienThoai) {
        if (tenTacgia.isEmpty()) {
            Toast.makeText(this, "Tên tác giả không được để trống", Toast.LENGTH_SHORT).show();
            return false;
        }

        if (email.isEmpty() || !Pattern.matches("^[A-Za-z0-9+_.-]+@(.+)$", email)) {
            Toast.makeText(this, "Email không hợp lệ", Toast.LENGTH_SHORT).show();
            return false;
        }

        if (diaChi.isEmpty()) {
            Toast.makeText(this, "Địa chỉ không được để trống", Toast.LENGTH_SHORT).show();
            return false;
        }

        if (dienThoai.isEmpty() || !Pattern.matches("\\d{10,11}", dienThoai)) {
            Toast.makeText(this, "Điện thoại không hợp lệ", Toast.LENGTH_SHORT).show();
            return false;
        }

        return true;
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
        tacgiaAdapter.notifyDataSetChanged();
    }

    private void deleteTacgia(Tacgia tacgia) {
        SQLiteDatabase db = dbHelper.getWritableDatabase();
        db.delete("Tacgia", "idTacgia=?", new String[]{String.valueOf(tacgia.getIdTacgia())});
        tacgiaList.remove(tacgia);
        tacgiaAdapter.notifyDataSetChanged();
    }

    public void setSelectedTacgia(Tacgia tacgia) {
        selectedTacgia = tacgia;
        etTenTacgia.setText(tacgia.getTenTacgia());
        etEmail.setText(tacgia.getEmail());
        etDiaChi.setText(tacgia.getDiaChi());
        etDienThoai.setText(tacgia.getDienThoai());
        btnAddUpdateTacgia.setText("Update Tác Giả");
    }

    private void viewOnMap(Tacgia tacgia) {
        Intent intent = new Intent(this, MapActivity.class);
        intent.putExtra("tacgia", tacgia);
        startActivity(intent);
    }

    public void onItemClick(Tacgia tacgia) {
        setSelectedTacgia(tacgia);
        viewOnMap(tacgia);
    }

    public void onItemLongClick(Tacgia tacgia) {
        deleteTacgia(tacgia);
    }
}

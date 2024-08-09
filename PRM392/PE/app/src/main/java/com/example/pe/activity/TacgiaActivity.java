package com.example.pe.activity;

import android.content.ContentValues;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

import androidx.appcompat.app.AppCompatActivity;

import com.example.pe.R;
import com.example.pe.helper.DatabaseHelper;
import com.example.pe.model.Tacgia;

import java.util.regex.Pattern;

public class TacgiaActivity extends AppCompatActivity {
    private DatabaseHelper dbHelper;
    private EditText etTenTacgia, etEmail, etDiaChi, etDienThoai;
    private Button btnAddTacgia;
    private Tacgia tacgia;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_tacgia);

        dbHelper = new DatabaseHelper(this);
        etTenTacgia = findViewById(R.id.etTenTacgia);
        etEmail = findViewById(R.id.etEmail);
        etDiaChi = findViewById(R.id.etDiaChi);
        etDienThoai = findViewById(R.id.etDienThoai);
        btnAddTacgia = findViewById(R.id.btnAddTacgia);

        tacgia = (Tacgia) getIntent().getSerializableExtra("tacgia");

        if (tacgia != null) {
            etTenTacgia.setText(tacgia.getTenTacgia());
            etEmail.setText(tacgia.getEmail());
            etDiaChi.setText(tacgia.getDiaChi());
            etDienThoai.setText(tacgia.getDienThoai());
            btnAddTacgia.setText("Cập Nhật Tác Giả");
        }

        btnAddTacgia.setOnClickListener(view -> {
            if (tacgia == null) {
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
            setResult(RESULT_OK);
            finish();
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

        int result = db.update("Tacgia", values, "idTacgia=?", new String[]{String.valueOf(tacgia.getIdTacgia())});
        if (result > 0) {
            Toast.makeText(this, "Tác Giả updated successfully", Toast.LENGTH_SHORT).show();
            setResult(RESULT_OK);
            finish();
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
}

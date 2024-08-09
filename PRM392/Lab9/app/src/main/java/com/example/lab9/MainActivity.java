package com.example.lab9;

import android.content.ContentValues;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.MenuInflater;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.Toast;

import androidx.appcompat.app.AlertDialog;
import androidx.appcompat.app.AppCompatActivity;
import androidx.appcompat.widget.Toolbar;

import java.util.ArrayList;

public class MainActivity extends AppCompatActivity {

    Database database;
    ListView lvCongViec;
    ArrayList<CongViec> congviecList;
    CongViecAdapter adapter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        Toolbar toolbar = findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);

        database = new Database(this);
        lvCongViec = findViewById(R.id.lvCongViec);
        congviecList = new ArrayList<>();
        adapter = new CongViecAdapter(this, R.layout.dong_cong_viec, congviecList);
        lvCongViec.setAdapter(adapter);

        GetDataCongViec();

        lvCongViec.setOnItemLongClickListener(new AdapterView.OnItemLongClickListener() {
            @Override
            public boolean onItemLongClick(AdapterView<?> parent, View view, int position, long id) {
                CongViec congviec = congviecList.get(position);
                ShowDialogDelete(congviec.getId(), congviec.getTenCV());
                return true;
            }
        });

        lvCongViec.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                CongViec congviec = congviecList.get(position);
                ShowDialogUpdate(congviec.getId(), congviec.getTenCV(), congviec.getNoiDung());
            }
        });
    }

    private void GetDataCongViec() {
        SQLiteDatabase db = database.getReadableDatabase();
        Cursor cursor = db.rawQuery("SELECT * FROM CongViec", null);
        congviecList.clear();
        while (cursor.moveToNext()) {
            int id = cursor.getInt(0);
            String tenCV = cursor.getString(1);
            String noiDung = cursor.getString(2);
            congviecList.add(new CongViec(id, tenCV, noiDung));
        }
        cursor.close();
        adapter.notifyDataSetChanged();
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        MenuInflater inflater = getMenuInflater();
        inflater.inflate(R.menu.add_congviec, menu);
        return super.onCreateOptionsMenu(menu);
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        if (item.getItemId() == R.id.menuAdd) {
            ShowDialogAdd();
            return true;
        }
        return super.onOptionsItemSelected(item);
    }

    private void ShowDialogAdd() {
        AlertDialog.Builder dialogBuilder = new AlertDialog.Builder(this);
        LayoutInflater inflater = this.getLayoutInflater();
        View dialogView = inflater.inflate(R.layout.dialog_them_cong_viec, null);
        dialogBuilder.setView(dialogView);

        EditText edtTenCV = dialogView.findViewById(R.id.edtTenCV);
        EditText edtNoiDung = dialogView.findViewById(R.id.edtNoiDung);
        Button btnThem = dialogView.findViewById(R.id.btnThem);
        Button btnHuy = dialogView.findViewById(R.id.btnHuy);

        AlertDialog alertDialog = dialogBuilder.create();
        alertDialog.show();

        btnThem.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String tenCV = edtTenCV.getText().toString().trim();
                String noiDung = edtNoiDung.getText().toString().trim();
                if (!tenCV.isEmpty() && !noiDung.isEmpty()) {
                    SQLiteDatabase db = database.getWritableDatabase();
                    ContentValues values = new ContentValues();
                    values.put("TenCV", tenCV);
                    values.put("NoiDung", noiDung);
                    long result = db.insert("CongViec", null, values);
                    GetDataCongViec();
                    alertDialog.dismiss();
                } else {
                    Toast.makeText(MainActivity.this, "Please enter both fields", Toast.LENGTH_SHORT).show();
                }
            }
        });

        btnHuy.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                alertDialog.dismiss();
            }
        });
    }

    private void ShowDialogUpdate(int id, String tenCV, String noiDung) {
        AlertDialog.Builder dialogBuilder = new AlertDialog.Builder(this);
        LayoutInflater inflater = this.getLayoutInflater();
        View dialogView = inflater.inflate(R.layout.dialog_sua_cong_viec, null);
        dialogBuilder.setView(dialogView);

        EditText edtTenCV = dialogView.findViewById(R.id.edtTenCV);
        EditText edtNoiDung = dialogView.findViewById(R.id.edtNoiDung);
        Button btnSua = dialogView.findViewById(R.id.btnSua);
        Button btnHuy = dialogView.findViewById(R.id.btnHuy);

        edtTenCV.setText(tenCV);
        edtNoiDung.setText(noiDung);

        AlertDialog alertDialog = dialogBuilder.create();
        alertDialog.show();

        btnSua.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String newTenCV = edtTenCV.getText().toString().trim();
                String newNoiDung = edtNoiDung.getText().toString().trim();

                if (!newTenCV.isEmpty() && !newNoiDung.isEmpty()) {
                    SQLiteDatabase db = database.getWritableDatabase();
                    ContentValues values = new ContentValues();
                    values.put("TenCV", newTenCV);
                    values.put("NoiDung", newNoiDung);
                    db.update("CongViec", values, "id=?", new String[]{String.valueOf(id)});
                    GetDataCongViec();
                    alertDialog.dismiss();
                } else {
                    Toast.makeText(MainActivity.this, "Please enter both fields", Toast.LENGTH_SHORT).show();
                }
            }
        });

        btnHuy.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                alertDialog.dismiss();
            }
        });
    }

    private void ShowDialogDelete(int id, String tenCV) {
        AlertDialog.Builder dialogBuilder = new AlertDialog.Builder(this);
        dialogBuilder.setTitle("Xóa công việc");
        dialogBuilder.setMessage("Bạn có chắc chắn muốn xóa công việc " + tenCV + " không?");
        dialogBuilder.setPositiveButton("Có", (dialog, which) -> {
            SQLiteDatabase db = database.getWritableDatabase();
            db.delete("CongViec", "id=?", new String[]{String.valueOf(id)});
            GetDataCongViec();
            dialog.dismiss();
        });
        dialogBuilder.setNegativeButton("Không", (dialog, which) -> dialog.dismiss());
        AlertDialog alertDialog = dialogBuilder.create();
        alertDialog.show();
    }
}

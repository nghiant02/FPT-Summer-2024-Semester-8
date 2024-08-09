package com.example.customlistview;

import android.os.Bundle;
import android.widget.ListView;

import androidx.appcompat.app.AppCompatActivity;

import java.util.ArrayList;

public class MainActivity extends AppCompatActivity {
    ListView lvTraiCay;
    ArrayList<TraiCay> arrayTraiCay;
    TraiCayAdapter adapter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        AnhXa();
        adapter = new TraiCayAdapter(this, R.layout.dong_trai_cay, arrayTraiCay);
        lvTraiCay.setAdapter(adapter);
    }

    private void AnhXa() {
        lvTraiCay = findViewById(R.id.listviewTraiCay);
        arrayTraiCay = new ArrayList<>();
        arrayTraiCay.add(new TraiCay("Chuối tiêu", "Chuối tiêu Long An", R.drawable.chuoi_tieu));
        arrayTraiCay.add(new TraiCay("Thanh Long", "Thanh long ruột đỏ", R.drawable.thanh_long));
        arrayTraiCay.add(new TraiCay("Dâu tây", "Dâu tây Đà Lạt", R.drawable.dau_tay));
        arrayTraiCay.add(new TraiCay("Dưa hấu", "Dưa hấu tiền giang", R.drawable.dua_hau));
        arrayTraiCay.add(new TraiCay("Cam vàng", "Cam vàng nhập khẩu", R.drawable.cam_vang));
    }
}

package com.example.pe.helper;

import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

public class DatabaseHelper extends SQLiteOpenHelper {
    private static final String DATABASE_NAME = "library.db";
    private static final int DATABASE_VERSION = 1;

    public DatabaseHelper(Context context) {
        super(context, DATABASE_NAME, null, DATABASE_VERSION);
    }

    @Override
    public void onCreate(SQLiteDatabase db) {
        String createTacgiaTable = "CREATE TABLE Tacgia (idTacgia INTEGER PRIMARY KEY AUTOINCREMENT, " +
                "tenTacgia TEXT, email TEXT, diaChi TEXT, dienThoai TEXT)";
        db.execSQL(createTacgiaTable);

        String createSachTable = "CREATE TABLE Sach (idSach INTEGER PRIMARY KEY AUTOINCREMENT, " +
                "tenSach TEXT, ngayXb TEXT, theLoai TEXT, idTacgia INTEGER)";
        db.execSQL(createSachTable);
    }

    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        db.execSQL("DROP TABLE IF EXISTS Tacgia");
        db.execSQL("DROP TABLE IF EXISTS Sach");
        onCreate(db);
    }
}

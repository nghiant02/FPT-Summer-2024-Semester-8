package com.example.lab9;

public class CongViec {
    private int id;
    private String tenCV;
    private String noiDung;

    public CongViec(int id, String tenCV, String noiDung) {
        this.id = id;
        this.tenCV = tenCV;
        this.noiDung = noiDung;
    }

    public int getId() {
        return id;
    }

    public String getTenCV() {
        return tenCV;
    }

    public String getNoiDung() {
        return noiDung;
    }
}

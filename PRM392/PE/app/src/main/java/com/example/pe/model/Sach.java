package com.example.pe.model;

import java.io.Serializable;

public class Sach implements Serializable {
    private int idSach;
    private String tenSach;
    private String ngayXb;
    private String theLoai;
    private int idTacgia;

    public Sach() {
    }

    public Sach(int idSach, String tenSach, String ngayXb, String theLoai, int idTacgia) {
        this.idSach = idSach;
        this.tenSach = tenSach;
        this.ngayXb = ngayXb;
        this.theLoai = theLoai;
        this.idTacgia = idTacgia;
    }

    public int getIdSach() {
        return idSach;
    }

    public void setIdSach(int idSach) {
        this.idSach = idSach;
    }

    public String getTenSach() {
        return tenSach;
    }

    public void setTenSach(String tenSach) {
        this.tenSach = tenSach;
    }

    public String getNgayXb() {
        return ngayXb;
    }

    public void setNgayXb(String ngayXb) {
        this.ngayXb = ngayXb;
    }

    public String getTheLoai() {
        return theLoai;
    }

    public void setTheLoai(String theLoai) {
        this.theLoai = theLoai;
    }

    public int getIdTacgia() {
        return idTacgia;
    }

    public void setIdTacgia(int idTacgia) {
        this.idTacgia = idTacgia;
    }
}

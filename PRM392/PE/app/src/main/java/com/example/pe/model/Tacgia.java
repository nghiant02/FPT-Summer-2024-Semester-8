package com.example.pe.model;

import java.io.Serializable;

public class Tacgia implements Serializable {
    private int idTacgia;
    private String tenTacgia;
    private String email;
    private String diaChi;
    private String dienThoai;

    public Tacgia() {
    }

    public Tacgia(int idTacgia, String tenTacgia, String email, String diaChi, String dienThoai) {
        this.idTacgia = idTacgia;
        this.tenTacgia = tenTacgia;
        this.email = email;
        this.diaChi = diaChi;
        this.dienThoai = dienThoai;
    }

    public int getIdTacgia() {
        return idTacgia;
    }

    public void setIdTacgia(int idTacgia) {
        this.idTacgia = idTacgia;
    }

    public String getTenTacgia() {
        return tenTacgia;
    }

    public void setTenTacgia(String tenTacgia) {
        this.tenTacgia = tenTacgia;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getDiaChi() {
        return diaChi;
    }

    public void setDiaChi(String diaChi) {
        this.diaChi = diaChi;
    }

    public String getDienThoai() {
        return dienThoai;
    }

    public void setDienThoai(String dienThoai) {
        this.dienThoai = dienThoai;
    }
}

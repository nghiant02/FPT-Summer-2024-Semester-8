package com.example.lab5_1;

public class Module {
    private String title;
    private String description;
    private String platform;
    private int imageResId;

    public Module(String title, String description, String platform, int imageResId) {
        this.title = title;
        this.description = description;
        this.platform = platform;
        this.imageResId = imageResId;
    }

    public String getTitle() {
        return title;
    }

    public String getDescription() {
        return description;
    }

    public String getPlatform() {
        return platform;
    }

    public int getImageResId() {
        return imageResId;
    }
}

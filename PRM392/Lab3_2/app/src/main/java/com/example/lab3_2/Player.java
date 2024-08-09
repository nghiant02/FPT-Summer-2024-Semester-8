package com.example.lab3_2;

public class Player {
    private String name;
    private String details;
    private int imageResourceId;
    private int flagResourceId;

    public Player(String name, String details, int imageResourceId, int flagResourceId) {
        this.name = name;
        this.details = details;
        this.imageResourceId = imageResourceId;
        this.flagResourceId = flagResourceId;
    }

    public String getName() {
        return name;
    }

    public String getDetails() {
        return details;
    }

    public int getImageResourceId() {
        return imageResourceId;
    }

    public int getFlagResourceId() {
        return flagResourceId;
    }
}

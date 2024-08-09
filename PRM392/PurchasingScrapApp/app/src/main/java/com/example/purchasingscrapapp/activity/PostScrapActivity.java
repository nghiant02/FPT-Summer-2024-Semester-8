package com.example.purchasingscrapapp.activity;

import android.content.Intent;
import android.graphics.Bitmap;
import android.net.Uri;
import android.os.Bundle;
import android.provider.MediaStore;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.ProgressBar;
import android.widget.Spinner;
import android.widget.Toast;
import androidx.annotation.Nullable;
import androidx.appcompat.app.AppCompatActivity;
import androidx.lifecycle.ViewModelProvider;
import com.example.purchasingscrapapp.R;
import com.example.purchasingscrapapp.model.Scrap;
import com.example.purchasingscrapapp.model.ScrapCategory;
import com.example.purchasingscrapapp.viewmodel.ScrapViewModel;
import com.google.firebase.Timestamp;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.storage.FirebaseStorage;
import com.google.firebase.storage.StorageReference;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

public class PostScrapActivity extends AppCompatActivity {

    private static final int PICK_IMAGE_REQUEST = 1;
    private static final int REQUEST_IMAGE_CAPTURE = 2;

    private ScrapViewModel scrapViewModel;
    private EditText editTextName, editTextDescription, editTextLocation;
    private Spinner spinnerCategory;
    private Button buttonPostScrap, buttonSelectImage;
    private ImageView imageViewScrap;
    private ProgressBar progressBar;

    private Uri imageUri;
    private String imageUrl;
    private List<ScrapCategory> scrapCategories = new ArrayList<>();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_post_scrap);

        initViews();

        scrapViewModel = new ViewModelProvider(this).get(ScrapViewModel.class);

        scrapViewModel.getScrapCategories().observe(this, categories -> {
            scrapCategories = categories;
            if (categories == null || categories.isEmpty()) {
                Toast.makeText(this, "No categories available. Please check your database.", Toast.LENGTH_SHORT).show();
                return;
            }
            List<String> categoryNames = new ArrayList<>();
            for (ScrapCategory category : categories) {
                categoryNames.add(category.getName());
            }
            ArrayAdapter<String> adapter = new ArrayAdapter<>(this, android.R.layout.simple_spinner_item, categoryNames);
            adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
            spinnerCategory.setAdapter(adapter);
        });

        buttonSelectImage.setOnClickListener(v -> openImageOptions());

        buttonPostScrap.setOnClickListener(v -> {
            String name = editTextName.getText().toString();
            String description = editTextDescription.getText().toString();
            String location = editTextLocation.getText().toString();
            String categoryName = spinnerCategory.getSelectedItem() != null ? spinnerCategory.getSelectedItem().toString() : null;
            String categoryId = getCategoryIdByName(categoryName);

            if (name.isEmpty() || description.isEmpty() || location.isEmpty() || categoryId == null) {
                Toast.makeText(this, "Please fill all fields and select a category", Toast.LENGTH_SHORT).show();
                return;
            }

            progressBar.setVisibility(View.VISIBLE);

            if (imageUri != null) {
                uploadImageToFirebase(name, description, location, categoryId);
            } else {
                postScrap(name, description, location, categoryId, null);
            }
        });
    }

    private void initViews() {
        editTextName = findViewById(R.id.edit_text_scrap_name);
        editTextDescription = findViewById(R.id.edit_text_scrap_description);
        editTextLocation = findViewById(R.id.edit_text_scrap_location);
        spinnerCategory = findViewById(R.id.spinner_scrap_category);
        imageViewScrap = findViewById(R.id.image_view_scrap);
        buttonSelectImage = findViewById(R.id.button_select_image);
        buttonPostScrap = findViewById(R.id.button_post_scrap);
        progressBar = findViewById(R.id.progress_bar);
    }

    private void openImageOptions() {
        Intent intent = new Intent();
        intent.setType("image/*");
        intent.setAction(Intent.ACTION_GET_CONTENT);
        startActivityForResult(Intent.createChooser(intent, "Select Picture"), PICK_IMAGE_REQUEST);
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, @Nullable Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        if (requestCode == PICK_IMAGE_REQUEST && resultCode == RESULT_OK && data != null && data.getData() != null) {
            imageUri = data.getData();
            try {
                Bitmap bitmap = MediaStore.Images.Media.getBitmap(getContentResolver(), imageUri);
                imageViewScrap.setImageBitmap(bitmap);
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
    }

    private void uploadImageToFirebase(String name, String description, String location, String categoryId) {
        StorageReference storageReference = FirebaseStorage.getInstance().getReference("scrap_images/" + UUID.randomUUID().toString());

        storageReference.putFile(imageUri)
                .addOnSuccessListener(taskSnapshot -> storageReference.getDownloadUrl().addOnSuccessListener(uri -> {
                    imageUrl = uri.toString();
                    postScrap(name, description, location, categoryId, imageUrl);
                }))
                .addOnFailureListener(e -> {
                    progressBar.setVisibility(View.GONE);
                    Toast.makeText(PostScrapActivity.this, "Image upload failed", Toast.LENGTH_SHORT).show();
                });
    }

    private void postScrap(String name, String description, String location, String categoryId, String imageUrl) {
        String userId = FirebaseAuth.getInstance().getCurrentUser().getUid();
        String scrapId = UUID.randomUUID().toString();
        Scrap scrap = new Scrap(scrapId, userId, categoryId, name, description, imageUrl, location, "pending", Timestamp.now(), Timestamp.now());

        scrapViewModel.postScrap(scrap).observe(this, success -> {
            progressBar.setVisibility(View.GONE);
            if (success) {
                Toast.makeText(PostScrapActivity.this, "Scrap posted successfully", Toast.LENGTH_SHORT).show();
                finish();
            } else {
                Toast.makeText(PostScrapActivity.this, "Failed to post scrap", Toast.LENGTH_SHORT).show();
            }
        });
    }

    private String getCategoryIdByName(String categoryName) {
        for (ScrapCategory category : scrapCategories) {
            if (category.getName().equals(categoryName)) {
                return category.getId();
            }
        }
        return null;
    }
}

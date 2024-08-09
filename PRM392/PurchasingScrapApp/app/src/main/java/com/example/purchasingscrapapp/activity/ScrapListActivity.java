package com.example.purchasingscrapapp.activity;

import android.content.Intent;
import android.os.Bundle;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.View;
import android.widget.EditText;
import android.widget.ProgressBar;

import androidx.appcompat.app.AppCompatActivity;
import androidx.lifecycle.ViewModelProvider;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.purchasingscrapapp.R;
import com.example.purchasingscrapapp.adapter.ScrapAdapter;
import com.example.purchasingscrapapp.model.Scrap;
import com.example.purchasingscrapapp.viewmodel.ScrapViewModel;
import com.google.firebase.auth.FirebaseAuth;

import java.util.ArrayList;
import java.util.List;

public class ScrapListActivity extends AppCompatActivity implements ScrapAdapter.OnScrapClickListener {

    private ScrapViewModel scrapViewModel;
    private ScrapAdapter scrapAdapter;
    private RecyclerView recyclerViewScrapList;
    private EditText editTextSearch;
    private ProgressBar progressBar;
    private List<Scrap> scrapList = new ArrayList<>();
    private String currentUserId;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_scrap_list);

        initViews();

        scrapAdapter = new ScrapAdapter(this);
        recyclerViewScrapList.setLayoutManager(new LinearLayoutManager(this));
        recyclerViewScrapList.setHasFixedSize(true);
        recyclerViewScrapList.setAdapter(scrapAdapter);

        scrapViewModel = new ViewModelProvider(this).get(ScrapViewModel.class);
        currentUserId = FirebaseAuth.getInstance().getCurrentUser().getUid();

        loadScrapPosts();

        editTextSearch.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence s, int start, int count, int after) {}

            @Override
            public void onTextChanged(CharSequence s, int start, int before, int count) {
                filterScraps(s.toString());
            }

            @Override
            public void afterTextChanged(Editable s) {}
        });
    }

    private void initViews() {
        editTextSearch = findViewById(R.id.edit_text_search);
        recyclerViewScrapList = findViewById(R.id.recycler_view_scrap_list);
        progressBar = findViewById(R.id.progress_bar);
    }

    private void loadScrapPosts() {
        progressBar.setVisibility(View.VISIBLE);
        recyclerViewScrapList.setVisibility(View.GONE);

        scrapViewModel.getScrapsByUser(currentUserId).observe(this, scraps -> {
            scrapList = scraps;
            scrapAdapter.setScraps(scraps);
            progressBar.setVisibility(View.GONE);
            recyclerViewScrapList.setVisibility(View.VISIBLE);
        });
    }

    private void filterScraps(String query) {
        List<Scrap> filteredList = new ArrayList<>();
        for (Scrap scrap : scrapList) {
            if (scrap.getName().toLowerCase().contains(query.toLowerCase())) {
                filteredList.add(scrap);
            }
        }
        scrapAdapter.setScraps(filteredList);
    }

    @Override
    public void onScrapClick(Scrap scrap) {
        Intent intent = new Intent(ScrapListActivity.this, ScrapDetailActivity.class);
        intent.putExtra("id", scrap.getId());
        intent.putExtra("userId", scrap.getUserId());
        intent.putExtra("categoryId", scrap.getCategoryId());
        intent.putExtra("name", scrap.getName());
        intent.putExtra("description", scrap.getDescription());
        intent.putExtra("imageUrl", scrap.getImageUrl());
        intent.putExtra("location", scrap.getLocation());
        intent.putExtra("status", scrap.getStatus());
        intent.putExtra("createdAt", scrap.getCreatedAt().toDate().getTime());
        intent.putExtra("updatedAt", scrap.getUpdatedAt().toDate().getTime());
        startActivityForResult(intent, 1);
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        if (requestCode == 1) {
            loadScrapPosts();
        }
    }
}

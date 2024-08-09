package com.example.purchasingscrapapp.activity;

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

import java.util.ArrayList;
import java.util.List;

public class StaffScrapListActivity extends AppCompatActivity implements ScrapAdapter.OnScrapClickListener {

    private ScrapViewModel scrapViewModel;
    private ScrapAdapter scrapAdapter;
    private RecyclerView recyclerViewScrapList;
    private EditText editTextSearch;
    private ProgressBar progressBar;
    private List<Scrap> scrapList = new ArrayList<>();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_staff_scrap_list);

        editTextSearch = findViewById(R.id.edit_text_search);
        recyclerViewScrapList = findViewById(R.id.recycler_view_scrap_list);
        progressBar = findViewById(R.id.progress_bar);

        recyclerViewScrapList.setLayoutManager(new LinearLayoutManager(this));
        recyclerViewScrapList.setHasFixedSize(true);

        scrapAdapter = new ScrapAdapter(this);
        recyclerViewScrapList.setAdapter(scrapAdapter);

        scrapViewModel = new ViewModelProvider(this).get(ScrapViewModel.class);

        // Show ProgressBar
        progressBar.setVisibility(View.VISIBLE);
        recyclerViewScrapList.setVisibility(View.GONE);

        scrapViewModel.getAllScraps().observe(this, scraps -> {
            scrapList = scraps;
            scrapAdapter.setScraps(scraps);

            // Hide ProgressBar and show RecyclerView
            progressBar.setVisibility(View.GONE);
            recyclerViewScrapList.setVisibility(View.VISIBLE);
        });

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
        // Handle click if necessary
    }
}

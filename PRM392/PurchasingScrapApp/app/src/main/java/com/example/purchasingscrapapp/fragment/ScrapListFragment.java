package com.example.purchasingscrapapp.fragment;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import androidx.annotation.NonNull;
import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;
import androidx.lifecycle.ViewModelProvider;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import com.example.purchasingscrapapp.R;
import com.example.purchasingscrapapp.adapter.ScrapAdapter;
import com.example.purchasingscrapapp.model.Scrap;
import com.example.purchasingscrapapp.viewmodel.ScrapViewModel;

public class ScrapListFragment extends Fragment {

    private ScrapViewModel scrapViewModel;
    private ScrapAdapter scrapAdapter;
    private RecyclerView recyclerView;

    public ScrapListFragment() {
        // Required empty public constructor
    }

    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container,
                             @Nullable Bundle savedInstanceState) {
        return inflater.inflate(R.layout.fragment_scrap_list, container, false);
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        recyclerView = view.findViewById(R.id.recycler_view_scrap_list);
        recyclerView.setLayoutManager(new LinearLayoutManager(getContext()));

        scrapAdapter = new ScrapAdapter(scrap -> {
            // Handle scrap click
        });
        recyclerView.setAdapter(scrapAdapter);

        scrapViewModel = new ViewModelProvider(this).get(ScrapViewModel.class);
        scrapViewModel.getAllScraps().observe(getViewLifecycleOwner(), scraps -> {
            scrapAdapter.setScraps(scraps);
        });

        scrapViewModel.getScrapCategories().observe(getViewLifecycleOwner(), categories -> {
            // Handle categories if needed
        });
    }
}

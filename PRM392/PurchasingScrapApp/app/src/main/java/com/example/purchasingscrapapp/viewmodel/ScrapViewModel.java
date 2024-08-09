package com.example.purchasingscrapapp.viewmodel;

import androidx.lifecycle.LiveData;
import androidx.lifecycle.ViewModel;
import com.example.purchasingscrapapp.model.Scrap;
import com.example.purchasingscrapapp.model.ScrapCategory;
import com.example.purchasingscrapapp.repository.ScrapRepository;
import java.util.List;

public class ScrapViewModel extends ViewModel {

    private ScrapRepository scrapRepository;

    public ScrapViewModel() {
        scrapRepository = new ScrapRepository();
    }

    public LiveData<List<Scrap>> getScrapsByUser(String userId) {
        return scrapRepository.getScrapsByUser(userId);
    }

    public LiveData<Boolean> postScrap(Scrap scrap) {
        return scrapRepository.postScrap(scrap);
    }

    public LiveData<List<ScrapCategory>> getScrapCategories() {
        return scrapRepository.getScrapCategories();
    }

    public LiveData<Boolean> updateScrap(Scrap scrap) {
        return scrapRepository.updateScrap(scrap);
    }

    public LiveData<Boolean> deleteScrap(String scrapId) {
        return scrapRepository.deleteScrap(scrapId);
    }

    public LiveData<List<Scrap>> getAllScraps() {
        return scrapRepository.getAllScraps();
    }

    public LiveData<List<Scrap>> getScrapsByStatus(String status) {
        return scrapRepository.getScrapsByStatus(status);
    }

    public LiveData<Scrap> getScrapById(String scrapId) {
        return scrapRepository.getScrapById(scrapId);
    }
}

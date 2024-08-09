package com.example.purchasingscrapapp.repository;

import androidx.lifecycle.LiveData;
import androidx.lifecycle.MutableLiveData;
import com.example.purchasingscrapapp.model.Scrap;
import com.example.purchasingscrapapp.model.ScrapCategory;
import com.google.firebase.Timestamp;
import com.google.firebase.firestore.CollectionReference;
import com.google.firebase.firestore.FirebaseFirestore;
import com.google.firebase.firestore.QueryDocumentSnapshot;
import com.google.firebase.firestore.DocumentSnapshot;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class ScrapRepository {

    private FirebaseFirestore db = FirebaseFirestore.getInstance();
    private CollectionReference scrapsRef = db.collection("scraps");
    private CollectionReference categoriesRef = db.collection("scrap_categories");

    public LiveData<List<Scrap>> getScrapsByUser(String userId) {
        MutableLiveData<List<Scrap>> scrapsData = new MutableLiveData<>();
        scrapsRef.whereEqualTo("userId", userId).addSnapshotListener((value, error) -> {
            if (error != null) {
                return;
            }
            List<Scrap> scraps = new ArrayList<>();
            if (value != null) {
                for (QueryDocumentSnapshot document : value) {
                    Scrap scrap = convertToScrap(document);
                    scraps.add(scrap);
                }
            }
            scrapsData.setValue(scraps);
        });
        return scrapsData;
    }

    public LiveData<Boolean> postScrap(Scrap scrap) {
        MutableLiveData<Boolean> successData = new MutableLiveData<>();
        scrap.setCreatedAt(Timestamp.now());
        scrap.setStatus("pending");
        scrapsRef.document(scrap.getId()).set(scrap).addOnCompleteListener(task -> successData.setValue(task.isSuccessful()));
        return successData;
    }

    public LiveData<List<ScrapCategory>> getScrapCategories() {
        MutableLiveData<List<ScrapCategory>> categoriesData = new MutableLiveData<>();
        categoriesRef.addSnapshotListener((value, error) -> {
            if (error != null) {
                return;
            }
            List<ScrapCategory> categories = new ArrayList<>();
            if (value != null) {
                for (QueryDocumentSnapshot document : value) {
                    ScrapCategory category = document.toObject(ScrapCategory.class);
                    category.setId(document.getId());
                    categories.add(category);
                }
            }
            categoriesData.setValue(categories);
        });
        return categoriesData;
    }

    public LiveData<Boolean> updateScrap(Scrap scrap) {
        MutableLiveData<Boolean> successData = new MutableLiveData<>();
        scrap.setUpdatedAt(Timestamp.now());
        scrapsRef.document(scrap.getId()).set(scrap).addOnCompleteListener(task -> successData.setValue(task.isSuccessful()));
        return successData;
    }

    public LiveData<Boolean> deleteScrap(String scrapId) {
        MutableLiveData<Boolean> successData = new MutableLiveData<>();
        scrapsRef.document(scrapId).delete().addOnCompleteListener(task -> successData.setValue(task.isSuccessful()));
        return successData;
    }

    public LiveData<List<Scrap>> getAllScraps() {
        MutableLiveData<List<Scrap>> scrapsData = new MutableLiveData<>();
        scrapsRef.addSnapshotListener((value, error) -> {
            if (error != null) {
                return;
            }
            List<Scrap> scraps = new ArrayList<>();
            if (value != null) {
                for (QueryDocumentSnapshot document : value) {
                    Scrap scrap = convertToScrap(document);
                    scraps.add(scrap);
                }
            }
            scrapsData.setValue(scraps);
        });
        return scrapsData;
    }

    public LiveData<List<Scrap>> getScrapsByStatus(String status) {
        MutableLiveData<List<Scrap>> scrapsData = new MutableLiveData<>();
        scrapsRef.whereEqualTo("status", status).addSnapshotListener((value, error) -> {
            if (error != null) {
                return;
            }
            List<Scrap> scraps = new ArrayList<>();
            if (value != null) {
                for (QueryDocumentSnapshot document : value) {
                    Scrap scrap = convertToScrap(document);
                    scraps.add(scrap);
                }
            }
            scrapsData.setValue(scraps);
        });
        return scrapsData;
    }

    public LiveData<Scrap> getScrapById(String scrapId) {
        MutableLiveData<Scrap> scrapData = new MutableLiveData<>();
        scrapsRef.document(scrapId).get().addOnSuccessListener(documentSnapshot -> {
            if (documentSnapshot.exists()) {
                Scrap scrap = convertToScrap(documentSnapshot);
                scrapData.setValue(scrap);
            }
        });
        return scrapData;
    }

    private Scrap convertToScrap(DocumentSnapshot document) {
        Scrap scrap = document.toObject(Scrap.class);
        scrap.setId(document.getId());

        Object createdAt = document.get("createdAt");
        if (createdAt instanceof Long) {
            scrap.setCreatedAt(new Timestamp(new Date((Long) createdAt)));
        } else {
            scrap.setCreatedAt(document.getTimestamp("createdAt"));
        }

        Object updatedAt = document.get("updatedAt");
        if (updatedAt instanceof Long) {
            scrap.setUpdatedAt(new Timestamp(new Date((Long) updatedAt)));
        } else {
            scrap.setUpdatedAt(document.getTimestamp("updatedAt"));
        }

        return scrap;
    }
}

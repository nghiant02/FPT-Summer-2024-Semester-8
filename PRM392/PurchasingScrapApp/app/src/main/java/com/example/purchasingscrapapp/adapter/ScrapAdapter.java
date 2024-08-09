package com.example.purchasingscrapapp.adapter;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.bumptech.glide.Glide;
import com.example.purchasingscrapapp.R;
import com.example.purchasingscrapapp.model.Scrap;

import java.util.ArrayList;
import java.util.List;

public class ScrapAdapter extends RecyclerView.Adapter<ScrapAdapter.ScrapHolder> {

    private List<Scrap> scraps = new ArrayList<>();
    private OnScrapClickListener listener;

    public ScrapAdapter(OnScrapClickListener listener) {
        this.listener = listener;
    }

    @NonNull
    @Override
    public ScrapHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View itemView = LayoutInflater.from(parent.getContext())
                .inflate(R.layout.item_scrap, parent, false);
        return new ScrapHolder(itemView);
    }

    @Override
    public void onBindViewHolder(@NonNull ScrapHolder holder, int position) {
        Scrap currentScrap = scraps.get(position);
        holder.textViewName.setText(currentScrap.getName());
        holder.textViewDescription.setText(currentScrap.getDescription());
        holder.textViewLocation.setText(currentScrap.getLocation());
        Glide.with(holder.itemView.getContext()).load(currentScrap.getImageUrl()).into(holder.imageViewScrap);
    }

    @Override
    public int getItemCount() {
        return scraps.size();
    }

    public void setScraps(List<Scrap> scraps) {
        this.scraps = scraps;
        notifyDataSetChanged();
    }

    class ScrapHolder extends RecyclerView.ViewHolder {
        private TextView textViewName;
        private TextView textViewDescription;
        private TextView textViewLocation;
        private ImageView imageViewScrap;

        public ScrapHolder(@NonNull View itemView) {
            super(itemView);
            textViewName = itemView.findViewById(R.id.text_view_name);
            textViewDescription = itemView.findViewById(R.id.text_view_description);
            textViewLocation = itemView.findViewById(R.id.text_view_location);
            imageViewScrap = itemView.findViewById(R.id.image_view_scrap);

            itemView.setOnClickListener(v -> {
                int position = getAdapterPosition();
                if (listener != null && position != RecyclerView.NO_POSITION) {
                    listener.onScrapClick(scraps.get(position));
                }
            });
        }
    }

    public interface OnScrapClickListener {
        void onScrapClick(Scrap scrap);
    }
}

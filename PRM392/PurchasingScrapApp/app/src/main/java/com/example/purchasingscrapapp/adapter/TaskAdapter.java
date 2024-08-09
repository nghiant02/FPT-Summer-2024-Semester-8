package com.example.purchasingscrapapp.adapter;

import android.app.AlertDialog;
import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.core.content.ContextCompat;
import androidx.recyclerview.widget.RecyclerView;

import com.bumptech.glide.Glide;
import com.example.purchasingscrapapp.R;
import com.example.purchasingscrapapp.model.Task;
import com.example.purchasingscrapapp.viewmodel.ScrapViewModel;

import java.util.ArrayList;
import java.util.List;

public class TaskAdapter extends RecyclerView.Adapter<TaskAdapter.TaskHolder> {

    private List<Task> tasks = new ArrayList<>();
    private OnTaskClickListener listener;
    private OnTaskStatusChangeListener statusChangeListener;
    private ScrapViewModel scrapViewModel;
    private Context context;

    public TaskAdapter(OnTaskClickListener listener, OnTaskStatusChangeListener statusChangeListener, ScrapViewModel scrapViewModel, Context context) {
        this.listener = listener;
        this.statusChangeListener = statusChangeListener;
        this.scrapViewModel = scrapViewModel;
        this.context = context;
    }

    @NonNull
    @Override
    public TaskHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View itemView = LayoutInflater.from(parent.getContext())
                .inflate(R.layout.item_task, parent, false);
        return new TaskHolder(itemView);
    }

    @Override
    public void onBindViewHolder(@NonNull TaskHolder holder, int position) {
        Task currentTask = tasks.get(position);

        scrapViewModel.getScrapById(currentTask.getScrapId()).observeForever(scrap -> {
            if (scrap != null) {
                holder.textViewScrapName.setText(scrap.getName());
                holder.textViewLocation.setText(scrap.getLocation());
                Glide.with(context).load(scrap.getImageUrl()).into(holder.imageViewScrap);
            }
        });

        holder.textViewTaskDescription.setText(currentTask.getDescription());
        holder.textViewTaskStatus.setText(currentTask.getStatus());

        holder.itemView.setBackgroundColor(ContextCompat.getColor(context, R.color.white));
        holder.layoutButtons.setVisibility(View.VISIBLE);

        holder.textInProgress.setOnClickListener(v -> {
            if (statusChangeListener != null) {
                statusChangeListener.onStatusChange(currentTask, "in_progress");
            }
        });

        holder.textComplete.setOnClickListener(v -> {
            if (statusChangeListener != null) {
                statusChangeListener.onStatusChange(currentTask, "completed");
            }
        });

        holder.textCancel.setOnClickListener(v -> {
            if (statusChangeListener != null) {
                statusChangeListener.onStatusChange(currentTask, "cancelled");
            }
        });

        holder.itemView.setOnLongClickListener(v -> {
            new AlertDialog.Builder(context)
                    .setTitle("Delete Task")
                    .setMessage("Are you sure you want to delete this task?")
                    .setPositiveButton(android.R.string.yes, (dialog, which) -> {
                        tasks.remove(currentTask);
                        notifyDataSetChanged();
                    })
                    .setNegativeButton(android.R.string.no, null)
                    .show();
            return true;
        });
    }

    @Override
    public int getItemCount() {
        return tasks.size();
    }

    public void setTasks(List<Task> tasks) {
        this.tasks = tasks;
        notifyDataSetChanged();
    }

    class TaskHolder extends RecyclerView.ViewHolder {
        private TextView textViewScrapName;
        private TextView textViewTaskDescription;
        private TextView textViewTaskStatus;
        private TextView textViewLocation;
        private ImageView imageViewScrap;
        private TextView textInProgress;
        private TextView textComplete;
        private TextView textCancel;
        private LinearLayout layoutButtons;

        public TaskHolder(@NonNull View itemView) {
            super(itemView);
            textViewScrapName = itemView.findViewById(R.id.text_view_scrap_name);
            textViewTaskDescription = itemView.findViewById(R.id.text_view_task_description);
            textViewTaskStatus = itemView.findViewById(R.id.text_view_task_status);
            textViewLocation = itemView.findViewById(R.id.text_view_location);
            imageViewScrap = itemView.findViewById(R.id.image_view_scrap);
            textInProgress = itemView.findViewById(R.id.text_in_progress);
            textComplete = itemView.findViewById(R.id.text_complete);
            textCancel = itemView.findViewById(R.id.text_cancel);
            layoutButtons = itemView.findViewById(R.id.layout_buttons);

            itemView.setOnClickListener(v -> {
                int position = getAdapterPosition();
                if (listener != null && position != RecyclerView.NO_POSITION) {
                    listener.onTaskClick(tasks.get(position));
                }
            });
        }
    }

    public interface OnTaskClickListener {
        void onTaskClick(Task task);
    }

    public interface OnTaskStatusChangeListener {
        void onStatusChange(Task task, String newStatus);
    }
}

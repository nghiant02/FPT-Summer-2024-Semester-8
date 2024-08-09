package com.example.feedbackmanagementsystem.api;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.example.feedbackmanagementsystem.Model.Trainee;
import com.example.feedbackmanagementsystem.R;

import java.util.List;

public class TraineeAdapter extends RecyclerView.Adapter<TraineeAdapter.TraineeViewHolder> {
    private List<Trainee> traineeList;
    private EditTraineeCallback editTraineeCallback;
    private DeleteTraineeCallback deleteTraineeCallback;

    public interface EditTraineeCallback {
        void onEdit(Trainee trainee);
    }

    public interface DeleteTraineeCallback {
        void onDelete(Trainee trainee);
    }

    public TraineeAdapter(List<Trainee> traineeList, EditTraineeCallback editTraineeCallback, DeleteTraineeCallback deleteTraineeCallback) {
        this.traineeList = traineeList;
        this.editTraineeCallback = editTraineeCallback;
        this.deleteTraineeCallback = deleteTraineeCallback;
    }

    @NonNull
    @Override
    public TraineeViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.trainee_item, parent, false);
        return new TraineeViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull TraineeViewHolder holder, int position) {
        Trainee trainee = traineeList.get(position);
        holder.name.setText(trainee.getName());
        holder.email.setText(trainee.getEmail());
        holder.phone.setText(trainee.getPhone());
        holder.gender.setText(trainee.getGender());

        holder.btnEdit.setOnClickListener(v -> editTraineeCallback.onEdit(trainee));
        holder.btnDelete.setOnClickListener(v -> deleteTraineeCallback.onDelete(trainee));
    }

    @Override
    public int getItemCount() {
        return traineeList.size();
    }

    static class TraineeViewHolder extends RecyclerView.ViewHolder {
        TextView name, email, phone, gender;
        Button btnEdit, btnDelete;

        public TraineeViewHolder(@NonNull View itemView) {
            super(itemView);
            name = itemView.findViewById(R.id.name);
            email = itemView.findViewById(R.id.email);
            phone = itemView.findViewById(R.id.phone);
            gender = itemView.findViewById(R.id.gender);
            btnEdit = itemView.findViewById(R.id.btnEdit);
            btnDelete = itemView.findViewById(R.id.btnDelete);
        }
    }
}

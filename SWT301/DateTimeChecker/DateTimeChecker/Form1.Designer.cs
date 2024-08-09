namespace DateTimeChecker
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            txtDay = new TextBox();
            txtMonth = new TextBox();
            txtYear = new TextBox();
            btnClear = new Button();
            btnCheck = new Button();
            labelDay = new Label();
            labelMonth = new Label();
            labelYear = new Label();
            labelTitle = new Label();
            SuspendLayout();
            // 
            // txtDay
            // 
            txtDay.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtDay.Location = new Point(160, 100);
            txtDay.Margin = new Padding(4, 3, 4, 3);
            txtDay.Name = "txtDay";
            txtDay.Size = new Size(140, 29);
            txtDay.TabIndex = 0;
            // 
            // txtMonth
            // 
            txtMonth.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtMonth.Location = new Point(160, 150);
            txtMonth.Margin = new Padding(4, 3, 4, 3);
            txtMonth.Name = "txtMonth";
            txtMonth.Size = new Size(140, 29);
            txtMonth.TabIndex = 1;
            // 
            // txtYear
            // 
            txtYear.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            txtYear.Location = new Point(160, 200);
            txtYear.Margin = new Padding(4, 3, 4, 3);
            txtYear.Name = "txtYear";
            txtYear.Size = new Size(140, 29);
            txtYear.TabIndex = 2;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.LightGray;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnClear.Location = new Point(90, 250);
            btnClear.Margin = new Padding(4, 3, 4, 3);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(100, 40);
            btnClear.TabIndex = 3;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += new EventHandler(btnClear_Click);
            // 
            // btnCheck
            // 
            btnCheck.BackColor = Color.LightBlue;
            btnCheck.FlatStyle = FlatStyle.Flat;
            btnCheck.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btnCheck.Location = new Point(220, 250);
            btnCheck.Margin = new Padding(4, 3, 4, 3);
            btnCheck.Name = "btnCheck";
            btnCheck.Size = new Size(100, 40);
            btnCheck.TabIndex = 4;
            btnCheck.Text = "Check";
            btnCheck.UseVisualStyleBackColor = false;
            btnCheck.Click += new EventHandler(btnCheck_Click);
            // 
            // labelDay
            // 
            labelDay.AutoSize = true;
            labelDay.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            labelDay.Location = new Point(90, 103);
            labelDay.Name = "labelDay";
            labelDay.Size = new Size(36, 21);
            labelDay.TabIndex = 5;
            labelDay.Text = "Day";
            // 
            // labelMonth
            // 
            labelMonth.AutoSize = true;
            labelMonth.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            labelMonth.Location = new Point(90, 153);
            labelMonth.Name = "labelMonth";
            labelMonth.Size = new Size(56, 21);
            labelMonth.TabIndex = 6;
            labelMonth.Text = "Month";
            // 
            // labelYear
            // 
            labelYear.AutoSize = true;
            labelYear.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            labelYear.Location = new Point(90, 203);
            labelYear.Name = "labelYear";
            labelYear.Size = new Size(41, 21);
            labelYear.TabIndex = 7;
            labelYear.Text = "Year";
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            labelTitle.Location = new Point(80, 30);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(227, 32);
            labelTitle.TabIndex = 8;
            labelTitle.Text = "Date Time Checker";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(420, 320);
            Controls.Add(labelTitle);
            Controls.Add(labelYear);
            Controls.Add(labelMonth);
            Controls.Add(labelDay);
            Controls.Add(btnCheck);
            Controls.Add(btnClear);
            Controls.Add(txtYear);
            Controls.Add(txtMonth);
            Controls.Add(txtDay);
            Margin = new Padding(4, 3, 4, 3);
            Name = "Form1";
            Text = "Date Time Checker";
            ResumeLayout(false);
            PerformLayout();
        }

        private TextBox txtDay;
        private TextBox txtMonth;
        private TextBox txtYear;
        private Button btnClear;
        private Button btnCheck;
        private Label labelDay;
        private Label labelMonth;
        private Label labelYear;
        private Label labelTitle;
    }
}

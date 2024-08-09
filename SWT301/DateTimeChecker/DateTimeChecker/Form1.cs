using System;
using System.Windows.Forms;

namespace DateTimeChecker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtDay.Text, out int day) &&
                int.TryParse(txtMonth.Text, out int month) &&
                int.TryParse(txtYear.Text, out int year))
            {
                if (DateTimeChecker.IsValidDate(year, month, day))
                {
                    MessageBox.Show($"{day}/{month}/{year} is correct date time!", "Message");
                }
                else
                {
                    MessageBox.Show($"{day}/{month}/{year} is NOT correct date time!", "Message");
                }
            }
            else
            {
                MessageBox.Show("Please enter valid integers for day, month, and year.", "Error");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDay.Clear();
            txtMonth.Clear();
            txtYear.Clear();
        }
    }
}

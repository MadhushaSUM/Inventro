using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Inventro.Utils;
using System.Data;

namespace Inventro.View
{
    public partial class AnalyzeTransactions : UserControl
    {
        public AnalyzeTransactions()
        {
            InitializeComponent();

            dgTransactions.AutoGenerateColumns = false;
            dgBoughtItems.AutoGenerateColumns = false;

            dgTransactions.ItemsSource = Database.getTransactions().DefaultView;
        }

        private void btnDetails_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (dgTransactions.SelectedItem is not DataRowView selectedItem)
            {
                MessageBox.Show("Select a row first", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            dgBoughtItems.ItemsSource = Database.getBoughtItems((int)selectedItem["ID"]).DefaultView;
        }

        private void btnApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            DataTable original = Database.getTransactions();
            DataTable filtered = original.Clone();

            foreach (DataRow row in original.Rows)
            {
                DateTime created_at = ((DateTime)row["created_at"]).Date;

                if (filterDate.SelectedDate != null)
                {
                    DateTime filter = (DateTime)filterDate.SelectedDate;
                    if (created_at.Year == filter.Year && created_at.DayOfYear == filter.DayOfYear)
                    {
                        filtered.ImportRow(row);
                    }
                }
            }

            dgTransactions.ItemsSource = filtered.DefaultView;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgTransactions.SelectedItem is not DataRowView selectedItem)
            {
                MessageBox.Show("Select a row first", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            MessageBoxResult response = MessageBox.Show("Do you want delete this Transaction?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (response == MessageBoxResult.Yes)
            {
                Database.DeleteTransaction(Convert.ToInt32(selectedItem["id"]));
                dgTransactions.ItemsSource = Database.getTransactions().DefaultView;
                dgBoughtItems.ItemsSource = null;
            }
        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            decimal totalSalesForTheDay = Database.getTotalSaleForDay();
            decimal startingMoney;
            try
            {
                startingMoney = Convert.ToDecimal(txtStartMoney.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Enter valid number", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            txtEndMoney.Text = Convert.ToString(startingMoney - totalSalesForTheDay);
        }
    }
}

using Inventro.Utils;
using Inventro.View.UserControls;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Inventro.View
{
    public partial class DailyRates : UserControl
    {
        private Button btnSave;
        public DailyRates()
        {
            InitializeComponent();

            btnSave = new()
            {
                Content = "Save",
                FontSize = 15,
                Style = (Style)Application.Current.Resources["SidePannelButtons"]
            };
            btnSave.Click += btnSave_Click;

            ObservableCollection<Product> products = Database.getProductsObservable();
            dailyPricesStack.Children.Clear();

            foreach (Product product in products)
            {
                dailyPricesStack.Children.Add(new DawaseMilaView(product));
            }
            dailyPricesStack.Children.Add(btnSave);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dailyPricesStack.Children.Remove(btnSave);

                Dictionary<int, decimal> dict = [];
                foreach (DawaseMilaView view in dailyPricesStack.Children)
                {
                    dict[view.GetProduct().getId()] = view.GetDailyRate();
                }
                Database.updateProductDailyRate(dict);

                dailyPricesStack.Children.Add(btnSave);
                MessageBox.Show("Daily rates updated", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            } catch (Exception)
            {
                MessageBox.Show("Error updating daily rates", "System Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}

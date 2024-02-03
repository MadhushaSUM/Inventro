using Inventro.Utils;
using Inventro.View.UserControls.ProductItems;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Inventro.View.UserControls
{
    public partial class TransactionTab : UserControl
    {
        private readonly TabControl _tabControl;
        public TransactionTab(TabControl transactionTabs)
        {
            InitializeComponent();

            _tabControl = transactionTabs;

            BillStack.LayoutUpdated += setTotalSum;

            ObservableCollection<Product> products = Database.getProductsObservable();
            
            foreach (Product product in products)
            {
                ProductButton button = new(product);                

                if (product.getProductCode() == "POL_KUUDA")
                {
                    button.GetButton().Click += (sender, e) =>
                    {
                        foreach (ProductButton button1 in buttonStack.Children)
                        {
                            button1.GetBorder().BorderBrush = Brushes.Transparent;
                        }

                        button.GetBorder().BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#5B8FB9");

                        ProductItemGrid.Children.Clear();
                        ProductItemGrid.Children.Add(new PolKuudaItem(product));
                    };
                }
                else
                {
                    button.GetButton().Click += (sender, e) =>
                    {
                        foreach (ProductButton button1 in buttonStack.Children)
                        {
                            button1.GetBorder().BorderBrush = Brushes.Transparent;
                        }

                        button.GetBorder().BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#5B8FB9");

                        ProductItemGrid.Children.Clear();
                        ProductItemGrid.Children.Add(new GeneralItem(product));
                    };
                }

                buttonStack.Children.Add(button);
            }          
        }

        private void btnAddToList_Click(object sender, RoutedEventArgs e)
        {
            if (ProductItemGrid.Children.Count == 0) return;
            
            WeightItem item = (WeightItem)ProductItemGrid.Children[0];

            if (item.getProduct().getProductCode() == "POL_KUUDA")
            {
                PolKuudaBillItemView billItem = new(
                    item.getProduct(),
                    item.getWeightTurns(),
                    item.getContainerWeight(),
                    item.getMinusWeight(),
                    BillStack);
                BillStack.Children.Add(billItem);
            } else
            {
                GeneralBillItemView billItem = new(
                    item.getProduct(),
                    item.getWeights(),
                    item.getContainerWeight(),
                    item.getMinusWeight(),
                    BillStack);
                BillStack.Children.Add(billItem);
            }            
        }

        private void setTotalSum(object sender, EventArgs e)
        {
            decimal total = 0;
            foreach (var bill in BillStack.Children)
            {
                if (bill is GeneralBillItemView view)
                {
                    total += view.getBillValue();
                }
                else if (bill is PolKuudaBillItemView view1)
                {
                    total += view1.GetBillValue();
                }

            }

            total = Math.Round(total, 2);
            lblTotalSum.Content = "රු. " + total.ToString();
        }

        private decimal getTotalSum()
        {
            decimal total = 0;
            foreach (var bill in BillStack.Children)
            {
                if (bill is GeneralBillItemView view)
                {
                    total += view.getBillValue();
                }
                else if (bill is PolKuudaBillItemView view1)
                {
                    total += view1.GetBillValue();
                }
            }

            return total;
        }

        private void btnSaveBill_Click(object sender, RoutedEventArgs e)
        {
            if (BillStack.Children.Count == 0) return;

            try
            {
                ArrayList billItems = new ArrayList(BillStack.Children);
                int transactionId = Database.addNewTransaction(billItems, getTotalSum());

                List<UserControl> list = [];
                foreach (UserControl bill in BillStack.Children)
                {
                    list.Add(bill);
                }

                PrintBill.Print(list, transactionId);
                MessageBox.Show("Transaction added", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                BillStack.Children.Clear();

                if (_tabControl.Items.Count > 1)
                {
                    _tabControl.Items.Remove(this.Parent);
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Procedure failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

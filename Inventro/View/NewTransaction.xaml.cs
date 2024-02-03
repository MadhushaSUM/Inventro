using Inventro.View.UserControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace Inventro.View
{
    public partial class NewTransaction : Window
    {
        public NewTransaction()
        {
            InitializeComponent();

            TabItem newTab = new()
            {
                Header = $"ගනුදෙනුව {transactionTabs.Items.Count + 1}",
                FontSize = 18,
                Content = new TransactionTab(transactionTabs)
            };
            transactionTabs.Items.Add(newTab);
        }


        // Window Control Button functions
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void btnMaximise_Click(object sender, RoutedEventArgs e)
        {
            // Get the available working area (excluding the taskbar)
            var workingArea = SystemParameters.WorkArea;

            if (WindowState == WindowState.Maximized)
            {
                // Reset properties if the window is not maximized
                this.MaxWidth = double.PositiveInfinity;
                this.MaxHeight = double.PositiveInfinity;
                this.Width = workingArea.Width / 2;
                this.Height = workingArea.Height / 2;

                WindowState = WindowState.Normal;
            }
            else
            {
                // Set the window properties accordingly
                this.MaxWidth = workingArea.Width;
                this.MaxHeight = workingArea.Height;
                this.Width = workingArea.Width;
                this.Height = workingArea.Height;

                WindowState = WindowState.Maximized;
            }
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnAddTransactionTab_Click(object sender, RoutedEventArgs e)
        {
            TabItem newTab = new()
            {
                Header = $"ගනුදෙනුව {transactionTabs.Items.Count + 1}",
                FontSize=18,
                Content = new TransactionTab(transactionTabs)
            };

            transactionTabs.Items.Add(newTab);
        }
    }
}

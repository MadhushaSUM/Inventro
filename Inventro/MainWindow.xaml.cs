using Inventro.View;
using Inventro.View.UserControls;
using System.Windows;
using System.Windows.Input;

namespace Inventro
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            dailyPricesStack.Children.Clear();

            string[] arr = { "කුරුදු", "ගම්මිරිස්", "අගුරු", "කරුන්කා", "ගොරකා" };
            foreach (string item in arr)
            {
                dailyPricesStack.Children.Add(new DawaseMilaView(item, btnSaveClick));
            }
        }

        public void btnSaveClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Clicked!");
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        // Side pannel button actions
        private void btnNewTransaction_Click(object sender, RoutedEventArgs e)
        {
            NewTransaction newTransaction = new NewTransaction();
            newTransaction.ShowDialog();
        }



        // Window Control Button functions
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void btnMaximise_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized) WindowState = WindowState.Normal;
            else WindowState = WindowState.Maximized;
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
using Inventro.View;
using System.Windows;
using System.Windows.Input;
using settings = Inventro.View.Settings;

namespace Inventro
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        // Side pannel button actions
        private void btnDailyRates_Click(object sender, RoutedEventArgs e)
        {
            mainWindowGrid.Children.Clear();
            mainWindowGrid.Children.Add(new DailyRates());
        }
        private void btnNewTransaction_Click(object sender, RoutedEventArgs e)
        {
            NewTransaction newTransaction = new NewTransaction();
            newTransaction.ShowDialog();
        }
        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {
            mainWindowGrid.Children.Clear();
            mainWindowGrid.Children.Add(new settings.Base());
        }
        private void btnAnalyzeTransactions_Click(object sender, RoutedEventArgs e)
        {
            mainWindowGrid.Children.Clear();
            mainWindowGrid.Children.Add(new AnalyzeTransactions());
        }


        // Window Control Button functions
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
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
    }
}
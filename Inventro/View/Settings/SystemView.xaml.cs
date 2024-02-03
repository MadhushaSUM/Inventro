using Inventro.Utils;
using System.Windows;
using System.Windows.Controls;

namespace Inventro.View.Settings
{
    public partial class SystemView : UserControl
    {
        public SystemView()
        {
            InitializeComponent();

            int[] steps = Database.GetKiloNGramSteps();
            txtKiloStep.Text = steps[0].ToString();
            txtGramStep.Text = steps[1].ToString();
        }

        private void btnUpdateKiloStep_Click(object sender, RoutedEventArgs e)
        {
            int kiloStep;
            try
            {
                kiloStep = Convert.ToInt32(txtKiloStep.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Enter valid number", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            Database.UpdateKiloNGramSteps(1, kiloStep);
            MessageBox.Show("Kilo Step Updated", "Updated", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnUpdateGramStep_Click(object sender, RoutedEventArgs e)
        {
            int gramStep;
            try
            {
                gramStep = Convert.ToInt32(txtGramStep.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Enter valid number", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            Database.UpdateKiloNGramSteps(2, gramStep);
            MessageBox.Show("Gram Step Updated", "Updated", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}

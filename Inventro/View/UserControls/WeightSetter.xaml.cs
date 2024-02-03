using Inventro.Utils;
using System.Windows;
using System.Windows.Controls;

namespace Inventro.View.UserControls
{
    public partial class WeightSetter : UserControl
    {
        private readonly int KILOCHANGER_VALUE;
        private readonly int GRAMCHANGER_VALUE;
        public WeightSetter()
        {
            InitializeComponent();

            int[] steps = Database.GetKiloNGramSteps();

            KILOCHANGER_VALUE = steps[0];
            GRAMCHANGER_VALUE = steps[1];
        }

        private void btnKiloPlus_Click(object sender, RoutedEventArgs e)
        {
            if (Int32.TryParse(txtKilos.Text, out int result))
            {
                txtKilos.Text = (result + KILOCHANGER_VALUE).ToString();
            }
            else
            {
                txtKilos.Text = "0";
            }

        }

        private void btnKiloMinus_Click(object sender, RoutedEventArgs e)
        {
            if (Int32.TryParse(txtKilos.Text, out int result))
            {
                if (result - KILOCHANGER_VALUE <= 0) txtKilos.Text = "0";
                else txtKilos.Text = (result - KILOCHANGER_VALUE).ToString();
            }
            else
            {
                txtKilos.Text = "0";
            }
        }

        private void btnGramPlus_Click(object sender, RoutedEventArgs e)
        {
            if (Int32.TryParse(txtGrams.Text, out int result))
            {
                if (result + GRAMCHANGER_VALUE >= 1000) return;
                txtGrams.Text = (result + GRAMCHANGER_VALUE).ToString();
            }
            else
            {
                txtGrams.Text = "0";
            }
        }

        private void btnGramMinus_Click(object sender, RoutedEventArgs e)
        {
            if (Int32.TryParse(txtGrams.Text, out int result))
            {
                if (result - GRAMCHANGER_VALUE <= 0) txtGrams.Text = "0";
                else txtGrams.Text = (result - GRAMCHANGER_VALUE).ToString();
            }
            else
            {
                txtGrams.Text = "0";
            }
        }

        public decimal getValue()
        {
            return Convert.ToDecimal(txtKilos.Text) + (Convert.ToDecimal(txtGrams.Text) / 1000);
        }
    }
}

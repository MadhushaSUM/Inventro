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
using System.Configuration;


namespace Inventro.View.Settings
{
    public partial class Base : UserControl
    {
        public Base()
        {
            InitializeComponent();
        }

        private void BtnSetProducts_Click(object sender, RoutedEventArgs e)
        {
            settingBase.Content = new ProductSettings();
        }

        private void btnSystem_Click(object sender, RoutedEventArgs e)
        {
            settingBase.Content = new SystemView();
        }

        private void btnPageSettings_Click(object sender, RoutedEventArgs e)
        {
            settingBase.Content = new PageSettings();
        }
    }
}

using Inventro.Utils;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Inventro.View.Settings
{
    public partial class PageSettings : UserControl
    {
        private List<Label> headerLabels = [];
        private List<Label> billItemLabels = [];
        private List<Label> footerLabels = [];

        private Label? SelectedHeaderLabel = null;
        private Label? SelectedBillItemLabel = null;
        private Label? SelectedFooterLabel = null;

        public PageSettings()
        {
            InitializeComponent();


            foreach (FontFamily font in Fonts.SystemFontFamilies)
            {
                ComboBoxItem item1 = new()
                {
                    Content = font.Source
                };

                fontCombo.Items.Add(item1);

                ComboBoxItem item2 = new()
                {
                    Content = font.Source
                };

                BillfontCombo.Items.Add(item2);

                ComboBoxItem item3 = new()
                {
                    Content = font.Source
                };

                footerFontCombo.Items.Add(item3);
            }

            CreatePage page = new();
            Canvas header = page.CreateHeader(headerLabels, 9999);
            header.Height = 250;
            header.Width = 600;

            ScaleTransform transform = new()
            {
                ScaleX = 2,
                ScaleY = 2,
                CenterX = 0,
                CenterY = 0
            };
            header.RenderTransform = transform;
            
            header.Background = Brushes.White;
            PageHeaderViewer.Content = header;
            setHeaderLabelClickDownHandlers();

            Canvas billItem = page.CreateBillItem(billItemLabels, false, false);
            billItem.Background = Brushes.White;
            billItem.Height = 300;
            billItem.Width = 600;
            billItem.RenderTransform = transform;
            BillItemViewer.Content = billItem;
            setBillItemLabelClickDownHandlers();

            Canvas footer = page.CreateFooter(footerLabels);
            footer.Background = Brushes.White;
            footer.Width = 600;
            footer.Height = 100;
            footer.RenderTransform = transform;
            FooterViewer.Content = footer;
            setFooterLabelClickDownHandlers();
        }

        private void setHeaderLabelClickDownHandlers()
        {
            foreach (Label label in headerLabels)
            {
                label.MouseDown += (sender, e) =>
                {
                    SelectedHeaderLabel = label;

                    foreach (Label label1 in headerLabels)
                    {
                        label1.BorderBrush = null;
                        label1.BorderThickness = new Thickness(0);
                    }

                    label.BorderBrush = Brushes.Red;
                    label.BorderThickness = new Thickness(1);

                    txtHeaderText.Text = label.Content.ToString();
                    txtSizeText.Text = label.FontSize.ToString();
                    fontCombo.SelectedItem = new ComboBoxItem()
                    {
                        Content = label.FontFamily.ToString(),
                    };

                    setVCentered.IsChecked = label.VerticalContentAlignment == VerticalAlignment.Center;
                    setHCentered.IsChecked = label.HorizontalContentAlignment == HorizontalAlignment.Center;
                    
                    txtHeaderTopMargin.Text = Canvas.GetTop(label).ToString();
                    txtHeaderLeftMargin.Text = Canvas.GetLeft(label).ToString();
                    txtHeaderWidth.Text = label.Width.ToString();
                    txtHeaderHeight.Text = label.Height.ToString();
                    setBold.IsChecked = label.FontWeight == FontWeights.Bold;
                    setItalic.IsChecked = label.FontStyle == FontStyles.Italic;
                };
            }
        }
        private void setBillItemLabelClickDownHandlers()
        {
            foreach (Label label in billItemLabels)
            {
                label.MouseDown += (sender, e) =>
                {
                    SelectedBillItemLabel = label;

                    foreach (Label label1 in billItemLabels)
                    {
                        label1.BorderBrush = null;
                        label1.BorderThickness = new Thickness(0);
                    }

                    label.BorderBrush = Brushes.Blue;
                    label.BorderThickness = new Thickness(1);

                    txtBillText.Text = label.Content.ToString();
                    txtBillSizeText.Text = label.FontSize.ToString();
                    BillfontCombo.SelectedItem = new ComboBoxItem()
                    {
                        Content = label.FontFamily.ToString(),
                    };

                    setBillVCentered.IsChecked = label.VerticalContentAlignment == VerticalAlignment.Center;
                    setBillHCentered.IsChecked = label.HorizontalContentAlignment == HorizontalAlignment.Center;

                    txtBillTopMargin.Text = Canvas.GetTop(label).ToString();
                    txtBillLeftMargin.Text = Canvas.GetLeft(label).ToString();
                    txtBillWidth.Text = label.Width.ToString();
                    txtBillHeight.Text = label.Height.ToString();
                    setBillBold.IsChecked = label.FontWeight == FontWeights.Bold;
                    setBillItalic.IsChecked = label.FontStyle == FontStyles.Italic;
                };
            }
        }
        private void setFooterLabelClickDownHandlers()
        {
            foreach (Label label in footerLabels)
            {
                label.MouseDown += (sender, e) =>
                {
                    SelectedFooterLabel = label;

                    foreach (Label label1 in footerLabels)
                    {
                        label1.BorderBrush = null;
                        label1.BorderThickness = new Thickness(0);
                    }

                    label.BorderBrush = Brushes.Yellow;
                    label.BorderThickness = new Thickness(1);

                    txtFooterText.Text = label.Content.ToString();
                    txtFooterSizeText.Text = label.FontSize.ToString();
                    footerFontCombo.SelectedItem = new ComboBoxItem()
                    {
                        Content = label.FontFamily.ToString(),
                    };

                    setFooterVCentered.IsChecked = label.VerticalContentAlignment == VerticalAlignment.Center;
                    setFooterHCentered.IsChecked = label.HorizontalContentAlignment == HorizontalAlignment.Center;

                    txtFooterTopMargin.Text = Canvas.GetTop(label).ToString();
                    txtFooterLeftMargin.Text = Canvas.GetLeft(label).ToString();
                    txtFooterWidth.Text = label.Width.ToString();
                    txtFooterHeight.Text = label.Height.ToString();
                    setFooterBold.IsChecked = label.FontWeight == FontWeights.Bold;
                    setFooterItalic.IsChecked = label.FontStyle == FontStyles.Italic;
                };
            }
        }


        private void btnSaveHeader_Click(object sender, RoutedEventArgs e)
        {
            SavePage.SavePageComponent(headerLabels, "Header");
        }
        private void btnLoadDefaultHeader_Click(object sender, RoutedEventArgs e)
        {
            CreatePage page = new();
            Canvas header = page.CreateDefaultHeader(headerLabels);
            header.Height = 200;
            header.Width = 600;

            ScaleTransform transform = new()
            {
                ScaleX = 2,
                ScaleY = 2,
                CenterX = 0,
                CenterY = 0
            };
            header.RenderTransform = transform;

            header.Background = Brushes.White;
            PageHeaderViewer.Content = header;
            setHeaderLabelClickDownHandlers();
        }
        private void txtHeaderText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SelectedHeaderLabel == null) return;
            
            SelectedHeaderLabel.Content = txtHeaderText.Text;
        }
        private void txtSizeText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SelectedHeaderLabel == null) return;
            if (Double.TryParse(txtSizeText.Text, out double fontSize))
            {
                SelectedHeaderLabel.FontSize = fontSize;
            }
        }
        private void txtHeaderTopMargin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SelectedHeaderLabel == null) return;
            if (Double.TryParse(txtHeaderTopMargin.Text, out double topMargin))
            {
                Canvas.SetTop(SelectedHeaderLabel, topMargin);
            }
        }        
        private void txtHeaderLeftMargin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SelectedHeaderLabel == null) return;
            if (Double.TryParse(txtHeaderLeftMargin.Text, out double leftMargin))
            {
                Canvas.SetLeft(SelectedHeaderLabel, leftMargin);
            }
        }
        private void fontCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedHeaderLabel == null) return;

            ComboBoxItem item = (ComboBoxItem)fontCombo.SelectedItem;
            try
            {
                SelectedHeaderLabel.FontFamily = new FontFamily(item.Content.ToString());
            } catch
            {
                MessageBox.Show("Set a proper Font Family", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            
        }
        private void txtHeaderWidth_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SelectedHeaderLabel == null) return;
            if (Double.TryParse(txtHeaderWidth.Text, out double width))
            {
                SelectedHeaderLabel.Width = width;
            }
        }
        private void txtHeaderHeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SelectedHeaderLabel == null) return;
            if (Double.TryParse(txtHeaderHeight.Text, out double height))
            {
                SelectedHeaderLabel.Height = height;
            }
        }
        private void setBold_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedHeaderLabel == null) return;
            if (setBold.IsChecked == true)
            {
                SelectedHeaderLabel.FontWeight = FontWeights.Bold;
            } else
            {
                SelectedHeaderLabel.FontWeight = FontWeights.Regular;
            }
        }
        private void setItalic_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedHeaderLabel == null) return;
            if (setItalic.IsChecked == true)
            {
                SelectedHeaderLabel.FontStyle = FontStyles.Italic;
            }
            else
            {
                SelectedHeaderLabel.FontStyle = FontStyles.Normal;
            }
        }
        private void setVCentered_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedHeaderLabel == null) return;
            if (setVCentered.IsChecked == true)
            {
                SelectedHeaderLabel.VerticalContentAlignment = VerticalAlignment.Center;
            }
            else
            {
                SelectedHeaderLabel.VerticalContentAlignment = VerticalAlignment.Top;
            }
        }
        private void setHCentered_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedHeaderLabel == null) return;
            if (setHCentered.IsChecked == true)
            {
                SelectedHeaderLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
            }
            else
            {
                SelectedHeaderLabel.HorizontalContentAlignment = HorizontalAlignment.Left;
            }
        }



        private void txtBillText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SelectedBillItemLabel == null) return;

            SelectedBillItemLabel.Content = txtBillText.Text;
        }
        private void txtBillSizeText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SelectedBillItemLabel == null) return;
            if (Double.TryParse(txtBillSizeText.Text, out double fontSize))
            {
                SelectedBillItemLabel.FontSize = fontSize;
            }
        }
        private void BillfontCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedBillItemLabel == null) return;

            ComboBoxItem item = (ComboBoxItem)BillfontCombo.SelectedItem;
            try
            {
                SelectedBillItemLabel.FontFamily = new FontFamily(item.Content.ToString());
            }
            catch
            {
                MessageBox.Show("Set a proper Font Family", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        private void setBillVCentered_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedBillItemLabel == null) return;
            if (setBillVCentered.IsChecked == true)
            {
                SelectedBillItemLabel.VerticalContentAlignment = VerticalAlignment.Center;
            }
            else
            {
                SelectedBillItemLabel.VerticalContentAlignment = VerticalAlignment.Top;
            }
        }
        private void setBillHCentered_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedBillItemLabel == null) return;
            if (setBillHCentered.IsChecked == true)
            {
                SelectedBillItemLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
            }
            else
            {
                SelectedBillItemLabel.HorizontalContentAlignment = HorizontalAlignment.Left;
            }
        }
        private void txtBillTopMargin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SelectedBillItemLabel == null) return;
            if (Double.TryParse(txtBillTopMargin.Text, out double topMargin))
            {
                Canvas.SetTop(SelectedBillItemLabel, topMargin);
            }
        }
        private void txtBillLeftMargin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SelectedBillItemLabel == null) return;
            if (Double.TryParse(txtBillLeftMargin.Text, out double leftMargin))
            {
                Canvas.SetLeft(SelectedBillItemLabel, leftMargin);
            }
        }
        private void txtBillWidth_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SelectedBillItemLabel == null) return;
            if (Double.TryParse(txtBillWidth.Text, out double width))
            {
                SelectedBillItemLabel.Width = width;
            }
        }
        private void txtBillHeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SelectedBillItemLabel == null) return;
            if (Double.TryParse(txtBillHeight.Text, out double height))
            {
                SelectedBillItemLabel.Height = height;
            }
        }
        private void setBillBold_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedBillItemLabel == null) return;
            if (setBillBold.IsChecked == true)
            {
                SelectedBillItemLabel.FontWeight = FontWeights.Bold;
            }
            else
            {
                SelectedBillItemLabel.FontWeight = FontWeights.Regular;
            }
        }
        private void setBillItalic_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedBillItemLabel == null) return;
            if (setBillItalic.IsChecked == true)
            {
                SelectedBillItemLabel.FontStyle = FontStyles.Italic;
            }
            else
            {
                SelectedBillItemLabel.FontStyle = FontStyles.Normal;
            }
        }
        private void btnSaveBill_Click(object sender, RoutedEventArgs e)
        {
            SavePage.SavePageComponent(billItemLabels, "BillItem");
        }
        private void btnLoadDefaultBill_Click(object sender, RoutedEventArgs e)
        {
            CreatePage page = new();
            Canvas billItem = page.CreateDefaultBillItem(billItemLabels);

            billItem.Background = Brushes.White;
            billItem.Height = 300;
            billItem.Width = 600;

            ScaleTransform transform = new()
            {
                ScaleX = 2,
                ScaleY = 2,
                CenterX = 0,
                CenterY = 0
            };

            billItem.RenderTransform = transform;
            BillItemViewer.Content = billItem;
            setBillItemLabelClickDownHandlers();
        }



        private void txtFooterText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SelectedFooterLabel == null) return;

            SelectedFooterLabel.Content = txtFooterText.Text;
        }
        private void txtFooterSizeText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SelectedFooterLabel == null) return;
            if (Double.TryParse(txtFooterSizeText.Text, out double fontSize))
            {
                SelectedFooterLabel.FontSize = fontSize;
            }
        }
        private void FooterFontCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedFooterLabel == null) return;

            ComboBoxItem item = (ComboBoxItem)footerFontCombo.SelectedItem;
            try
            {
                SelectedFooterLabel.FontFamily = new FontFamily(item.Content.ToString());
            }
            catch
            {
                MessageBox.Show("Set a proper Font Family", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
        private void setFooterVCentered_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedFooterLabel == null) return;
            if (setFooterVCentered.IsChecked == true)
            {
                SelectedFooterLabel.VerticalContentAlignment = VerticalAlignment.Center;
            }
            else
            {
                SelectedFooterLabel.VerticalContentAlignment = VerticalAlignment.Top;
            }
        }
        private void setFooterHCentered_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedFooterLabel == null) return;
            if (setFooterHCentered.IsChecked == true)
            {
                SelectedFooterLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
            }
            else
            {
                SelectedFooterLabel.HorizontalContentAlignment = HorizontalAlignment.Left;
            }
        }
        private void txtFooterTopMargin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SelectedFooterLabel == null) return;
            if (Double.TryParse(txtFooterTopMargin.Text, out double topMargin))
            {
                Canvas.SetTop(SelectedFooterLabel, topMargin);
            }
        }
        private void txtFooterLeftMargin_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SelectedFooterLabel == null) return;
            if (Double.TryParse(txtFooterLeftMargin.Text, out double leftMargin))
            {
                Canvas.SetLeft(SelectedFooterLabel, leftMargin);
            }
        }
        private void txtFooterWidth_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SelectedFooterLabel == null) return;
            if (Double.TryParse(txtFooterWidth.Text, out double width))
            {
                SelectedFooterLabel.Width = width;
            }
        }
        private void txtFooterHeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SelectedFooterLabel == null) return;
            if (Double.TryParse(txtFooterHeight.Text, out double height))
            {
                SelectedFooterLabel.Height = height;
            }
        }
        private void setFooterBold_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedFooterLabel == null) return;
            if (setFooterBold.IsChecked == true)
            {
                SelectedFooterLabel.FontWeight = FontWeights.Bold;
            }
            else
            {
                SelectedFooterLabel.FontWeight = FontWeights.Regular;
            }
        }
        private void setFooterItalic_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedFooterLabel == null) return;
            if (setFooterItalic.IsChecked == true)
            {
                SelectedFooterLabel.FontStyle = FontStyles.Italic;
            }
            else
            {
                SelectedFooterLabel.FontStyle = FontStyles.Normal;
            }
        }
        private void btnSaveFooter_Click(object sender, RoutedEventArgs e)
        {
            SavePage.SavePageComponent(footerLabels, "Footer");
        }
        private void btnLoadDefaultFooter_Click(object sender, RoutedEventArgs e)
        {
            CreatePage page = new();
            ScaleTransform transform = new()
            {
                ScaleX = 2,
                ScaleY = 2,
                CenterX = 0,
                CenterY = 0
            };

            Canvas footer = page.CreateDefaultFooter(footerLabels);
            footer.Background = Brushes.White;
            footer.Width = 600;
            footer.Height = 100;
            footer.RenderTransform = transform;
            FooterViewer.Content = footer;
            setFooterLabelClickDownHandlers();
        }
    }
}

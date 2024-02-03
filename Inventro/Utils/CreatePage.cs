using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Inventro.Utils
{
    public class CreatePage
    {
        private readonly string[] headerLabelNames = ["lblAddress", "lblDateTime", "lblDateTimeValue", "lblStoreName", "lblTelephone", "lblInvoiceNo", "lblInvoiceNoValue"];
        private readonly string[] billItemLabelNames = ["lblDot", "lblProductName", "lblDailyRate", "lblMeasuredWeight", "lblMeasuredWeightValue", "lblContainerWeight", "lblContainerWeightValue", "lblMinusWeight", "lblMinusWeightValue", "lblTotalWeightValue", "lblTotalMoneyValue"];
        private readonly string[] footerLabelNames = ["lblTotal", "lblTotalValue"];
        public Canvas CreateHeader(List<Label> labels, int transactionID)
        {
            labels.Clear();

            Canvas header = new()
            {
                Width = 300,
                Height = 125,
            };

            try
            {
                foreach (string labelName in headerLabelNames)
                {
                    string filePath = "BillLayout/Header/" + labelName + ".json";
                    string fileReading = File.ReadAllText(filePath);
                    LabelSavingFormat labelFormats = JsonSerializer.Deserialize<LabelSavingFormat>(fileReading);

                    Label label = CreateLabel(labelFormats, 0);

                    if (labelName == "lblInvoiceNoValue")
                    {
                        label.Content = transactionID.ToString();
                    }

                    labels.Add(label);
                    header.Children.Add(label);
                }
            } catch (Exception)
            {
                MessageBox.Show("Saved Header layout is corrupted.\nDefault view is loading...", "Files Corrupted", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                header = CreateDefaultHeader(labels);
            }

            return header;
        }
        public Canvas CreateDefaultHeader(List<Label> labels)
        {
            labels.Clear();

            Canvas header = new()
            {
                Width = 300,
                Height = 125,
            };


            Label lblStoreName = new()
            {
                Name = "lblStoreName",
                Content = "ජයවීර ට්‍රේඩර්ස්",
                FontSize = 20,
                Width = 300,
                Height = 40,
                FontFamily = new FontFamily("Arial"),
                HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
            };
            Canvas.SetTop(lblStoreName, 0);
            Canvas.SetLeft(lblStoreName, 0);
            header.Children.Add(lblStoreName);
            labels.Add(lblStoreName);

            Label lblAddress = new()
            {
                Name = "lblAddress",
                Content = "නොම්බර 97, බෙලිඅත්ත පාර, හක්මණ.",
                FontSize = 7,
                Width = 300,
                Height = 20,
                FontFamily = new FontFamily("Arial"),
                HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
            };
            Canvas.SetTop(lblAddress, 40);
            Canvas.SetLeft(lblAddress, 0);
            header.Children.Add(lblAddress);
            labels.Add(lblAddress);

            Label lblTelephone = new()
            {
                Name = "lblTelephone",
                Content = "දුරකථන අංක: 0773164736",
                FontSize = 7,
                Width = 300,
                Height = 20,
                FontFamily = new FontFamily("Arial"),
                HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
            };
            Canvas.SetTop(lblTelephone, 60);
            Canvas.SetLeft(lblTelephone, 0);
            header.Children.Add(lblTelephone);
            labels.Add(lblTelephone);

            Label lblDateTime = new()
            {
                Name = "lblDateTime",
                Content = "දිනය සහ වේලාව:",
                FontSize = 7,
                Width = 100,
                Height = 20,
                FontFamily = new FontFamily("arial"),
                HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
            };
            Canvas.SetTop(lblDateTime, 80);
            Canvas.SetLeft(lblDateTime, 0);
            header.Children.Add(lblDateTime);
            labels.Add(lblDateTime);

            Label lblDateTimeValue = new()
            {
                Name = "lblDateTimeValue",
                Content = "YYYY - MM - DD HH : mm (AM/PM)",
                FontSize = 7,
                Width = 200,
                Height = 20,
                FontFamily = new FontFamily("Arial"),
                HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
            };
            Canvas.SetTop(lblDateTimeValue, 80);
            Canvas.SetLeft(lblDateTimeValue, 100);
            header.Children.Add(lblDateTimeValue);
            labels.Add(lblDateTimeValue);

            Label lblInvoiceNo = new()
            {
                Name = "lblInvoiceNo",
                Content = "Invoice No.:",
                FontSize = 7,
                Width = 100,
                Height = 25,
                FontFamily = new FontFamily("arial"),
                HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
            };
            Canvas.SetTop(lblInvoiceNo, 100);
            Canvas.SetLeft(lblInvoiceNo, 0);
            header.Children.Add(lblInvoiceNo);
            labels.Add(lblInvoiceNo);

            Label lblInvoiceNoValue = new()
            {
                Name = "lblInvoiceNoValue",
                Content = "#####",
                FontSize = 7,
                Width = 200,
                Height = 25,
                FontFamily = new FontFamily("arial"),
                HorizontalContentAlignment = System.Windows.HorizontalAlignment.Left,
                VerticalContentAlignment = System.Windows.VerticalAlignment.Center,
            };
            Canvas.SetTop(lblInvoiceNoValue, 100);
            Canvas.SetLeft(lblInvoiceNoValue, 100);
            header.Children.Add(lblInvoiceNoValue);
            labels.Add(lblInvoiceNoValue);

            return header;
        }

        
        public Canvas CreateBillItem(List<Label> labels, bool containerFlag, bool minusFlag)
        {
            labels.Clear();

            Canvas billItem = new()
            {
                Width = 300,
                Height = 150,
            };

            try
            {
                double negativeTopMargin = 0;
                foreach (string labelName in billItemLabelNames)
                {
                    string filePath = "BillLayout/BillItem/" + labelName + ".json";
                    string fileReading = File.ReadAllText(filePath);
                    LabelSavingFormat labelFormats = JsonSerializer.Deserialize<LabelSavingFormat>(fileReading);

                    if (containerFlag)
                    {
                        if (labelName == "lblContainerWeight" || labelName == "lblContainerWeightValue")
                        {
                            negativeTopMargin += labelFormats.Height / 2;
                            continue;
                        }
                    }
                    if (minusFlag)
                    {
                        if (labelName == "lblMinusWeight" || labelName == "lblMinusWeightValue")
                        {
                            negativeTopMargin += labelFormats.Height / 2;
                            continue;
                        }
                    }

                    Label label = CreateLabel(labelFormats, negativeTopMargin);
                    labels.Add(label);
                    billItem.Children.Add(label);
                }

                Line line = new()
                {
                    Stroke = Brushes.Black,
                    X1 = 120,
                    X2 = 300,
                    StrokeThickness = 1,
                };
                Canvas.SetTop(line, 120 - negativeTopMargin);
                Canvas.SetLeft(line, 0);
                billItem.Children.Add(line);

                billItem.Height -= negativeTopMargin;  
            }
            catch (Exception)
            {
                MessageBox.Show("Saved BillItem layout is corrupted.\nDefault view is loading...", "Files Corrupted", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                billItem = CreateDefaultBillItem(labels);
            }

            return billItem;
        }
        public Canvas CreatePolKuudaBillItem(Product product, List<decimal> weightTurns, decimal containerWeight, decimal minusWeight)
        {

            Canvas billItem = new()
            {
                Width = 300
            };

            try
            {
            string[] arr1 = ["lblDot", "lblProductName", "lblDailyRate"];
            foreach (string labelName in arr1)
            {

                string filePath = "BillLayout/BillItem/" + labelName + ".json";
                string fileReading = File.ReadAllText(filePath);
                LabelSavingFormat labelFormats = JsonSerializer.Deserialize<LabelSavingFormat>(fileReading);

                Label label = CreateLabel(labelFormats, 0);

                switch (labelName)
                {
                    case "lblProductName":
                        label.Content = product.getProductName();
                        break;
                    case "lblDailyRate":
                        label.Content = $"( 1kg = රු.{product.getDailyRate()})"; ;
                        break;
                }

                billItem.Children.Add(label);
            }
                
            string filePath1 = "BillLayout/BillItem/lblMeasuredWeight.json";
            string fileReading1 = File.ReadAllText(filePath1);
            LabelSavingFormat lblMeasuredWeightFormat = JsonSerializer.Deserialize<LabelSavingFormat>(fileReading1);

            string filePath2 = "BillLayout/BillItem/lblMeasuredWeightValue.json";
            string fileReading2 = File.ReadAllText(filePath2);
            LabelSavingFormat lblMeasuredWeightValueFormat = JsonSerializer.Deserialize<LabelSavingFormat>(fileReading2);

                for (int i = 0; i < weightTurns.Count; i++)
                {
                    Label label1 = CreatePolKuudaWeightTurnLabel(lblMeasuredWeightFormat, i, 0, $"{i + 1} වන වටය");
                    billItem.Children.Add(label1);

                    Label label2 = CreatePolKuudaWeightTurnLabel(lblMeasuredWeightValueFormat, i, 0, $"{weightTurns[i]:F3 kg}");
                billItem.Children.Add(label2);
            }

            string[] arr2 = ["lblContainerWeight", "lblContainerWeightValue", "lblMinusWeight", "lblMinusWeightValue", "lblTotalWeightValue", "lblTotalMoneyValue"];

            decimal totalWeight = 0;
            foreach (decimal weight in weightTurns)
            {
                totalWeight += weight;
            }
            totalWeight -= (containerWeight + minusWeight);

            double negativeTopMargin = 0;
            foreach (string labelName in arr2)
            {

                string filePath = "BillLayout/BillItem/" + labelName + ".json";
                string fileReading = File.ReadAllText(filePath);
                LabelSavingFormat labelFormats = JsonSerializer.Deserialize<LabelSavingFormat>(fileReading);

                if (containerWeight == 0)
                {
                    if (labelName == "lblContainerWeight" || labelName == "lblContainerWeightValue")
                    {
                        negativeTopMargin += labelFormats.Height / 2;
                        continue;
                    }
                }
                if (minusWeight == 0)
                {
                    if (labelName == "lblMinusWeight" || labelName == "lblMinusWeightValue")
                    {
                        negativeTopMargin += labelFormats.Height / 2;
                        continue;
                    }
                }

                string content = null;

                    switch (labelName)
                    {
                        case "lblContainerWeight":
                            content = "කූඩ බර";
                            break;
                        case "lblContainerWeightValue":
                            content = $"{Math.Round(containerWeight, 3):F3} kg";
                            break;
                        case "lblMinusWeightValue":
                            content = $"{Math.Round(minusWeight, 3):F3} kg";
                            break;
                        case "lblTotalWeightValue":
                            content = $"{Math.Round(totalWeight, 3):F3} kg";
                        break;
                    case "lblTotalMoneyValue":
                        content = $"රු. {Math.Round(totalWeight * product.getDailyRate(), 2):F2}";
                        break;
                }

                Label label = CreatePolKuudaWeightTurnLabel(labelFormats, weightTurns.Count - 1, negativeTopMargin, content);
                billItem.Children.Add(label);
            }

            Line line = new()
            {
                Stroke = Brushes.Black,
                X1 = 120,
                X2 = 300,
                StrokeThickness = 1,
            };
            Canvas.SetTop(line, 120 + weightTurns.Count * 25 - negativeTopMargin);
            Canvas.SetLeft(line, 0);
            billItem.Children.Add(line);

            billItem.Height = 150 + (weightTurns.Count - 1) * 25;
            billItem.Height -= negativeTopMargin;

        }
            catch (Exception)
            {
                MessageBox.Show("Saved BillItem layout is corrupted.\nDefault view is loading...", "Files Corrupted", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

            return billItem;
        }
        public Canvas CreateDefaultBillItem(List<Label> labels)
        {
            Canvas BillItem = new()
            {
                Height = 150,
                Width = 300,
            };

            Label lblDot = new()
            {
                Name = "lblDot",
                Content = "●",
                FontSize = 15,
                Width = 20,
                Height = 30,
                FontFamily = new FontFamily("Arial"),
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
            };
            Canvas.SetTop(lblDot, 0);
            Canvas.SetLeft(lblDot, 0);
            BillItem.Children.Add(lblDot);
            labels.Add(lblDot);

            Label lblProductName = new()
            {
                Name = "lblProductName",
                Content = "Product Name",
                FontSize = 15,
                Width = 130,
                Height = 30,
                FontFamily = new FontFamily("Arial"),
                HorizontalContentAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Center,
            };
            Canvas.SetTop(lblProductName, 0);
            Canvas.SetLeft(lblProductName, 20);
            BillItem.Children.Add(lblProductName);
            labels.Add(lblProductName);

            Label lblDailyRate = new()
            {
                Name = "lblDailyRate",
                Content = "( 1kg = රු. ###.##)",
                FontSize = 10,
                Width = 150,
                Height = 30,
                FontFamily = new FontFamily("Arial"),
                HorizontalContentAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Center,
            };
            Canvas.SetTop(lblDailyRate, 0);
            Canvas.SetLeft(lblDailyRate, 150);
            BillItem.Children.Add(lblDailyRate);
            labels.Add(lblDailyRate);

            Label lblMeasuredWeight = new()
            {
                Name = "lblMeasuredWeight",
                Content = "කිරූ සම්පූර්ණ බර",
                FontSize = 10,
                Width = 100,
                Height = 30,
                FontFamily = new FontFamily("Arial"),
                HorizontalContentAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Center,
            };
            Canvas.SetTop(lblMeasuredWeight, 30);
            Canvas.SetLeft(lblMeasuredWeight, 20);
            BillItem.Children.Add(lblMeasuredWeight);
            labels.Add(lblMeasuredWeight);

            Label lblMeasuredWeightValue = new()
            {
                Name = "lblMeasuredWeightValue",
                Content = "##.### kg",
                FontSize = 10,
                Width = 80,
                Height = 30,
                FontFamily = new FontFamily("Arial"),
                HorizontalContentAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Center,
            };
            Canvas.SetTop(lblMeasuredWeightValue, 30);
            Canvas.SetLeft(lblMeasuredWeightValue, 120);
            BillItem.Children.Add(lblMeasuredWeightValue);
            labels.Add(lblMeasuredWeightValue);

            Label lblContainerWeight = new()
            {
                Name = "lblContainerWeight",
                Content = "මලු බර",
                FontSize = 10,
                Width = 100,
                Height = 30,
                FontFamily = new FontFamily("Arial"),
                HorizontalContentAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Center,
            };
            Canvas.SetTop(lblContainerWeight, 60);
            Canvas.SetLeft(lblContainerWeight, 20);
            BillItem.Children.Add(lblContainerWeight);
            labels.Add(lblContainerWeight);

            Label lblContainerWeightValue = new()
            {
                Name = "lblContainerWeightValue",
                Content = "##.### kg",
                FontSize = 10,
                Width = 80,
                Height = 30,
                FontFamily = new FontFamily("Arial"),
                HorizontalContentAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Center,
            };
            Canvas.SetTop(lblContainerWeightValue, 60);
            Canvas.SetLeft(lblContainerWeightValue, 120);
            BillItem.Children.Add(lblContainerWeightValue);
            labels.Add(lblContainerWeightValue);

            Label lblMinusWeight = new()
            {
                Name = "lblMinusWeight",
                Content = "අඩු වන බර",
                FontSize = 10,
                Width = 100,
                Height = 30,
                FontFamily = new FontFamily("Arial"),
                HorizontalContentAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Center,
            };
            Canvas.SetTop(lblMinusWeight, 90);
            Canvas.SetLeft(lblMinusWeight, 20);
            BillItem.Children.Add(lblMinusWeight);
            labels.Add(lblMinusWeight);

            Label lblMinusWeightValue = new()
            {
                Name = "lblMinusWeightValue",
                Content = "##.### kg",
                FontSize = 10,
                Width = 80,
                Height = 30,
                FontFamily = new FontFamily("Arial"),
                HorizontalContentAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Center,
            };
            Canvas.SetTop(lblMinusWeightValue, 90);
            Canvas.SetLeft(lblMinusWeightValue, 120);
            BillItem.Children.Add(lblMinusWeightValue);
            labels.Add(lblMinusWeightValue);

            Line line = new()
            {
                Stroke = Brushes.Black,
                X1 = 120,
                X2 = 300,
                StrokeThickness = 1,
            };
            Canvas.SetTop(line, 120);
            Canvas.SetLeft(line, 0);
            BillItem.Children.Add(line);

            Label lblTotalWeightValue = new()
            {
                Name = "lblTotalWeightValue",
                Content = "##.### kg",
                FontSize = 10,
                Width = 80,
                Height = 30,
                FontFamily = new FontFamily("Arial"),
                HorizontalContentAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Center,
            };
            Canvas.SetTop(lblTotalWeightValue, 120);
            Canvas.SetLeft(lblTotalWeightValue, 120);
            BillItem.Children.Add(lblTotalWeightValue);
            labels.Add(lblTotalWeightValue);

            Label lblTotalMoneyValue = new()
            {
                Name = "lblTotalMoneyValue",
                Content = "රු.####.##",
                FontSize = 10,
                Width = 100,
                Height = 30,
                FontFamily = new FontFamily("Arial"),
                HorizontalContentAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Center,
            };
            Canvas.SetTop(lblTotalMoneyValue, 120);
            Canvas.SetLeft(lblTotalMoneyValue, 200);
            BillItem.Children.Add(lblTotalMoneyValue);
            labels.Add(lblTotalMoneyValue);

            return BillItem;
        }
        
        
        public Canvas CreateFooter(List<Label> labels)
        {
            labels.Clear();

            Canvas footer = new()
            {
                Width = 300,
                Height = 50,
            };

            try
            {
                foreach (string labelName in footerLabelNames)
                {
                    string filePath = "BillLayout/Footer/" + labelName + ".json";
                    string fileReading = File.ReadAllText(filePath);
                    LabelSavingFormat labelFormats = JsonSerializer.Deserialize<LabelSavingFormat>(fileReading);

                    Label label = CreateLabel(labelFormats, 0);
                    labels.Add(label);
                    footer.Children.Add(label);
                }

                Line line1 = new()
                {
                    Stroke = Brushes.Black,
                    X1 = 200,
                    X2 = 300,
                    StrokeThickness = 1,
                };
                Canvas.SetTop(line1, 5);
                Canvas.SetLeft(line1, 0);
                footer.Children.Add(line1);

                Line line2 = new()
                {
                    Stroke = Brushes.Black,
                    X1 = 200,
                    X2 = 300,
                    StrokeThickness = 1,
                };
                Canvas.SetTop(line2, 35);
                Canvas.SetLeft(line2, 0);
                footer.Children.Add(line2);
            }
            catch (Exception)
            {
                MessageBox.Show("Saved Footer layout is corrupted.\nDefault view is loading...", "Files Corrupted", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                footer = CreateDefaultFooter(labels);
            }

            return footer;
        }
        public Canvas CreateDefaultFooter(List<Label> labels)
        {
            Canvas footer = new()
            {
                Width = 300,
                Height = 50,
            };

            Line line1 = new()
            {
                Stroke = Brushes.Black,
                X1 = 200,
                X2 = 300,
                StrokeThickness = 1,
            };
            Canvas.SetTop(line1, 5);
            Canvas.SetLeft(line1, 0);
            footer.Children.Add(line1);

            Line line2 = new()
            {
                Stroke = Brushes.Black,
                X1 = 200,
                X2 = 300,
                StrokeThickness = 1,
            };
            Canvas.SetTop(line2, 35);
            Canvas.SetLeft(line2, 0);
            footer.Children.Add(line2);

            Label lblTotal = new()
            {
                Name = "lblTotal",
                Content = "මුළු වටිනාකම",
                FontSize = 15,
                Width = 150,
                Height = 30,
                FontFamily = new FontFamily("Arial"),
                FontWeight = FontWeights.Bold,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
            };
            Canvas.SetTop(lblTotal, 5);
            Canvas.SetLeft(lblTotal, 0);
            labels.Add(lblTotal);
            footer.Children.Add(lblTotal);

            Label lblTotalValue = new()
            {
                Name = "lblTotalValue",
                Content = "රු.#####.##",
                FontSize = 15,
                Width = 100,
                Height = 30,
                FontFamily = new FontFamily("Arial"),
                FontWeight = FontWeights.Bold,
                HorizontalContentAlignment = HorizontalAlignment.Left,
                VerticalContentAlignment = VerticalAlignment.Center,
            };
            Canvas.SetTop(lblTotalValue, 5);
            Canvas.SetLeft(lblTotalValue, 200);
            labels.Add(lblTotalValue);
            footer.Children.Add(lblTotalValue);

            return footer;
        }





        private Label CreateLabel(LabelSavingFormat jsonFile, double negativeTopMargin)
        {
            Label label = new()
            {
                Name = jsonFile.Name,
                Content = jsonFile.Content,
                FontSize = jsonFile.FontSize,
                FontFamily = new FontFamily(jsonFile.FontFamily),
                VerticalContentAlignment = jsonFile.ContentVerticalyCentered ? VerticalAlignment.Center : VerticalAlignment.Top,
                HorizontalContentAlignment = jsonFile.ContentHorizontalyCentered ? HorizontalAlignment.Center : HorizontalAlignment.Left,
                Width = jsonFile.Width,
                Height = jsonFile.Height,
                FontWeight = jsonFile.FontWeight ? FontWeights.Bold : FontWeights.Regular,
                FontStyle = jsonFile.FontStyle ? FontStyles.Italic : FontStyles.Normal
            };

            Canvas.SetTop(label, jsonFile.TopMargin - negativeTopMargin);
            Canvas.SetLeft(label, jsonFile.LeftMargin);

            return label;
        }
        private Label CreatePolKuudaWeightTurnLabel(LabelSavingFormat jsonFile, int i, double negativeTopMargin, string content = null)
        {
            Label label = new()
            {
                Name = jsonFile.Name,
                FontSize = jsonFile.FontSize,
                FontFamily = new FontFamily(jsonFile.FontFamily),
                VerticalContentAlignment = jsonFile.ContentVerticalyCentered ? VerticalAlignment.Center : VerticalAlignment.Top,
                HorizontalContentAlignment = jsonFile.ContentHorizontalyCentered ? HorizontalAlignment.Center : HorizontalAlignment.Left,
                Width = jsonFile.Width,
                Height = jsonFile.Height,
                FontWeight = jsonFile.FontWeight ? FontWeights.Bold : FontWeights.Regular,
                FontStyle = jsonFile.FontStyle ? FontStyles.Italic : FontStyles.Normal
            };

            if (content == null)
            {
                label.Content = jsonFile.Content;
            } else
            {
                label.Content = content;
            }

            Canvas.SetTop(label, jsonFile.TopMargin + i * 25 - negativeTopMargin);
            Canvas.SetLeft(label, jsonFile.LeftMargin);

            return label;
        }
    }
}

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
using System.Drawing;
using System.Globalization;

namespace ConvertRGBApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RGBData rgbData;
        private HexData hexData;
        private HSLData hslData;
        private Background background;
        public MainWindow()
        {
            InitializeComponent();
            rgbData = new RGBData(setRGBText);
            hexData = new HexData(setHexText);
            hslData = new HSLData(setHSLText);
            background = new Background(setBackground);

            rgbData.attach(hexData);
            rgbData.attach(background);
            rgbData.attach(hslData);

            hexData.attach(rgbData);
            hexData.attach(background);
            hexData.attach(hslData);

            hslData.attach(rgbData);
            hslData.attach(background);
            hslData.attach(hexData);

            RGBButton.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 255, 0));
            HexButton.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 255, 0));
            HSLButton.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 255, 0));
        }

        private void RGB_TextChanged(object sender, RoutedEventArgs e)
        {
            if(!rgbData.IsLocked) rgbData.setVal(RGBText.Text);
        }

        private void Hex_TextChanged(object sender, RoutedEventArgs e)
        {
            if(!hexData.IsLocked) hexData.setVal(HexText.Text);
        }
        private void HSL_TextChanged(object sender, RoutedEventArgs e)
        {
            if(!hslData.IsLocked) hslData.setVal(HSLText.Text);
        }

        private void turnRGB(object sender, RoutedEventArgs e)
        {
            if (rgbData.getIsTurned())
            {
                rgbData.setIsTurned(false);
                rgbData.detach(hexData);
                rgbData.detach(hslData);
                rgbData.detach(background);
                hexData.detach(rgbData);
                hslData.detach(rgbData);
                RGBButton.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 0, 0));
            }
            else
            {
                rgbData.setIsTurned(true);
                if(hexData.getIsTurned()) hexData.attach(rgbData);
                if(hslData.getIsTurned()) hslData.attach(rgbData);
                if(hexData.getIsTurned())rgbData.attach(hexData);
                if(hslData.getIsTurned())rgbData.attach(hslData);
                rgbData.attach(background);
                RGBButton.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 255, 0));
            }
        }
        private void turnHex(object sender, RoutedEventArgs e)
        {
            if (hexData.getIsTurned())
            {
                hexData.setIsTurned(false);
                Hex_TextChanged(sender, e);
                hexData.detach(rgbData);
                hexData.detach(hslData);
                hexData.detach(background);
                //HexText.Clear();
                rgbData.detach(hexData);
                hslData.detach(hexData);
                HexButton.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 0, 0));
            }
            else
            {
                hexData.setIsTurned(true);
                if (rgbData.getIsTurned()) rgbData.attach(hexData);
                if (hslData.getIsTurned()) hslData.attach(hexData);
                if (rgbData.getIsTurned()) hexData.attach(rgbData);
                if (hslData.getIsTurned()) hexData.attach(hslData);
                hexData.attach(background);
                //Hex_TextChanged(sender, e);
                HexButton.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 255, 0));
            }
        }
        private void turnHSL(object sender, RoutedEventArgs e)
        {
            if (hslData.getIsTurned())
            {
                hslData.setIsTurned(false);
                //HSL_TextChanged(sender, e);
                hslData.detach(hexData);
                hslData.detach(rgbData);
                hslData.detach(background);
                //HSLText.Clear();
                hexData.detach(hslData);
                rgbData.detach(hslData);
                HSLButton.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 0, 0));
            }
            else
            {
                hslData.setIsTurned(true);
                if (hexData.getIsTurned()) hexData.attach(hslData);
                if (rgbData.getIsTurned()) rgbData.attach(hslData);
                if (hexData.getIsTurned()) hslData.attach(hexData);
                if (rgbData.getIsTurned()) hslData.attach(rgbData);
                hslData.attach(background);
                //HSL_TextChanged(sender, e);
                HSLButton.Background = new SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 255, 0));
            }
        }

        public void setRGBText(String str)
        {
            RGBText.Text = str;
        }

        public void setHexText(String str)
        {
            HexText.Text = str;
        }
        public void setHSLText(String str)
        {
            HSLText.Text = str;
        }
        public void setBackground(String str)
        {
            System.Drawing.Color color = background.getVal();
            System.Windows.Media.Color c = System.Windows.Media.Color.FromRgb(color.R, color.G, color.B);
            this.Background = new SolidColorBrush(c);
        }



    }
}

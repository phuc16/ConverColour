using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Globalization;
namespace ConvertRGBApp
{
    class HSLData : Observable, Observer
    {
        private string HSLString;
        private bool isTurned;
        public HSLData(UIDelegate del)
        {
            HSLString = string.Empty;
            isTurned = true;
            this.del = del;
        }
        public void update(System.Drawing.Color c)
        {
            if (c == Color.Empty)
            {
                updateUI(string.Empty);
                return;
            }
            HSLString = c.GetHue().ToString("N2", CultureInfo.InvariantCulture) + ", " + c.GetSaturation().ToString("N2", CultureInfo.InvariantCulture) + ", " + c.GetBrightness().ToString("N2", CultureInfo.InvariantCulture);
            updateUI(HSLString);
        }
        public bool getIsTurned()
        {
            return isTurned;
        }
        public void setIsTurned(bool b)
        {
            isTurned = b;
        }
        private bool isValid(double h, double s, double l)
        {
            if (h < 0.0 || h > 360.0) return false;
            else if (s < 0.0 || s > 1.0) return false;
            else if (l < 0.0 || l > 1.0) return false;
            return true;
        }
        private static double GetColorComponent(double temp1, double temp2, double temp3)
        {
            if (temp3 < 0.0)
                temp3 += 1.0;
            else if (temp3 > 1.0)
                temp3 -= 1.0;

            if (temp3 < 1.0 / 6.0)
                return temp1 + (temp2 - temp1) * 6.0 * temp3;
            else if (temp3 < 0.5)
                return temp2;
            else if (temp3 < 2.0 / 3.0)
                return temp1 + ((temp2 - temp1) * ((2.0 / 3.0) - temp3) * 6.0);
            else
                return temp1;
        }
        public static Color ColorFromHSL(double h, double s, double l)
        {
            double r = 0, g = 0, b = 0;
            if (l != 0)
            {
                if (s == 0)
                    r = g = b = l;
                else
                {
                    double temp2;
                    if (l < 0.5)
                        temp2 = l * (1.0 + s);
                    else
                        temp2 = l + s - (l * s);

                    double temp1 = 2.0 * l - temp2;

                    r = GetColorComponent(temp1, temp2, h + 1.0 / 3.0);
                    g = GetColorComponent(temp1, temp2, h);
                    b = GetColorComponent(temp1, temp2, h - 1.0 / 3.0);
                }
            }
            return Color.FromArgb((int)(255 * r), (int)(255 * g), (int)(255 * b));

        }
        public void setVal(string str)
        {
            string[] values = str.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (values.Length != 3)
            {
                notify(Color.Empty);
                return;
            }
            else
            {
                double h, s, l;
                try
                {
                    NumberFormatInfo provider = new NumberFormatInfo();
                    provider.NumberDecimalSeparator = ".";
                    h = Convert.ToDouble(values[0], provider);
                    s = Convert.ToDouble(values[1], provider);
                    l = Convert.ToDouble(values[2], provider);
                    if (!isValid(h, s, l)) throw new Exception();
                    System.Drawing.Color color = ColorFromHSL(h, s, l);
                    HSLString = str;
                    notify(color);
                }
                catch (Exception e)
                {
                    notify(Color.Empty);
                }
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ConvertRGBApp
{
    class RGBData : Observable, Observer
    {
        private string RGBString;
        private bool isTurned;
        public RGBData(UIDelegate del)
        {
            RGBString = string.Empty;
            isTurned = true;
            this.del = del;
        }
        void Observer.update(System.Drawing.Color c)
        {
            if (c == Color.Empty)
            {
                updateUI(string.Empty);
                return;
            }
            RGBString = c.R.ToString() + ", " + c.G.ToString() + ", " + c.B.ToString();
            updateUI(RGBString);
        }
        public bool getIsTurned()
        {
            return isTurned;
        }
        public void setIsTurned(bool b)
        {
            isTurned = b;
        }
        private bool isValid(int val)
        {
            if (0 <= val && val <= 255) return true;
            else return false;
        }
        public void setVal(string str)
        {
            string[] colorList = str.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (colorList.Length != 3)
            {
                notify(Color.Empty);
                return;
            }
            else
            {
                int r, g, b;
                try
                {
                    r = int.Parse(colorList[0]);
                    g = int.Parse(colorList[1]);
                    b = int.Parse(colorList[2]);
                    if (!isValid(r) || !isValid(g) || !isValid(b)) throw new Exception();
                    System.Drawing.Color color = System.Drawing.Color.FromArgb(r, g, b);
                    RGBString = str;
                    notify(color);
                }
                catch(Exception e)
                {
                    notify(Color.Empty);
                }
            }
        }
    }
}

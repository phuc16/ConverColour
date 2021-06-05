using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ConvertRGBApp
{
    class Background : Observable, Observer
    {
        private Color backgroundColor;
        
        public Background(UIDelegate del)
        {
            backgroundColor = Color.Empty;
            this.del = del;
        }
        public void update(System.Drawing.Color c)
        {
            if (c == Color.Empty)
            {
                backgroundColor = Color.White;
                updateUI(string.Empty);
                return;
            }
            backgroundColor = c;
            string tmp = "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
            updateUI(tmp);
        }
        public Color getVal()
        {
            return backgroundColor;
        }
    }
}

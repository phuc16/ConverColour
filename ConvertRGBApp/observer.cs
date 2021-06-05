using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Drawing;

namespace ConvertRGBApp
{
    public interface Observer
    {
        void update(System.Drawing.Color c);
    }

    public class Error : Observer
    {
        private bool checkError;
        public Error()
        {
            checkError = false;
        }
        public void update(System.Drawing.Color c)
        {

            var r = c.R;
            var g = c.G;
            var b = c.B;

            if (c == Color.Empty) checkError = true;
            else checkError = false;
        }
        public string get()
        {
            if (checkError) return "Invalid color! Try Again.";
            else return "Valid color ^^";
        }
    }
}

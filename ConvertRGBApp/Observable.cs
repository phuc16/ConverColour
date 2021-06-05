using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ConvertRGBApp
{
    public class Observable
    {
        protected List<Observer> observersList;
        public delegate void UIDelegate(String str);
        protected UIDelegate del;
        protected bool isLocked;

        public Observable() {
            observersList = new List<Observer>();
            isLocked = false;
            del = null;
        }
        protected void updateUI(string str)
        {
            isLocked = true;
            del(str);
            isLocked = false;
        }

        public bool IsLocked
        {
            get { return isLocked; }
        }
        public void attach(Observer observer)
        {
            if (!observersList.Contains(observer)) observersList.Add(observer);
        }
        public void detach(Observer observer)
        {
            if (observersList.Contains(observer)) observersList.Remove(observer);
        }
        protected void notify(System.Drawing.Color c)
        {
            foreach(var obs in observersList)
            {
                obs.update(c);
            }
        }
    }
}

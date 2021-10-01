using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarAddingApplication.Elements
{
    public class CheckBox : Element
    {
        public CheckBox(By by) : base(by) { }

        public CheckBox(string xpath) : base(xpath) { }

        public bool IsSelected(params object[] param)
        {
            return FindElement(param).Selected;
        }
    }
}

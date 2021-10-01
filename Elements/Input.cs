using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarAddingApplication.Elements
{
    /// <summary>
    /// Специальный класс с переработанными методами для Selenium - "Поля ввода данных"
    /// </summary>
    public class Input : Element
    {
        public Input(By by) : base(by) { }

        public Input(string xpath) : base(xpath) { }

        /// <summary> Чистим поле </summary>
        public void ClearField(params object[] param)
        {
            var el = FindElement(param);
            var textLength = el.GetAttribute("value").Length;
            for (var i = 0; i < textLength; i++)
                el.SendKeys(Keys.Backspace);
        }
    }
}

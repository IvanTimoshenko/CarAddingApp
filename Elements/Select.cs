using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarAddingApplication.Elements
{
    /// <summary>
    /// Специальный класс с переработанными методами для Selenium - "Выпадающий список"
    /// </summary>
    public class Select : Element
    {
        public Select(By by) : base(by) { }

        public Select(string xpath) : base(xpath) { }


        /// <summary> Метод для выбора пункта из выпадающего списка </summary>
        /// <param name="param"> Параметры для параметризованного XPath </param>
        /// <returns></returns>
        public void SelectDropDownList(int index, params object[] param)
        {
            new SelectElement(GetElement(GetByLocator(param))).SelectByIndex(index);
        }

        /// <summary>
        /// Метод для проверки активности элемента класса "Select"
        /// </summary>
        /// <param name="select">Объект класса "Select", содержащий путь ко всем "/options"</param>
        /// <param name="index">Индекс для услуги "/options[INDEX]"</param>
        /// <returns>True/False</returns>
        public bool IsSelected(Select select, int index)
        {
            //проверяем, что услуга выбрана ([{index + 1}] - нумерация для элемента "/options" начинается с 1, а не с 0)
            return Program.WebDriver.Driver.FindElement(By.XPath($"{select.Xpath}/option[{index + 1}]")).Selected;
        }

        /// <summary>
        /// Метод для проверки неактивного элемента класса "Select"
        /// </summary>
        /// <param name="select">Объект класса "Select", содержащий путь ко всем "/options"</param>
        /// <returns>True/False</returns>
        public bool IsSelected(Select select)
        {
            //проверяем, что услуга выбрана ([{index + 1}] - нумерация для элемента "/options" начинается с 1, а не с 0)
            return Program.WebDriver.Driver.FindElement(By.XPath($"{select.Xpath}/option[1]")).Selected;
        }

        /// <summary> Получает элемент </summary>
        /// <param name="param"> Параметры для параметризированного XPath </param>
        /// <returns> IWebElement </returns>
        private IWebElement GetElement(params object[] param)
        {
            return Program.WebDriver.Wait.Until(ExpectedConditions.ElementExists(GetByLocator(param)));
        }
    }
}

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace CarAddingApplication.Elements
{
    /// <summary>
    /// Специальный класс с переработанными методами для Selenium - общий
    /// </summary>
    public class Element
    {
        /// <summary> Локатор элемента </summary>
        public By By { get; protected set; } = null;

        /// <summary> Параметризированный XPath элемента </summary>
        public string Xpath { get; protected set; } = null;

        /// <summary> Создать новый элемент </summary>
        /// <param name="by"> Локатор элемента </param>
        public Element(By by)
        {
            By = by;
        }

        /// <summary> Создать новый элемент с параметризированным XPath </summary>
        /// <param name="xpath"> Параметризированный XPath элемента </param>
        public Element(string xpath)
        {
            Xpath = xpath;
        }

        /// <summary> Поиск элемента по локатору </summary>
        /// <param name="param"> Параметры для параметризированного XPath </param>
        /// <returns> Объект класса IWebElement </returns>
        public IWebElement FindElement(params object[] param)
        {
            // Ожидаем элемент (тут вылетит ошибка, если элемент не найден)
            WaitAnimation(param);

            // Возвращаем элемент
            return GetElement(param);
        }

        /// <summary> Получает элемент </summary>
        /// <param name="param"> Параметры для параметризированного XPath </param>
        /// <returns> IWebElement </returns>
        private IWebElement GetElement(params object[] param)
        {
            return Program.WebDriver.Wait.Until(ExpectedConditions.ElementExists(GetByLocator(param)));
        }

        /// <summary> Поиск элементов по локатору </summary>
        /// <param name="param"> Параметры для параметризированного XPath </param>
        /// <returns> Лист IWebElement </returns>
        public List<IWebElement> FindElements(params object[] param)
        {
            return GetElements(param);
        }

        /// <summary> Получает элементы </summary>
        /// <param name="param"> Параметры для параметризированного XPath </param>
        /// <returns> List<IWebElement> </returns>
        private List<IWebElement> GetElements(params object[] param)
        {
            return Program.WebDriver.Driver.FindElements(GetByLocator(param)).ToList();
        }
        /// <summary> Метод проверки присутствия элемента на странице </summary>
        /// <param name="param"> Параметры для параметризированного XPath </param>
        /// <returns> True/False </returns>
        public bool Exist(params object[] param)
        {
            return FindElements(param).Any();
        }

        /// <summary> Получить локатор текущего элемента </summary>
        /// <param name="param"> Параметры для параметризированного XPath </param>
        public By GetByLocator(params object[] param)
        {
            By by = By;
            if (By == null)
                by = By.XPath(string.Format(Xpath, param));
            return by;
        }

        /// <summary> Получить xpath </summary>
        /// <param name="param"> Параметры для параметризированного XPath </param>
        public string GetXpathLocator(params object[] param)
        {
            var by = GetByLocator(param).ToString();
            return by.Substring(10, by.Length - 10);
        }

        /// <summary> Элемент исчезает </summary>
        /// <param name="param"> Параметры для параметризированного XPath </param>
        /// <param name="waitTime"> Время (в секндах) для ожидание пропадания элемента </param>
        public void ElementDisappear(int? waitTime = null, params object[] param)
        {
            if (waitTime == null)
            {
                waitTime = Config.WebDriverWait.Seconds;
            }

            try { new WebDriverWait(Program.WebDriver.Driver, TimeSpan.FromSeconds(5)).Until(ExpectedConditions.ElementExists(GetByLocator(param))); }
            catch (WebDriverTimeoutException) { return; }

            for (var sw = Stopwatch.StartNew(); sw.Elapsed.TotalSeconds < waitTime;)
            {
                if (!Exist(param))
                    return;
            }
        }

        /// <summary> Ожидаем пока элемент и страница загрузятся полностью </summary>
        /// <param name="param"> Параметры для параметризированного XPath </param>
        /// <returns> Найден ли элемент </returns>
        private void WaitAnimation(params object[] param)
        {
            try
            {
                var element = GetElement(param);
                var location = element.Location;

                for (var sw = Stopwatch.StartNew(); sw.Elapsed < Config.WebDriverWait;)
                {
                    Program.WebDriver.Wait.Until(ExpectedConditions.ElementExists(GetByLocator(param)));
                    Program.WebDriver.Wait.Until(ExpectedConditions.ElementIsVisible(GetByLocator(param)));

                    var _location = element.Location;
                    if (_location.Equals(location))
                    {
                        break;
                    }
                    else
                    {
                        location = _location;
                        element = GetElement(param);
                    }
                }
            }
            catch (WebDriverTimeoutException e) { throw e; }
            catch (Exception e)
            {
                if (e.Message.Contains("System.Net.Sockets.SocketException"))
                    throw e;
            }
        }

        /// <summary>
        /// Метод проверки нахождения элемента на странице
        /// </summary>
        /// <param name="param"> Параметры для параметризированного XPath </param>
        /// <returns> True/False </returns>
        public bool IsDisplayed(params object[] param)
        {
            return FindElement(param).Displayed;
        }

        /// <summary>
        /// Метод для проверки доступности элемента к изменению пользователем
        /// </summary>
        /// <param name="required"> Ожидаемое состояние: true - элемент скрыт, false - элемент доступен </param>
        /// <param name="param"> Параметры для параметризированного XPath </param>
        /// <returns> True/False </returns>
        public bool IsHided(bool required, params object[] param)
        {
            string req;
            if (required)
            {
                //Положительный результат функции .GetAttribute - проверяет наличие атрибута "disabled"
                req = "true";
            }
            else
            {
                //Отрицательный Ррзультат функции .GetAttribute - проверяет наличие атрибута "disabled"
                req = null;
            }

            var elements = FindElements(param);
            if (elements.Count == 0)
            {
                throw new Exception("Элемент(ы) не был найден");
            }

            foreach (var el in elements)
            {
                //При соответствии ожидаемому результату - продолжаем
                if (el.GetAttribute("disabled") == req)
                {
                    continue;
                }
                //Если один из элементов не скрыт - возваращаем обратное ожидаемому
                else
                {
                    return !required;
                }
            }
            return required;
        }
    }
}

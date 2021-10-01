using CarAddingApplication.WebDriverActions.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAddingApplication.WebDriverActions
{
    public class WDActions
    {
        IWebDriver webDriver;
        public WDActions(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        public void Action()
        {
            Autorization();
            AdminPage.BtnCars.Click();
            CarsPage.BtnAddCar.Click();
            var wd = new CarAddingActions(webDriver);
        }

        private void Autorization()
        {
            AutorizationPage.InputName.FindElement().SendKeys(Config.AdminLogin);
            AutorizationPage.InputPassword.FindElement().SendKeys(Config.AdminPassword);
            AutorizationPage.BtnLogIn.Click();
        }
    }
}

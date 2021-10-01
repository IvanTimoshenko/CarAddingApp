using CarAddingApplication.Elements;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAddingApplication.WebDriverActions
{
    public class CarAddingActions
    {
        public CarAddingActions(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        IWebDriver webDriver;

        static Input InputCarNumber { get; } = new Input("//input[@id='number']");
        static Select SelectBodyType { get; } = new Select("//select[@id='form_factor']");
        static Select SelectClassOfCar { get; } = new Select("//select[@id='class']");
        static Select SelectCompany { get; } = new Select("//select[@id='company_id']");
        static Input InputModel { get; } = new Input("//input[@id='model']");
        static Input InputBrand { get; } = new Input("//input[@id='brand']");
    }
}

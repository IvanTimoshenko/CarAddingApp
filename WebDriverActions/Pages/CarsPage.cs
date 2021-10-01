using CarAddingApplication.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAddingApplication.WebDriverActions.Pages
{
    public class CarsPage
    {
        public static Button BtnAddCar { get; } = new Button("//div/a[@href='/cars/add']");
    }
}

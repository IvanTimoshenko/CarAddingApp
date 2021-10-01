using CarAddingApplication.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAddingApplication.WebDriverActions.Pages
{
    public class AdminPage
    {
        public static Button BtnCars { get; } = new Button("//ul/li/a[@href='/cars']");
    }
}

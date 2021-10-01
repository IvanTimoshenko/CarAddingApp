using CarAddingApplication.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAddingApplication.WebDriverActions.Pages
{
    public class AutorizationPage
    {
        /// <summary> Поле ввода логина </summary>
        public static Input InputName { get; } = new Input("//input[@id='name']");
        /// <summary> Поле ввода пароля </summary>
        public static Input InputPassword { get; } = new Input("//input[@id='password']");
        /// <summary> Кнопка "Войти" </summary>
        public static Button BtnLogIn { get; } = new Button("//button[@type='submit']");
    }
}

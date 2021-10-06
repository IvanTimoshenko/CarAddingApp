using CarAddingApplication.ObjectCar;
using CarAddingApplication.Utils;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAddingApplication
{
    class Program
    {
        public static TimeSpan WebDriverWait { get; set; } = TimeSpan.FromSeconds(20);

        /// <summary> Текущий WebDriver </summary>
        public static WebDriver WebDriver { get; set; }

        static void Main(string[] args)
        {
            
            KillAllExcelProcesses();
            
            var ex = new ExcelReader();
            int i = 1;
            foreach (var el in ex.CarList)
            {
                Console.Write($"{i}. ");
                foreach (var elem in el)
                {
                    Console.Write($"{elem}, ");
                }
                Console.WriteLine();
                i++;
            }
            KillAllExcelProcesses();
            List<Car> cars = new List<Car>();
            foreach (var el in ex.CarList)
            {
                cars.Add(new Car(el));
            }
            int a = 0;
        }

        static void KillAllExcelProcesses()
        {
            foreach (Process proc in Process.GetProcessesByName("EXCEL"))
            {
                try
                {
                    proc.Kill();
                }
                catch (Exception) { }
            }
        }
    }
}

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
        static void Main(string[] args)
        {
            KillAllExcelProcesses();
            
            var ex = new ExcelReader();
            var result = ex.GetAllValues();
            if (result.Item2)
            {
                KillAllExcelProcesses();
                return;
            }
            foreach (var el in result.Item1)
            {
                foreach (var elem in el)
                {
                    Console.Write($"{elem}, ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("aaaaa");
            Console.ReadKey();
            KillAllExcelProcesses();
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

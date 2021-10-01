using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAddingApplication.Messages
{
    public class ExcelReaderMessages
    {
        private string EmptyCellError = "Список пустых строк, исключенных из списка на добавление: ";
        private string EndOfDocumentMes = "Обнаружен конец документа. Количество взятых строк: ";

        public void PrintEmptyCellErrorMessage(List<List<string>> row)
        {
            Console.WriteLine($"{EmptyCellError}");
            Console.Write("|");
            foreach (var el in row)
            {
                foreach (var elem in el)
                {
                    if (elem == string.Empty)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"ПУСТОТА |");
                    }
                    Console.ResetColor();
                    Console.Write($"{elem} |");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void PrintEndOfDocumentMes(int row)
        {
            Console.WriteLine($"{EndOfDocumentMes}{row - Config.RowStartValue}");
        }
    }
}

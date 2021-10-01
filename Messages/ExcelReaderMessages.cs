using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarAddingApplication.Messages
{
    public class ExcelReaderMessages
    {
        private string EmptyCellError = "Найдена пустая ячейка, операция прервана. Адрес ячейки: ";
        private string EndOfDocumentMes = "Обнаружен конец документа. Количество взятых строк: ";

        public void PrintEmptyCellErrorMessage(int row, int column)
        {
            Console.WriteLine($"{EmptyCellError}{row}.{column}");
        }

        public void PrintEndOfDocumentMes(int row)
        {
            Console.WriteLine($"{EndOfDocumentMes}{row - Config.RowStartValue}");
        }
    }
}

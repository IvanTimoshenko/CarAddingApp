using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarAddingApplication.Messages;
using Microsoft.Office.Interop.Excel;

namespace CarAddingApplication
{
    public class ExcelReader
    {
        Application xlApp;
        Workbook xlWorkbook;
        Worksheet xlWorksheet;
        ExcelReaderMessages Message = new ExcelReaderMessages();

        public ExcelReader()
        {
            xlApp = new Application();
            xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\ivanv\Desktop\CarAdder\test.xlsx");
            xlWorksheet = (Worksheet)xlWorkbook.Sheets[1];
            // Unhide All Cells and clear formats
            xlWorksheet.Columns.ClearFormats();
            xlWorksheet.Rows.ClearFormats();
        }
        
        public Tuple<List<List<string>>, bool> GetAllValues()
        {
            var list = new List<List<string>>();
            bool stopSearching = false;
            bool emptyCellDetected = false;
            for (int row = Config.RowStartValue; !stopSearching && !emptyCellDetected ; row++)
            {
                var temp = new List<string>();
                for (int column = Config.ColumnStartValue; column <= Config.ColumnEndValue; column++)
                {
                    var value = GetValueOfCell(row, column);
                    if (value.Item1 == string.Empty)
                    {
                        Message.PrintEmptyCellErrorMessage(row, column);
                        emptyCellDetected = true;
                        break;
                    }
                    if (value.Item2)
                    {
                        Message.PrintEndOfDocumentMes(row);
                        stopSearching = true;
                        break;
                    }
                    temp.Add(value.Item1);
                }
                list.Add(temp);
            }
            return new Tuple<List<List<string>>, bool>(list, emptyCellDetected);
        }

        private Tuple<string, bool> GetValueOfCell(int row, int column)
        {
            string value = string.Empty;
            bool end = false;
            try
            {
                value = (xlWorksheet.Cells[row, column] as Range).Value2.ToString();
                return new Tuple<string, bool>(value, end);
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
            {
                if (column == Config.ColumnStartValue)
                {
                    return new Tuple<string, bool>(value, !end);
                }
            }
            value = string.Empty;
            return new Tuple<string, bool>(value, !end);
        }

        private bool IsDocumentEnded(int row, int column)
        {
            bool valueFound = false;
            for (int i = Config.ColumnStartValue; i < Config.ColumnEndValue && valueFound == false; i++)
            {
                try
                {
                    (xlWorksheet.Cells[row, column] as Range).Value2.ToString();
                    valueFound = true;
                    break;
                }
                catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
                {
                    continue;
                }
            }
            if (valueFound)
            {
                return false;
            }
            return true;
        }
    }
}

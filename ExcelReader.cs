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
        static Application xlApp;
        static Workbook xlWorkbook;
        static Worksheet xlWorksheet;
        static ExcelReaderMessages Message = new ExcelReaderMessages();
        public int RangeOfDocument { get; private set; }

        public List<int> IndexesOfBadRows { get; private set; } = new List<int>();

        public List<List<string>> BadRows { get; private set; } = new List<List<string>>();

        public List<List<string>> CarList { get; private set; } = new List<List<string>>();

        public ExcelReader()
        {
            xlApp = new Application();
            xlWorkbook = xlApp.Workbooks.Open(@"C:\Users\ivanv\Desktop\CarAdder\test.xlsx");
            xlWorksheet = (Worksheet)xlWorkbook.Sheets[1];
            // Unhide All Cells and clear formats
            xlWorksheet.Columns.ClearFormats();
            xlWorksheet.Rows.ClearFormats();

            GetRangeOfDocument();
            Message.PrintEmptyCellErrorMessage(BadRows);
            GetAllValues();
            int a = 0;
        }
        
        private void GetAllValues()
        {
            for (int row = Config.RowStartValue; row < RangeOfDocument + Config.RowStartValue; row++)
            {
                bool badRow = false;
                for (int i = 0; i < BadRows.Count; i++)
                {
                    if (row == IndexesOfBadRows[i])
                    {
                        badRow = true;
                    }
                    
                }
                if (badRow == true)
                {
                    continue;
                }
                var temp = new List<string>();
                for (int column = Config.ColumnStartValue; column <= Config.ColumnEndValue; column++)
                {
                    temp.Add(GetValueOfCell(row, column));
                }
                CarList.Add(temp);
            }
            Message.PrintEndOfDocumentMes(RangeOfDocument);
        }

        private List<string> GetSpecificRow(int row)
        {
            List<string> values = new List<string>();
            for (int column = Config.ColumnStartValue; column <= Config.ColumnEndValue; column++)
            {
                string value = string.Empty;
                try
                {
                    value = (xlWorksheet.Cells[row, column] as Range).Value2.ToString();
                } catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException) { };
                values.Add(value);
            }
            return values;
        }


        private string GetValueOfCell(int row, int column)
        {
            try
            {
                return (xlWorksheet.Cells[row, column] as Range).Value2.ToString();
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
            {
                throw;
            }
        }

        private bool IsDocumentEnded(int row)
        {
            bool valueFound = false;
            for (int i = Config.ColumnStartValue; i < Config.ColumnEndValue && valueFound == false; i++)
            {
                try
                {
                    (xlWorksheet.Cells[row, i] as Range).Value2.ToString();
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

        private void GetRangeOfDocument()
        {
            int rows = 0;
            bool stopSearching = false;
            for (int row = Config.RowStartValue; stopSearching == false; row++)
            {
                for (int column = Config.ColumnStartValue; column <= Config.ColumnEndValue && stopSearching == false; column++)
                {
                    try
                    {
                        (xlWorksheet.Cells[row, column] as Range).Value2.ToString();
                    }
                    catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
                    {
                        switch (IsDocumentEnded(row))
                        {
                            case true:
                                stopSearching = true;
                                break;
                            case false:
                                IndexesOfBadRows.Add(row);
                                BadRows.Add(GetSpecificRow(row));
                                break;
                        }
                    }
                }
                if (stopSearching)
                {
                    RangeOfDocument = rows;
                    break;
                }
                rows++;
            }
        }
    }
}

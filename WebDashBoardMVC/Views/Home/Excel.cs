using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;
using static WebDashBoardMVC.Views.Home.Excel;


namespace WebDashBoardMVC.Views.Home
{
    public class Excel: IDisposable
    {
        _Application excel = new _Excel.Application();
        private string file_path;
        private Workbook workbook;
        private Worksheet worksheet;
        public Excel(string file_path, int worksheet_index)
        {
            this.file_path = file_path;
            workbook = excel.Workbooks.Open(file_path);
            worksheet = workbook.Worksheets[worksheet_index];
        }

        public Workbook Workbook { get { return workbook; } private set { } }
        public Worksheet Worksheet { get { return worksheet; } set { worksheet = value; } }

        public string ReadWorksheetsCell(Worksheet worksheet, int row_index, int column_index)
        {
            var info = worksheet.Rows.Cells[row_index, column_index].Value;
            if (info == null)
            {
                return "";
            }
            return info.ToString();
        }

        public List<string> ReadWorksheetsRow(Worksheet worksheet, int row_index)
        {
            int columns = 7;
            List<string> data = new List<string>();
            for (int i = 0; i < columns; i++)
            {
                var value = ReadWorksheetsCell(worksheet, row_index, i + 1);
                data.Add(value);
            }
            return data;
        }

        public void GetInfoByDate(DateTime date)
        {

        }

        public void Dispose()
        {
            try
            {
                Workbook.Close(file_path);
                this.excel.Quit();

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.Office.Interop.Excel;

namespace WebDashBoardMVC.Models
{
    public class RecordDbInitializer: DropCreateDatabaseAlways<RecordContext>
    {
        protected override void Seed(RecordContext context)
        {
            string path = @"C:\Users\Михаил\Desktop\Проект.xlsx";
            Excel excel = new Excel(path, 1);
            var worksheet = excel.Worksheet;
            for (int j = 1; j < 2500; j++)
            {
                var row_data = excel.ReadWorksheetsRow(worksheet, j + 1);
                if (row_data[0] == "")
                {
                    continue;
                }
                string office = row_data[0];
                string name = row_data[1];
                string date = row_data[2];
                string clients = row_data[3];
                string calls = row_data[4];
                string meets = row_data[5];
                context.Records.Add(new Record { OfficeName = office, EmployeName = name, Date = Convert.ToDateTime(date).Date, ClientsNumber = Convert.ToInt32(clients), ClientsCalls = Convert.ToInt32(calls), ClientsMeets = Convert.ToInt32(meets) });
            }
            excel.Dispose();
            base.Seed(context);
        }
    }
}

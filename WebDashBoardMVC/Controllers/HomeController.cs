using Microsoft.AspNetCore.Mvc;
using Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using WebDashBoardMVC.Models;

namespace WebDashBoardMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RecordContext _context = new();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            IEnumerable<Record> records = _context.Records;
            ViewBag.Records = records;
            return View(records);
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public IActionResult BuildGraphic(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                // Возвращаем пустой результат, или выполняем другое действие по вашему усмотрению
                return View(new List<Record>());
            }

            // Выполняем поиск в базе данных
            IEnumerable<Record> records = _context.Records.Where(r => r.EmployeName.Contains(searchString)).ToList();
            ViewBag.Recs = records;
            var meetsRecords = records.GroupBy(record => new { record.Date, record.EmployeName }).Select(record => new
            {
                EmployeName = record.Key.EmployeName,
                Date = record.Key.Date,
                Meets = record.Sum(r => r.ClientsMeets)
            }).ToArray();
            var callsRecords = records.GroupBy(record => new { record.Date, record.EmployeName }).Select(record => new
            {
                EmployeName = record.Key.EmployeName,
                Date = record.Key.Date,
                ClientCalls = record.Sum(r => r.ClientsCalls)
            }).ToArray();
            var clientsRecords = records.GroupBy(record => new { record.Date, record.EmployeName }).Select(record => new
            {
                EmployeName = record.Key.EmployeName,
                Date = record.Key.Date,
                ClientNumber = record.Sum(r => r.ClientsNumber)
            }).ToArray();

            // Списки для хранения соответсвутющих значений
            var meets = new List<double>();
            var calls = new List<double>();
            var clients = new List<double>();
            var data1 = new List<double>();
            var data2 = new List<double>();
            var data3 = new List<double>();

            // Добавляем соответствующие значения в списки
            foreach (var item in meetsRecords)
            {
                meets.Add(item.Meets);
            }
            foreach (var item in callsRecords)
            {
                calls.Add(item.ClientCalls);
            }
            foreach (var item in clientsRecords)
            {
                clients.Add(item.ClientNumber);
            }
            
            // Вычисляем результаты и заполняем списки
            for (int i = 0; i < calls.Count; i++)
            {
                if (meets[i] != 0 && calls[i] != 0)
                {
                    data1.Add(meets[i] / calls[i] * 100);
                }
                else
                {
                    data1.Add(0);
                }

                if (clients[i] != 0 && calls[i] != 0)
                {
                    data2.Add(calls[i] / clients[i] * 100);
                }
                else
                {
                    data2.Add(0);
                }

                if (clients[i] != 0 && calls[i] != 0)
                {
                    data3.Add(clients[i] / calls[i] * 100);
                }
                else
                {
                    data3.Add(0);
                }
            }
            
            // Преобразуем дату из числовых значений в символьные
            var date = callsRecords.Select(record => record.Date.Month).ToArray();
            var dateByMonth = new List<string>();
            foreach (var item in date)
            {
                switch (item)
                {
                    case 1:
                        dateByMonth.Add("Январь");
                        break;
                    case 2:
                        dateByMonth.Add("Февраль");
                        break;
                    case 3:
                        dateByMonth.Add("Март");
                        break;
                    case 4:
                        dateByMonth.Add("Апрель");
                        break;
                    case 5:
                        dateByMonth.Add("Май");
                        break;
                    case 6:
                        dateByMonth.Add("Июнь");
                        break;
                    case 7:
                        dateByMonth.Add("Июль");
                        break;
                    case 8:
                        dateByMonth.Add("Август");
                        break;
                    case 9:
                        dateByMonth.Add("Сентябрь");
                        break;
                    case 10:
                        dateByMonth.Add("Октябрь");
                        break;
                    case 11:
                        dateByMonth.Add("Ноябрь");
                        break;
                    case 12:
                        dateByMonth.Add("Декабрь");
                        break;
                    default:
                        break;
                }
            }
            
            // Добавляем итоговые результаты во ViewBag
            ViewBag.Date = dateByMonth.ToArray();
            ViewBag.Data1 = data1.ToArray();
            ViewBag.Data2 = data2.ToArray();
            ViewBag.Data3 = data3.ToArray();
            return View(records);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}

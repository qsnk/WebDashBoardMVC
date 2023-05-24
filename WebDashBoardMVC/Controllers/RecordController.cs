using Microsoft.AspNetCore.Mvc;
using WebDashBoardMVC.Models;

namespace WebDashBoardMVC.Controllers
{
    public class RecordController : Controller
    {
        public readonly RecordContext _context;

        public RecordController(RecordContext context) 
        { 
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Record> records = _context.Records;
            ViewBag.Records = records;
            return View();
        }

        public IActionResult Dashboard(string searchString) 
        {
            var recs = _context.Records.Where(rec => rec.EmployeName.ToLower().Contains(searchString.ToLower())).ToList();
            ViewBag.Recs = recs;
            if (recs.Any())
            {
                return View();
            }
            return View();
        }

        public IActionResult BuildGraphic() 
        {
            return View();
        }

        [HttpPost]
        public List<object> GetRecords(string searchString) 
        {
            List<object> data = new List<object>();
            var records = _context.Records.Where(record => record.EmployeName.ToLower().Contains(searchString.ToLower())).ToList();
            data.Add(records);
            return data;
        }
    }
}

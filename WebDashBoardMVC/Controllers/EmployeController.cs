using Microsoft.AspNetCore.Mvc;
using Excel = Microsoft.Office.Interop.Excel;

namespace WebDashBoardMVC.Controllers
{
    public class EmployeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Import() 
        {

            return View();
        }
    }
}

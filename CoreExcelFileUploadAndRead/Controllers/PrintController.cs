using Microsoft.AspNetCore.Mvc;

namespace CoreExcelFileUploadAndRead.Controllers
{
    public class PrintController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            string id = Request.Query["id"];

            return View();
        }
    }
}

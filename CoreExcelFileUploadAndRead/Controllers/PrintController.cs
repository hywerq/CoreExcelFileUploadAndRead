using CoreExcelFileUploadAndRead.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoreExcelFileUploadAndRead.Controllers
{
    public class PrintController : Controller
    {
        private readonly ExcelFileLoader _fileLoader;

        public PrintController(ExcelFileLoader fileLoader)
        {
            _fileLoader = fileLoader;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int.TryParse(Request.Query["id"], out int fileID);

            ViewBag.File = await _fileLoader.LoadFileInfoAsync(fileID);
            ViewBag.Classes = await _fileLoader.LoadFileClassesAsync(fileID);
            ViewBag.ClassGroups = await _fileLoader.LoadFileClassGroupsAsync(fileID);
            ViewBag.BalanceAccounts = await _fileLoader.LoadFileBalanceAccountsAsync(fileID);

            return View();
        }
    }
}

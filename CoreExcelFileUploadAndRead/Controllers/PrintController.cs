﻿using CoreExcelFileUploadAndRead.Database.Entities;
using CoreExcelFileUploadAndRead.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoreExcelFileUploadAndRead.Controllers
{
    public class PrintController : Controller
    {
        private ExcelFileLoader fileLoader;

        private List<ExcelFile> Files { get; }

        public PrintController(ExcelFileLoader fileLoader)
        {
            this.fileLoader = fileLoader;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int.TryParse(Request.Query["id"], out int fileID);

            ViewBag.File = await fileLoader.LoadFileInfoAsync(fileID);
            ViewBag.Classes = await fileLoader.LoadFileClassesAsync(fileID);
            ViewBag.ClassGroups = await fileLoader.LoadFileClassGroupsAsync(fileID);
            ViewBag.BalanceAccounts = await fileLoader.LoadFileBalanceAccountsAsync(fileID);

            return View();
        }
    }
}

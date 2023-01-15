using CoreExcelFileUploadAndRead.Database.Entities;
using CoreExcelFileUploadAndRead.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoreExcelFileUploadAndRead.Controllers
{
    public class FileController : Controller
    {
        private ExcelFileUploader fileUploader; 

        private List<ExcelFile> Files { get; set; }

        public FileController(ExcelFileUploader fileUploader)
        {
            this.fileUploader = fileUploader;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Files = await fileUploader.LoadUploadedFilesListAsync();

            return View(Files);
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] IFormFile file, [FromServices] IWebHostEnvironment hostingEnvironment)
        {
            try
            {
                string fileName = $"{hostingEnvironment.WebRootPath}\\files\\{file.FileName}";
                using (FileStream fileStream = System.IO.File.Create(fileName))
                {
                    file.CopyTo(fileStream);
                    fileStream.Flush();
                }

                await fileUploader.GetExcelFileDataAsync(file.FileName);

                ViewBag.SuccessMessage = "Added successfully";
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error: " + ex.Message;
            }
            finally
            {
                Files = await fileUploader.LoadUploadedFilesListAsync();
            }

            return View(Files);
        }
    }
}

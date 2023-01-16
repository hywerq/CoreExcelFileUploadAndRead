using CoreExcelFileUploadAndRead.Database.Entities;
using CoreExcelFileUploadAndRead.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoreExcelFileUploadAndRead.Controllers
{
    public class FileController : Controller
    {
        private readonly ExcelFileUploader _fileUploader; 

        private List<ExcelFile> Files { get; set; }

        public FileController(ExcelFileUploader fileUploader)
        {
            _fileUploader = fileUploader;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            Files = await _fileUploader.LoadUploadedFilesListAsync();

            return View(Files);
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] IFormFile file, [FromServices] IWebHostEnvironment hostingEnvironment)
        {
            //creating folder for files if it doesn't exist
            string dir = $"{hostingEnvironment.WebRootPath}\\files";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            //copying the file to the server, reading it and importing it into the database
            try
            {
                string fileName = Path.Combine(dir, file.FileName);
                using (FileStream fileStream = System.IO.File.Create(fileName))
                {
                    file.CopyTo(fileStream);
                    fileStream.Flush();
                }

                await _fileUploader.GetExcelFileDataAsync(file.FileName);

                ViewBag.SuccessMessage = "Added successfully";
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error: " + ex.Message;
            }
            finally
            {
                Files = await _fileUploader.LoadUploadedFilesListAsync();
            }

            return View(Files);
        }
    }
}

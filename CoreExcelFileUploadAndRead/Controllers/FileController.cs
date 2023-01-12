using CoreExcelFileUploadAndRead.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace CoreExcelFileUploadAndRead.Controllers
{
    public class FileController : Controller
    {
        private readonly DatabaseContext databaseContext;
        private List<ExcelFile> uploadedFilesList = new List<ExcelFile>();

        public FileController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            uploadedFilesList = LoadUploadedFilesList();

            return View(uploadedFilesList);
        }

        [BindProperty]
        public ExcelFile File { get; set; }

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

                File.Name = file.FileName;

                databaseContext.Files.Add(File);
                await databaseContext.SaveChangesAsync();

                ViewBag.SuccessMessage = "Added successfully";
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error: " + ex.Message;
            }
            finally
            {
                uploadedFilesList = LoadUploadedFilesList();
            }

            return View(uploadedFilesList);
        }

        private List<ExcelFile> LoadUploadedFilesList()
        {
            List<ExcelFile> files = new List<ExcelFile>();

            try
            {
                files = databaseContext.Files.ToList();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error: " + ex.Message;
            }

            return files;
        }
    }
}

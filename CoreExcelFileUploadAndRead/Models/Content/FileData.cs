namespace CoreExcelFileUploadAndRead.Models.Content
{
	public class FileData
	{
        public List<ExcelFile> Files { get; set; } = new();
        public List<Class> Classes { get; set; } = new();
        public List<ClassGroup> ClassGroups { get; set; } = new();
        public List<BalanceAccount> BalanceAccounts { get; set; } = new();
    }
}

namespace CoreExcelFileUploadAndRead.Database.Entities
{
    public class FileData
    {
        public int Id { get; set; }
        public int? ExcelFileId { get; set; }
        public ExcelFile? ExcelFile { get; set; }
        public int? ClassId { get; set; }
        public FileClass? Class { get; set; }
        public int? ClassGroupId { get; set; }
        public ClassGroup? ClassGroup { get; set; }
        public int? BalanceAccountId { get; set; }
        public BalanceAccount? BalanceAccount { get; set; }
    }
}

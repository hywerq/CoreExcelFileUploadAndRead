namespace CoreExcelFileUploadAndRead.Database.Entities
{
    public class FileData
    {
        public int Id { get; set; }
        public int ExcelFileId { get; set; }
        //public ExcelFile ExcelFile { get; set; } = new();
        public int ClassId { get; set; }
       // public Class Class { get; set; } = new();
        public int ClassGroupId { get; set; }
        //public ClassGroup ClassGroup { get; set; } = new();
        public int BalanceAccountId { get; set; }
        //public BalanceAccount BalanceAccount { get; set; } = new ();
    }
}

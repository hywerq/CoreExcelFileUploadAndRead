namespace CoreExcelFileUploadAndRead.Database.Entities
{
    public class FileClass
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public decimal OpeningBalanceActive { get; set; }
        public decimal OpeningBalancePassive { get; set; }
        public decimal TurnoverDebit { get; set; }
        public decimal TurnoverCredit { get; set; }
        public decimal ClosingBalanceActive { get; set; }
        public decimal ClosingBalancePassive { get; set; }

        public List<FileData> FileDatas { get; set; } = new();
    }
}

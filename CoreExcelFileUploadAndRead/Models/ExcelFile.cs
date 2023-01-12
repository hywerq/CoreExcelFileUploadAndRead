namespace CoreExcelFileUploadAndRead.Models
{
	public class ExcelFile
	{
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Title { get; set; } = "";
        public string BankName { get; set; } = "";
        public string Subject { get; set; } = "";
        public string Currency { get; set; } = "";
        public DateTime PrintTime { get; set; }
        public DateTime UploadTime { get; set; }
        public DateTime PeriodStart { get; set; } 
        public DateTime PeriodEnd { get; set; }
        public decimal OpeningBalanceActive { get; set; }
        public decimal OpeningBalancePassive { get; set; }
        public decimal TurnoverDebit { get; set; }
        public decimal TurnoverCredit { get; set; }
        public decimal ClosingBalanceActive { get; set; }
        public decimal ClosingBalancePassive { get; set; }
    }
}

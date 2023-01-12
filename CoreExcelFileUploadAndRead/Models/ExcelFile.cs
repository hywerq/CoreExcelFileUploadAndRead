namespace CoreExcelFileUploadAndRead.Models
{
	public class ExcelFile
	{
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Title { get; set; } = "";
        public string Subject { get; set; } = "";
        public string Currency { get; set; } = "";
        public DateTime PrintTime { get; set; }
        public DateTime UploadTime { get; set; }
        public DateTime PeriodStart { get; set; } 
        public DateTime PeriodEnd { get; set; }
        public double OpeningBalanceActive { get; set; }
        public double OpeningBalancePassive { get; set; }
        public double TurnoverDebit { get; set; }
        public double TurnoverCredit { get; set; }
        public double ClosingBalanceActive { get; set; }
        public double ClosingBalancePassive { get; set; }
    }
}

namespace CoreExcelFileUploadAndRead.Database.Entities
{
    public class BalanceAccount
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public decimal OpeningBalanceActive { get; set; }
        public decimal OpeningBalancePassive { get; set; }
        public decimal TurnoverDebit { get; set; }
        public decimal TurnoverCredit { get; set; }
        public decimal ClosingBalanceActive { get; set; }
        public decimal ClosingBalancePassive { get; set; }
        //public FileData Data { get; set; } = new();
    }
}

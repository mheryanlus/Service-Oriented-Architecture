namespace ServiceOrientedArchitecture.Common.Models
{
    public class TransactionHistoryDto
    {
        public string UserName { get; set; }

        public float Ammount { get; set; }

        public string TransactionType { get; set; }

        public DateTime? TransactionDate { get; set; }
    }
}

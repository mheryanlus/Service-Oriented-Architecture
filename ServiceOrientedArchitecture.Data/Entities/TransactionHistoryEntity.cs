using System.ComponentModel.DataAnnotations;

namespace ServiceOrientedArchitecture.Data.Entities;

public class TransactionHistoryEntity
{
    [Key]
    public long TransactionId { get; set; }

    public int UserId { get; set; }

    public UserEntity User { get; set; }

    public float Ammount { get; set; }

    public string TransactionType { get; set; }

    public DateTime? TransactionDate { get; set;}
}

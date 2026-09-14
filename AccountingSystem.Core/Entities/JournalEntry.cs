using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingSystem.Core.Entities
{
    /// <summary>
    /// دفتر اليومية (Journal)
    /// </summary>
    public class JournalEntry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string EntryNumber { get; set; }

        public DateTime EntryDate { get; set; } = DateTime.Now;

        [StringLength(500)]
        public string Description { get; set; }

        public int DebitAccountId { get; set; }
        [ForeignKey("DebitAccountId")]
        public virtual GeneralLedgerAccount DebitAccount { get; set; }

        public int CreditAccountId { get; set; }
        [ForeignKey("CreditAccountId")]
        public virtual GeneralLedgerAccount CreditAccount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
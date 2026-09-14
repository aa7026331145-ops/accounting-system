using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccountingSystem.Core.Enums;

namespace AccountingSystem.Core.Entities
{
    /// <summary>
    /// الحسابات المالية
    /// </summary>
    public class GeneralLedgerAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string AccountCode { get; set; }

        [Required]
        [StringLength(200)]
        public string AccountName { get; set; }

        public AccountType AccountType { get; set; } // أصول، خصوم، رأس مال، إيرادات، مصروفات

        [StringLength(500)]
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; } = 0; // الرصيد الحالي

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }

        // Relations
        public virtual ICollection<JournalEntry> JournalEntries { get; set; } = new List<JournalEntry>();
    }
}
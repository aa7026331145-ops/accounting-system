using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccountingSystem.Core.Enums;

namespace AccountingSystem.Core.Entities
{
    /// <summary>
    /// حركات المخزون
    /// </summary>
    public class InventoryMovement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string MovementNumber { get; set; }

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        public MovementType MovementType { get; set; } // وارد أو صادر

        [Column(TypeName = "decimal(18,2)")]
        public decimal Quantity { get; set; }

        [StringLength(500)]
        public string Reference { get; set; } // رقم الفاتورة أو المستند المرجعي

        [StringLength(500)]
        public string Notes { get; set; }

        public DateTime MovementDate { get; set; } = DateTime.Now;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
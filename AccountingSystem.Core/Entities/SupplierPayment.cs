using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccountingSystem.Core.Enums;

namespace AccountingSystem.Core.Entities
{
    /// <summary>
    /// مدفوعات الموردين
    /// </summary>
    public class SupplierPayment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string PaymentNumber { get; set; }

        public int SupplierId { get; set; }
        [ForeignKey("SupplierId")]
        public virtual Supplier Supplier { get; set; }

        public int? PurchaseInvoiceId { get; set; }
        [ForeignKey("PurchaseInvoiceId")]
        public virtual PurchaseInvoice PurchaseInvoice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public PaymentMethod PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;

        [StringLength(500)]
        public string Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
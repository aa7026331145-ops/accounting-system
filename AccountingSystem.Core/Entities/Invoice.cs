using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AccountingSystem.Core.Enums;

namespace AccountingSystem.Core.Entities
{
    /// <summary>
    /// فواتير المبيعات
    /// </summary>
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string InvoiceNumber { get; set; }

        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        public DateTime InvoiceDate { get; set; } = DateTime.Now;
        public DateTime? DueDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SubTotal { get; set; } // المجموع الفرعي

        [Column(TypeName = "decimal(18,2)")]
        public decimal Discount { get; set; } = 0; // الخصم

        [Column(TypeName = "decimal(18,2)")]
        public decimal Tax { get; set; } = 0; // الضريبة

        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; } // الإجمالي

        [Column(TypeName = "decimal(18,2)")]
        public decimal PaidAmount { get; set; } = 0; // المبلغ المدفوع

        public InvoiceStatus Status { get; set; } = InvoiceStatus.Draft; // الحالة

        [StringLength(500)]
        public string Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        // Relations
        public virtual ICollection<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
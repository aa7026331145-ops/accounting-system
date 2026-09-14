using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingSystem.Core.Entities
{
    /// <summary>
    /// عملاء النظام
    /// </summary>
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; } // رمز العميل

        [Required]
        [StringLength(200)]
        public string Name { get; set; } // اسم العميل

        [StringLength(20)]
        public string Phone { get; set; } // رقم الهاتف

        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; } // البريد الإلكتروني

        [StringLength(500)]
        public string Address { get; set; } // العنوان

        [StringLength(20)]
        public string TaxId { get; set; } // الرقم الضريبي

        [Column(TypeName = "decimal(18,2)")]
        public decimal CreditLimit { get; set; } = 0; // حد الائتمان

        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; } = 0; // الرصيد

        public bool IsActive { get; set; } = true; // هل العميل نشط

        [StringLength(500)]
        public string Notes { get; set; } // ملاحظات

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        // Relations
        public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
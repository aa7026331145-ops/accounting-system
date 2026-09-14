namespace AccountingSystem.Core.Enums
{
    /// <summary>
    /// حالات الفاتورة
    /// </summary>
    public enum InvoiceStatus
    {
        Draft = 0,        // مسودة
        Posted = 1,       // مسجلة
        Confirmed = 2,    // مؤكدة
        PartiallyPaid = 3, // مدفوعة جزئياً
        FullyPaid = 4,    // مدفوعة بالكامل
        Cancelled = 5     // ملغاة
    }
}
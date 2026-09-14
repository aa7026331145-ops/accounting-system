namespace AccountingSystem.Core.Enums
{
    /// <summary>
    /// حالات طلبات الشراء
    /// </summary>
    public enum PurchaseStatus
    {
        Draft = 0,          // مسودة
        Confirmed = 1,      // مؤكدة
        PartiallyReceived = 2, // استقبال جزئي
        FullyReceived = 3,   // استقبال كامل
        PartiallyPaid = 4,   // مدفوعة جزئياً
        FullyPaid = 5,       // مدفوعة بالكامل
        Cancelled = 6        // ملغاة
    }
}
namespace AccountingSystem.Core.Enums
{
    /// <summary>
    /// طرق الدفع
    /// </summary>
    public enum PaymentMethod
    {
        Cash = 0,           // نقد
        Check = 1,          // شيك
        BankTransfer = 2,   // تحويل بنكي
        CreditCard = 3,     // بطاقة ائتمان
        OnlinePayment = 4   // دفع إلكتروني
    }
}
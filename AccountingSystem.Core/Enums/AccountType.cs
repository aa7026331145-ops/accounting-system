namespace AccountingSystem.Core.Enums
{
    /// <summary>
    /// أنواع الحسابات المالية
    /// </summary>
    public enum AccountType
    {
        Assets = 0,         // أصول
        Liabilities = 1,    // خصوم
        Equity = 2,         // رأس المال
        Revenue = 3,        // إيرادات
        Expenses = 4        // مصروفات
    }
}
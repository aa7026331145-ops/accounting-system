namespace AccountingSystem.Core.Enums
{
    /// <summary>
    /// أنواع حركات المخزون
    /// </summary>
    public enum MovementType
    {
        In = 0,         // وارد (إضافة)
        Out = 1,        // صادر (خروج)
        Adjustment = 2  // تعديل
    }
}
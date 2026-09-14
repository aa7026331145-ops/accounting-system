# نظام محاسبي متكامل - Accounting Management System (C# ASP.NET Core)

نظام محاسبي شامل ومتكامل مبني بـ C# و ASP.NET Core، يشبه Odoo و Majed Soft

## الميزات الرئيسية

### 1. إدارة البيانات الأساسية
- ✅ إدارة العملاء (Customers)
- ✅ إدارة الموردين (Suppliers)
- ✅ إدارة المنتجات (Products)
- ✅ إدارة الفئات (Categories)
- ✅ إدارة الوحدات القياسية (Units)

### 2. المبيعات
- ✅ إنشاء الفواتير (Sales Invoices)
- ✅ إدارة طلبات المبيعات (Sales Orders)
- ✅ تتبع المدفوعات
- ✅ الخصومات والعروض
- ✅ الضرائب والرسوم

### 3. الشراء
- ✅ إنشاء فواتير الشراء (Purchase Invoices)
- ✅ إدارة طلبات الشراء (Purchase Orders)
- ✅ متابعة المستحقات للموردين
- ✅ تتبع مستندات الشراء

### 4. إدارة المخزون
- ✅ تتبع كميات المنتجات
- ✅ تسجيل الصادر والوارد
- ✅ تقارير المخزون
- ✅ حركات المخزون

### 5. الحسابات المالية
- ✅ دفتر اليومية (General Journal)
- ✅ الميزانية العمومية (Balance Sheet)
- ✅ قائمة الدخل (Income Statement)
- ✅ كشوف الحسابات (Trial Balance)

### 6. التقارير والإحصائيات
- ✅ تقارير المبيعات
- ✅ تقارير الشراء
- ✅ تقارير العملاء والموردين
- ✅ التقارير المالية
- ✅ تقارير الأداء

## التقنيات المستخدمة

- **Framework:** ASP.NET Core 7.0+
- **Database:** SQL Server / PostgreSQL
- **ORM:** Entity Framework Core
- **API:** RESTful API with Swagger
- **Authentication:** JWT Tokens
- **Frontend:** Razor Pages / Blazor / Angular
- **Logging:** Serilog
- **Caching:** Redis

## البنية الأساسية للمشروع

```
AccountingSystem/
├── AccountingSystem.Web/              # ASP.NET Core Web Project
│   ├── Controllers/                   # API Controllers
│   ├── Views/                         # Razor Views
│   ├── Models/                        # View Models
│   └── wwwroot/                       # Static Files (CSS, JS)
│
├── AccountingSystem.Core/             # Business Logic Layer
│   ├── Entities/                      # Domain Models
│   ├── Services/                      # Business Services
│   ├── Interfaces/                    # Service Interfaces
│   ├── Dto/                           # Data Transfer Objects
│   └── Constants/                     # Constants & Enums
│
├── AccountingSystem.Data/             # Data Access Layer
│   ├── DbContext/                     # Entity Framework DbContext
│   ├── Repositories/                  # Repository Pattern
│   ├── Migrations/                    # Database Migrations
│   └── Configurations/                # Entity Configurations
│
├── AccountingSystem.Tests/            # Unit Tests
│   ├── ServiceTests/
│   ├── RepositoryTests/
│   └── ControllerTests/
│
└── AccountingSystem.sln               # Solution File
```

## المتطلبات

- **.NET 7.0 SDK** أو أحدث
- **SQL Server 2019** أو PostgreSQL 12+
- **Visual Studio 2022** أو VS Code

## التثبيت والتشغيل

### 1. استنساخ المستودع
```bash
git clone https://github.com/aa7026331145-ops/accounting-system.git
cd accounting-system
```

### 2. تثبيت المكتبات
```bash
dotnet restore
```

### 3. تكوين قاعدة البيانات
```bash
# تحديث ملف appsettings.json بالبيانات الصحيحة
# ثم تطبيق الـ Migrations
dotnet ef database update
```

### 4. تشغيل المشروع
```bash
dotnet run
```

الدخول إلى: `https://localhost:5001`

## الهيكل النموذجي للـ Entities

### Customer (العميل)
```csharp
public class Customer
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
    public decimal CreditLimit { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

### Invoice (الفاتورة)
```csharp
public class Invoice
{
    public int Id { get; set; }
    public string InvoiceNumber { get; set; }
    public int CustomerId { get; set; }
    public DateTime InvoiceDate { get; set; }
    public decimal Amount { get; set; }
    public decimal Tax { get; set; }
    public decimal Total { get; set; }
    public InvoiceStatus Status { get; set; }
    public List<InvoiceItem> Items { get; set; }
}
```

## API Endpoints

### العملاء
```
GET    /api/customers              - الحصول على قائمة العملاء
POST   /api/customers              - إنشاء عميل جديد
GET    /api/customers/{id}         - الحصول على تفاصيل العميل
PUT    /api/customers/{id}         - تحديث بيانات العميل
DELETE /api/customers/{id}         - حذف العميل
```

### الفواتير
```
GET    /api/invoices               - الحصول على قائمة الفواتير
POST   /api/invoices               - إنشاء فاتورة جديدة
GET    /api/invoices/{id}          - الحصول على تفاصيل الفاتورة
PUT    /api/invoices/{id}          - تحديث الفاتورة
DELETE /api/invoices/{id}          - حذف الفاتورة
```

### التقارير
```
GET    /api/reports/sales          - تقرير المبيعات
GET    /api/reports/purchases      - تقرير الشراء
GET    /api/reports/balance-sheet  - الميزانية العمومية
GET    /api/reports/income-stmt    - قائمة الدخل
```

## المساهمة

يرحب المشروع بالمساهمات! يرجى:
1. Fork المستودع
2. إنشاء فرع للميزة الجديدة (`git checkout -b feature/AmazingFeature`)
3. Commit التغييرات (`git commit -m 'Add some AmazingFeature'`)
4. Push للفرع (`git push origin feature/AmazingFeature`)
5. فتح Pull Request

## الترخيص

MIT License

---

**تم الإنشاء بواسطة:** aa7026331145-ops
**اللغة:** C# / ASP.NET Core

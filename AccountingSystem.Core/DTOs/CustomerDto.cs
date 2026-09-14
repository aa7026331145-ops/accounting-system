namespace AccountingSystem.Core.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string TaxId { get; set; }
        public decimal CreditLimit { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class CreateCustomerDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string TaxId { get; set; }
        public decimal CreditLimit { get; set; }
        public string Notes { get; set; }
    }

    public class UpdateCustomerDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string TaxId { get; set; }
        public decimal CreditLimit { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }
    }
}
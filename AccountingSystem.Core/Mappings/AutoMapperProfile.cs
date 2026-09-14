using AutoMapper;
using AccountingSystem.Core.DTOs;
using AccountingSystem.Core.Entities;
using AccountingSystem.Core.Enums;

namespace AccountingSystem.Core.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Customer Mappings
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<CreateCustomerDto, Customer>();
            CreateMap<UpdateCustomerDto, Customer>();

            // Product Mappings
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.UnitName, opt => opt.MapFrom(src => src.Unit.Name));
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();

            // Invoice Mappings
            CreateMap<Invoice, InvoiceDto>()
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
                .ForMember(dest => dest.RemainingAmount, opt => opt.MapFrom(src => src.Total - src.PaidAmount));
            CreateMap<CreateInvoiceDto, Invoice>();

            // InvoiceItem Mappings
            CreateMap<InvoiceItem, InvoiceItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
            CreateMap<InvoiceItemInputDto, InvoiceItem>();

            // Supplier Mappings
            CreateMap<Supplier, Supplier>().ReverseMap();

            // PurchaseInvoice Mappings
            CreateMap<PurchaseInvoice, PurchaseInvoice>().ReverseMap();

            // Payment Mappings
            CreateMap<Payment, Payment>().ReverseMap();

            // InventoryMovement Mappings
            CreateMap<InventoryMovement, InventoryMovement>().ReverseMap();

            // GeneralLedgerAccount Mappings
            CreateMap<GeneralLedgerAccount, GeneralLedgerAccount>().ReverseMap();
        }
    }
}
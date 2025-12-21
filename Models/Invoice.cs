using System;

namespace Models;

public class Invoice
{
    public string ID { get; set; }
    public DateTime DateCreated { get; set; }
    public string Reference { get; set; }
    public string CompanyName { get; set; }
    public string TargetIDType { get; set; }
    public string TargetLabel { get; set; }
    public string TargetID { get; set; }
    public double Amount { get; set; }

    public double TaxRate { get; set; }
    public string CurrencyCode { get; set; }
    public DateTime DateDue { get; set; }
    public string Status { get; set; }
    public double AmountPaid { get; set; }
    public double Outstanding { get; set; }
    public string PONumber { get; set; }
    public string Address { get; set; }
    public string InvoiceAddress { get; set; }
    public string InvoicePostcode { get; set;}
    public string InvoiceCountry { get; set; }
    public string InvoiceCounty { get; set; }
    public string InvoiceEmail { get; set;}
    public string InvoiceTown { get; set; }
    public List<InvoiceItem> Items { get; set; } = new();
}
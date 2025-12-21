using System;

namespace Models;

public class InvoiceItem
{
    public string ID { get; set; }
    public string InvoiceID { get; set; }
    public string Reference { get; set; }
    public string DynamicLabel { get; set; }
    public decimal? Tax { get; set; }
    public decimal? Amount { get; set; }
    public string SourceID { get; set; }
    public string SourceIDType { get; set; }
}

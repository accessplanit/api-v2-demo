using Models;

namespace Services;

public class InvoiceService
{
    public Invoice? CurrentInvoice { get; private set; }

    public void SetInvoice(Invoice invoice) 
        => CurrentInvoice = invoice;

    public Invoice? GetInvoice() 
        => CurrentInvoice;
}

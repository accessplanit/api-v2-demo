using System;
using Constants;
using Services;

namespace APIClient;

public class AccessPlanitInvoiceItemClient : AccessPlanitAPIClientBase
{
    public AccessPlanitInvoiceItemClient(HttpClient httpClient, StateManagementService stateManagementService) : base(httpClient, stateManagementService)
    {
    }

    protected override APIModule Module => APIModule.InvoiceItem;
}
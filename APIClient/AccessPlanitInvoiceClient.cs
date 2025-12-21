using System;
using Constants;
using Services;

namespace APIClient;

public class AccessPlanitInvoiceClient : AccessPlanitAPIClientBase
{
    public AccessPlanitInvoiceClient(HttpClient httpClient, StateManagementService stateManagementService) : base(httpClient, stateManagementService)
    {
    }

    protected override APIModule Module => APIModule.Invoice;
}

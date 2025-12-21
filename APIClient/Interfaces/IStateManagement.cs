using Services;

namespace APIClient.Interfaces
{
    public interface IStateManagement
    {
        StateManagementService StateManagementService { get; set; }
    }
}

using Microsoft.AspNetCore.SignalR;
using ShopApi.Data;

namespace ShopApi.Hubs
{
    public class ContractsHub: Hub
    {
        public async Task SendPaidContract(Contract contract)
        {
            await Clients.All.SendAsync("ContractPaid", contract);
        }
    }
}

using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ShopApi.Data;
using ShopApi.Hubs;
using Timer = System.Threading.Timer;

namespace ShopApi.Services
{
    public class PaymentService : IHostedService
    {
        private readonly AppDbContext? _dbContext;
        private IHubContext<ContractsHub>? _hubContext;
        private ILogger _logger;
        
        public PaymentService(IServiceProvider provider, ILogger<BackgroundService> logger)
        {
            _logger = logger;
            var scope = provider.CreateScope();
            _dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            _hubContext = scope.ServiceProvider.GetRequiredService<IHubContext<ContractsHub>>();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var timer = new Timer(Pay, null, TimeSpan.Zero, TimeSpan.FromSeconds(3));
        }

        public async void Pay(object? s)
        {
            var activeContracts = await _dbContext.Contracts!
                .Where(c => c.Status == EContractStatus.Active)
                .ToListAsync();

            foreach (var contract in activeContracts)
            {
                await PayContract(contract);
            }
        }

        private async Task PayContract(Contract contract)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7116/api/payment");
            request.Content = JsonContent.Create(new {UserId=1});
            var response = await client.SendAsync(request);
            _logger.LogInformation(response.StatusCode.ToString());
            
            if (response.IsSuccessStatusCode)
            {
                contract.Status = EContractStatus.Paid;
                contract.Amount += 1;
                _dbContext?.Contracts?.Update(contract);
                await _dbContext?.SaveChangesAsync()!;

                await _hubContext!.Clients.All.SendAsync("ContractPaid", contract);
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

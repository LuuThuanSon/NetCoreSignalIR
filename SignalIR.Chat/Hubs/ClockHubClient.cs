using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SignalIR.Common;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SignalIR.Chat.Hubs
{
    public partial class ClockHubClient : IClock, IHostedService
    {
        private readonly ILogger<ClockHubClient> _logger;
        private HubConnection _connection;

        public ClockHubClient(ILogger<ClockHubClient> logger)
        {
            _logger = logger;

            _connection = new HubConnectionBuilder()
                .WithUrl(Strings.HubUrl)
                .Build();

            _connection.On<DateTime>(Strings.Events.TimeSent, ShowTime);
        }

        public Task ShowTime(DateTime currentTime)
        {
            //_logger.LogInformation("Clients receive from Server : {CurrentTime}", currentTime.ToShortTimeString());

            return Task.CompletedTask;
        }

        public Task SendMessage(string user, string message)
        {
            return Task.CompletedTask;
        }

        public Task SendMessageToCaller(string message)
        {
            return Task.CompletedTask;
        }

        public Task SendMessageToGroup(string message)
        {
            return Task.CompletedTask;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Loop is here to wait until the server is running
            while (true)
            {
                try
                {
                    await _connection.StartAsync(cancellationToken);
                    break;
                }
                catch
                {
                    await Task.Delay(1000);
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _connection.DisposeAsync();
        }

        public Task ReceiveMessage(string user, string message)
        {
            throw new NotImplementedException();
        }

        public Task ReceiveMessage(string message)
        {
            throw new NotImplementedException();
        }
    }
}
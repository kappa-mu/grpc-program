using Grpc.Net.Client;
using MeterReader.gRPC;
using static MeterReader.gRPC.MeterReaderService;

namespace MeterReadingClient
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ReadingGenerator _generator;
        private readonly int _customerId;
        private readonly string _serviceUrl;

        public Worker(ILogger<Worker> logger, ReadingGenerator generator, IConfiguration config)
        {
            _logger = logger;
            _generator = generator;
            _customerId = config.GetValue<int>("CustomerId");
            _serviceUrl = config.GetValue<string>("ServiceUrl");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var channel = GrpcChannel.ForAddress(_serviceUrl);
                var client = new MeterReaderServiceClient(channel);

                var packet = new ReadingPacket()
                {
                    Successful = ReadingStatus.Success
                };

                for(var i = 0; i < 10; i++)
                {
                    var reading = await _generator.GenerateAsync(_customerId);
                    packet.Readings.Add(reading);
                }

                var status = client.AddReading(packet);
                if(status.Status == ReadingStatus.Success)
                {
                    _logger.LogInformation("Successfully called gRPC");
                }
                else
                {
                    _logger.LogInformation("gRPC call failed?");
                }


                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
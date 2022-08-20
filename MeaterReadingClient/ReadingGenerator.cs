using MeterReader.gRPC;

namespace MeterReadingClient;

public class ReadingGenerator
{
    public Task<ReadingMessage> GenerateAsync(int customerId)
    {
        var reading = new ReadingMessage()
        {
            CustomerId = customerId,
            ReadingValue = new Random().Next(10000)
        };
        return Task.FromResult(reading);
    }

}


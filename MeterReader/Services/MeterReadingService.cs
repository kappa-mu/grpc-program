using Grpc.Core;
using MeterReader.Domain;
using MeterReader.gRPC;
using static MeterReader.gRPC.MeterReaderService;

namespace MeterReader.Services;
public class MeterReadingService : MeterReaderServiceBase
{
    private ILogger<MeterReadingService> _logger;

    public MeterReadingService(ILogger<MeterReadingService> logger)
    {
        _logger = logger;

    }
    public override async Task<StatusMessage> AddReading(ReadingPacket request, ServerCallContext context)
    {
        _logger.LogInformation("Successfully invoked AddReading");
        if (request.Successful == ReadingStatus.Success)
        {

            foreach(var reading in request.Readings)
            {
                var readingValue = new MeterReading()
                {
                    CustomerId = reading.CustomerId,
                    value = reading.ReadingValue,
                    Notes = reading.Notes
                    
                };
            }

             
            return new StatusMessage()
            {
                Status = ReadingStatus.Success,
                Notes = "Successfully Read"
            };
        }
        return new StatusMessage()
        {
            Status = ReadingStatus.Failure,
            Notes = "Not Successfull"
        };
    }
}


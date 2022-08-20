namespace MeterReader.Domain;
public class MeterReading
{
    public int CustomerId { get; set; }
    public int value { get; set; }
    public string? Notes { get; set; }
}

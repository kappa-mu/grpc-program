using MeterReader.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddGrpc(cfg => cfg.EnableDetailedErrors = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();


app.MapGrpcService<MeterReadingService>();

app.Run();


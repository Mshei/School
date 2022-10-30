using EventStore;
using ParkingPlateRecognizer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<IEventStore, EventStore.EventStore>();
builder.Services.AddTransient<IEventStore, EventStore.EventStore>();
builder.Services.AddHttpClient<IParkingPlateRecognizerService, ParkingPlateRecognizerService>();
builder.Services.AddTransient<IParkingPlateRecognizerService, ParkingPlateRecognizerService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

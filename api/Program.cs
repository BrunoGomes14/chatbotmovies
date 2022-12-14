using api.Interfaces.Message;
using api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.ConfigureServices();
    
var app = builder.Build();
app.Services.GetService<TelegramConfiguration>()!
            .Setup();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsBrunoDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    new ThreadKeepAlive();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

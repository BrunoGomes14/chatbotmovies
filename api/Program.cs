using api.Models;
using api.Interfaces.Message;
using api.Services.External;
using Refit;
using api.Services;
using api.Business;
using api.Interfaces.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var config = builder.Configuration.Get<AppSettings>();
builder.Services.AddSingleton<AppSettings>(config);
builder.Services.AddScoped<WhatsappSendMessage>();
builder.Services.AddScoped<GetNearestTheater>();
builder.Services.AddScoped<GetMoviesInfo>();
builder.Services.AddScoped<TranslateAnswer>();
builder.Services.AddScoped<MessageProcessor>();
builder.Services.AddScoped<IChatDatabase, ChatMySqlDatabase>();

builder.Services
    .AddRefitClient<IGeolocationAPI>()
    .ConfigureHttpClient(c => {
        c.BaseAddress = new Uri("https://maps.googleapis.com/maps/api");
        c.Timeout = TimeSpan.FromSeconds(10);
    });

builder.Services
    .AddRefitClient<IIngressosAPI>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api-content.ingresso.com/v0"));

builder.Services
    .AddRefitClient<ITmdbAPI>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://api.themoviedb.org/3"));

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

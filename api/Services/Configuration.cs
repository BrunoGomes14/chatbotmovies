using api.Business;
using api.Interfaces.Data;
using api.Interfaces.Message;
using api.Models;
using api.Services.External;
using Refit;

namespace api.Services
{
    public static class Configuration
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            var config = builder.Configuration.Get<AppSettings>();
            builder.Services.AddSingleton<AppSettings>(config);

            builder.Services.AddScoped<WhatsappSendMessage>();
            builder.Services.AddScoped<TelegramSendMessage>();
            builder.Services.AddScoped<GetNearestTheater>();
            builder.Services.AddScoped<GetMoviesInfo>();
            builder.Services.AddScoped<TranslateAnswer>();
            builder.Services.AddScoped<MessageProcessor>();
            builder.Services.AddTransient<TelegramConfiguration>();

            builder.Services.AddScoped<IChatDatabase, ChatMySqlDatabase>();

            builder.Services
                 .AddRefitClient<IGeolocationAPI>()
                 .ConfigureHttpClient(c => {
                     c.BaseAddress = new Uri("https://maps.googleapis.com/maps/api");
                     c.Timeout = TimeSpan.FromSeconds(10);
                 });

            builder.Services
                .AddRefitClient<IIngressosAPI>()
                .ConfigureHttpClient(c => {
                    c.BaseAddress = new Uri("https://api-content.ingresso.com/v0");
                    c.Timeout = TimeSpan.FromSeconds(10);
                });

            builder.Services
                .AddRefitClient<ITmdbAPI>()
                .ConfigureHttpClient(c => {
                    c.BaseAddress = new Uri("https://api.themoviedb.org/3");
                    c.Timeout = TimeSpan.FromSeconds(10);
                });
        }
    }
}

using BugTracker.Services;
using BugTracker.View;
using Microsoft.Extensions.Logging;

namespace BugTracker
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
		builder.Logging.AddDebug();
#endif
            builder.Services.AddScoped<IBugService, BugService>();
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<ProjectViewModel>();
            builder.Services.AddSingleton<ProjectPage>();
            builder.Services.AddSingleton<TicketViewModel>();
            builder.Services.AddSingleton<TicketPage>();
            builder.Services.AddSingleton<EmployeeViewModel>();
            builder.Services.AddSingleton<EmployeePage>();

            return builder.Build();
        }
    }
}
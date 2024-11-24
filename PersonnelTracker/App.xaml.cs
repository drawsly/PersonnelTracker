using Microsoft.EntityFrameworkCore;
using PersonnelTracker.Services;
using PersonnelTracker.ViewModels.Pages;
using PersonnelTracker.ViewModels.Windows;
using PersonnelTracker.Views.Pages;
using PersonnelTracker.Views.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PersonnelTracker.ViewModels.Pages.piPages;
using PersonnelTracker.Views.Pages.piPages;

namespace PersonnelTracker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private static readonly IHost _host = Host
            .CreateDefaultBuilder()
            .ConfigureAppConfiguration(c => { c.SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!); })
            .ConfigureServices((context, services) =>
            {
                var connString = System.Configuration.ConfigurationManager.ConnectionStrings["PersonelDatabase"].ConnectionString;

                services.AddDbContext<AppDbContext>(options =>
                        options.UseMySql(connString, ServerVersion.AutoDetect(connString)));

                services.AddHostedService<ApplicationHostService>();

                services.AddSingleton<IPageService, PageService>();
                services.AddSingleton<IThemeService, ThemeService>();
                services.AddSingleton<ITaskBarService, TaskBarService>();
                services.AddSingleton<INavigationService, NavigationService>();
                services.AddSingleton<ISnackbarService, SnackbarService>();
                services.AddSingleton<IContentDialogService, ContentDialogService>();

                services.AddSingleton<PersonelService>();

                services.AddSingleton<INavigationWindow, MainWindow>();
                services.AddSingleton<MainWindowViewModel>();

                services.AddSingleton<GostergePaneliPage>();
                services.AddSingleton<GostergePaneliViewModel>();

                services.AddSingleton<AyarlarPage>();
                services.AddSingleton<AyarlarViewModel>();

                services.AddSingleton<PersonelIslemleriPage>();
                services.AddSingleton<PersonelIslemleriViewModel>();

                services.AddTransient<PersonelEkle>();
                services.AddTransient<PersonelEkleViewModel>();

                services.AddTransient<PersonelDuzenle>();
                services.AddTransient<PersonelDuzenleViewModel>();

                services.AddSingleton<OrganizasyonIslemleriPage>();
                services.AddSingleton<OrganizasyonIslemleriViewModel>();
            })
            .Build();

        public static T? GetService<T>()
            where T : class
        {
            return _host.Services.GetService(typeof(T)) as T;
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            _host.Start();
        }

        private async void OnExit(object sender, ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();
        }

        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
        }
    }
}
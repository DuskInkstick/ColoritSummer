using ColoritSummer.Interfaces.Repositories;
using ColoritSummer.Models.Entities;
using ColoritSummer.WPF.ViewModels;
using ColoritSummery.WebAPIClient.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;

namespace ColoritSummer.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IHost? _hosting;
        public static IHost Hosting => _hosting ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();
        public static IServiceProvider Services => Hosting.Services;
        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureServices(ConfigureServices);
        }

        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddScoped<MainWindowViewModel>();
            services.AddScoped<LoginWindowViewModel>();
            services.AddScoped<RegistrationWindowViewModel>();

            services.AddHttpClient<IRepository<UserInfo>, WebRepository<UserInfo>>(client =>
                client.BaseAddress = new Uri($"{host.Configuration["WebAPI"]}api/Users/"));
        }
        protected async override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var host = Hosting;
            await host.StartAsync().ConfigureAwait(false);
        }

        protected async override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            var host = Hosting;
            await host.StopAsync().ConfigureAwait(false);
            host.Dispose();
            _hosting = null;
        }
    }
}

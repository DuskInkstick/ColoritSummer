using Microsoft.Extensions.DependencyInjection;

namespace ColoritSummer.WPF.ViewModels
{
    internal class ViewModelsLocator
    {
        public MainWindowViewModel MainWindowViewModel => App.Services.GetRequiredService<MainWindowViewModel>();
    }
}

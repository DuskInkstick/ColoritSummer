using ColoritSummer.WPF.ViewModels.Base;

namespace ColoritSummer.WPF.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private string? _title = "Hu? Tao";
        public string? Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        public string Titel2 { get; set; } = "asdasd";
    }
}

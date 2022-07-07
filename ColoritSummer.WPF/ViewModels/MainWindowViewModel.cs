using ColoritSummer.WPF.Infrastructure.Commands;
using ColoritSummer.WPF.View.Windows;
using ColoritSummer.WPF.ViewModels.Base;
using System.Windows.Input;

namespace ColoritSummer.WPF.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Поля и свойства

        private string? _title = "Hu? Tao";
        /// <summary>Заголовок окна </summary>
        public string? Title
        {
            get => _title;
            set => Set(ref _title, value);
        }
        #endregion

        #region Комманды

        #region Открытия LoginWindow 
        /// <summary>Комманда открытия диалогового окна LoginWindow </summary>
        public ICommand OpenLoginCommand { get; }
        private bool CanOpenLoginCommandExecute(object? p) => true;
        private void OnOpenLoginCommandExecuted(object? p) => new LoginWindow().ShowDialog();

        public ICommand OpenRegistrationCommand { get; }
        #endregion

        #endregion

        public MainWindowViewModel()
        {
            #region Инициализация комманд
            OpenLoginCommand = new LambdaCommand(OnOpenLoginCommandExecuted, CanOpenLoginCommandExecute);
            OpenRegistrationCommand = new LambdaCommand(p => new RegistrationWindow().ShowDialog());
            #endregion
        }

    }
}

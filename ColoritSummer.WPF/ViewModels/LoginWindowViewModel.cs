using ColoritSummer.WPF.Infrastructure.Commands;
using ColoritSummer.WPF.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ColoritSummer.WPF.ViewModels
{
    internal class LoginWindowViewModel : ViewModel
    {
        #region Поля и свойства

        private string? _email;
        /// <summary>Электронная почта пользователя </summary>
        public string? Email
        {
            get => _email;
            set => Set(ref _email, value);
        }

        private string _infoField = "Зарегестрируйтесь пажалуйста очень надо";
        /// <summary>Текст уведомляющий пользователя о текущем статусе программы </summary>
        public string InfoField
        {
            get => _infoField;
            set => Set(ref _infoField, value);
        }
        #endregion

        #region Комманды

        /// <summary>Комманда для запроса на авторизацию </summary>
        public ICommand TryLoginCommand { get; }
        private bool CanTryLoginCommandExecute(object? p) =>
            string.IsNullOrEmpty(Email) == false;

        private void OnTryLoginCommandExecuted(object? p)
        {
            var pass = p as PasswordBox;

        }

        /// <summary>Комманда для запроса на регистрацию </summary>
        public ICommand RegistrationCommand { get; }

        private bool CanRegistrationCommandExecute(object? p) => true;
        private void onRegistrationCommandExecuted(object? p)
        {
        }

        #endregion

        public LoginWindowViewModel()
        {
            TryLoginCommand = new LambdaCommand(OnTryLoginCommandExecuted, CanTryLoginCommandExecute);
            RegistrationCommand = new LambdaCommand(onRegistrationCommandExecuted, CanRegistrationCommandExecute);
        }
    }
}

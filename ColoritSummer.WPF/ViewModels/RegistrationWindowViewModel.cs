using ColoritSummer.WPF.Infrastructure.Commands;
using ColoritSummer.WPF.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ColoritSummer.WPF.ViewModels
{
    internal class RegistrationWindowViewModel : ViewModel
    {
        #region Поля и свойства

        private string _email;
        /// <summary>Электронная почта организации </summary>
        public string Email
        {
            get => _email;
            set => Set(ref _email, value);
        }

        private string _organizationName;
        /// <summary>Название организации </summary>
        public string OrganizationName
        {
            get => _organizationName;
            set => Set(ref _organizationName, value);
        }

        private string? _city;
        /// <summary>Адрес: Город </summary>
        public string City
        {
            get => _city;
            set => Set(ref _city, value);
        }

        #endregion

        #region Комманды

        /// <summary>Команда для запроса на регистрацию</summary>
        public ICommand RegistrationCommand { get; }
        private bool CanRegistrationCommandExecute(object? p) =>
            !string.IsNullOrEmpty(Email)
            && !string.IsNullOrEmpty(OrganizationName)
            && !string.IsNullOrEmpty(City);

        private void OnRegistrationCommandExecuted(object? p)
        {
            var vals = p as object[];
            var pass1 = vals[0] as PasswordBox;
            var pass2 = vals[1] as PasswordBox;

            MessageBox.Show($"{pass1.Password} {pass2.Password} {Equals(pass1.Password, pass2.Password)}");
        } 

        public ICommand CancelCommand { get; }
        private bool CanCancelCommandExecute(object? p) => true;
        private void OnCancelCommandExecuted(object? p)
        {
        }
        #endregion

        public RegistrationWindowViewModel()
        {
            RegistrationCommand = new LambdaCommand(OnRegistrationCommandExecuted, CanRegistrationCommandExecute);
            CancelCommand = new LambdaCommand(OnCancelCommandExecuted, CanCancelCommandExecute);
        }
    }
}

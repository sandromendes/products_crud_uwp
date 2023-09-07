using Prism.Mvvm;
using ProductsCRUD.Business.Services.Users;
using ProductsCRUD.Common.Util;
using ProductsCRUD.Common.Util.Labels;
using ProductsCRUD.Domain.Models.Users;
using System;
using Windows.UI.Xaml.Controls;

namespace ProductsCRUD.ViewModels
{
    public class RegisterUserPageViewModel : BindableBase
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _phoneNumber;
        private string _cpf;
        private string _password;
        private string _errorMessage;

        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set => SetProperty(ref _phoneNumber, value);
        }

        public string CPF
        {
            get => _cpf;
            set => SetProperty(ref _cpf, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        private readonly IUserService userService;

        public RegisterUserPageViewModel(IUserService userService)
        {
            this.userService = userService;
        }

        public void RegisterUser()
        {
            var user = new User
            {
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                PhoneNumber = PhoneNumber,
                CPF = CPF,
                PasswordHash = EncryptionUtils.Encrypt(Password)
            };

            try
            {
                userService.RegisterUser(user);
                ShowSucessMessage();

                FirstName = null;
                LastName = null;
                Email = null;
                PhoneNumber = null;
                CPF = null;
                Password = null;
            }
            catch (Exception e)
            {
                ShowErrorMessage(e.Message);
            }
        }

        private async void ShowSucessMessage()
        {
            var dialog = new ContentDialog
            {
                Title = "Sucesso!",
                Content = "Usuário registrado com sucesso!",

                CloseButtonText = ButtonLabel.CLOSE_OK
            };
            await dialog.ShowAsync();
        }

        private async void ShowErrorMessage(string append)
        {
            var dialog = new ContentDialog
            {
                Title = "Erro!",
                Content = $"Erro ao registrar usuário. Tente novamente!" +
                $"{append}",
                CloseButtonText = ButtonLabel.CLOSE_OK
            };
            await dialog.ShowAsync();
        }
    }
}

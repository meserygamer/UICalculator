using System;
using System.Collections.Generic;
using System.Reactive;
using ReactiveUI;
using UICalculator.Models;

namespace UICalculator.ViewModels
{
	public class AuthorizationUCViewModel : ViewModelBase
	{
        /// <summary>
        /// Основной конструктор ViewModel
        /// </summary>
        public AuthorizationUCViewModel()
        {
            Login = string.Empty;
            Password = string.Empty;
            DoAuthorizationCommand = ReactiveCommand.Create<string>(DoAuthorizationCommandRun);
        }


        /// <summary>
        /// Событие, происходящее в момент успешного завершения авторизации
        /// </summary>
        public event Action NotifyAuthorizationWasSuccessComplete;


        /// <summary>
        /// Свойство для привязки логина
        /// </summary>
        public string Login
		{
			get => _login;
			set => this.RaiseAndSetIfChanged(ref _login, value);
		}
        /// <summary>
        /// Свойство для привязки пароля
        /// </summary>
        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        /// <summary>
        /// Комманда, выполняемая при нажатии на кнопку "Войти"
        /// </summary>
        public ReactiveCommand<string, Unit> DoAuthorizationCommand { get; }


        /// <summary>
        /// Метод, реализующий комманду DoAuthorizationCommand
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
		public void DoAuthorizationCommandRun(string parameter)
		{

			if(Password == AuthorizationData.CurrectlyPassword 
				&& Login == AuthorizationData.CurrectlyLogin)
			{
                NotifyAuthorizationWasSuccessComplete();
                return;
			}

		}


        /// <summary>
        /// Поле, хранящее логин
        /// </summary>
        private string _login;
        /// <summary>
        /// Поле, хранящее пароль
        /// </summary>
        private string _password;
    }
}
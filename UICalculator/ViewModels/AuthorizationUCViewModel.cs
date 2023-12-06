using System;
using System.Collections.Generic;
using System.Reactive;
using Avalonia.Threading;
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
            CaptchaUCViewModel = new CaptchaUCViewModel();
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
        /// свойство для привязки ViewModel капчи
        /// </summary>
        public CaptchaUCViewModel CaptchaUCViewModel
        {
            get => _captchaViewModel;
            set
            {
                value.CapchaWasPassed += onCaptchaWasPassed;
                value.CapchaWasFailed += onCaptchaWasFailed;
                this.RaiseAndSetIfChanged(ref _captchaViewModel, value);
            }
        }

        /// <summary>
        /// свойство для хранения статуса последней попытки входа
        /// </summary>
        public bool IsLastEnterWasFailed
        {
            get => _isLastEnterWasFailed;
            set => this.RaiseAndSetIfChanged(ref _isLastEnterWasFailed, value);
        }
        /// <summary>
        /// свойство для хранения статуса блокировки интерфейса 
        /// </summary>
        public bool IsInterfaceEnabled
        {
            get => _isInterfaceEnabled;
            set => this.RaiseAndSetIfChanged(ref _isInterfaceEnabled, value);
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
            IsLastEnterWasFailed = true;
            return;
		}


        /// <summary>
        /// Поле, хранящее логин
        /// </summary>
        private string _login;
        /// <summary>
        /// Поле, хранящее пароль
        /// </summary>
        private string _password;

        /// <summary>
        /// Поле хранящее ViewModel капчи
        /// </summary>
        private CaptchaUCViewModel _captchaViewModel;

        /// <summary>
        /// Поле, хранящее информацию о статусе последней попытки входа
        /// </summary>
        private bool _isLastEnterWasFailed = false;
        /// <summary>
        /// Поле, хранящее информацию о статусе блокировки интерфейса 
        /// </summary>
        private bool _isInterfaceEnabled = true;


        /// <summary>
        /// Метод реагирующий на провал капчи
        /// </summary>
        private void onCaptchaWasFailed()
        {
            DispatcherTimer DP = new DispatcherTimer();
            DP.Interval = new TimeSpan(0, 0, 10);
            DP.Tick += new EventHandler((a, b) =>
            {
                IsInterfaceEnabled = true;
                DP.Stop();
            });
            IsInterfaceEnabled = false;
            DP.Start();
        }
        /// <summary>
        /// Метод реагирующий на успешное завершение капчи
        /// </summary>
        private void onCaptchaWasPassed()
        {
            DoAuthorizationCommandRun("");
        }
    }
}
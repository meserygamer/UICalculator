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
        /// �������� ����������� ViewModel
        /// </summary>
        public AuthorizationUCViewModel()
        {
            Login = string.Empty;
            Password = string.Empty;
            DoAuthorizationCommand = ReactiveCommand.Create<string>(DoAuthorizationCommandRun);
            CaptchaUCViewModel = new CaptchaUCViewModel();
        }


        /// <summary>
        /// �������, ������������ � ������ ��������� ���������� �����������
        /// </summary>
        public event Action NotifyAuthorizationWasSuccessComplete;


        /// <summary>
        /// �������� ��� �������� ������
        /// </summary>
        public string Login
		{
			get => _login;
			set => this.RaiseAndSetIfChanged(ref _login, value);
		}
        /// <summary>
        /// �������� ��� �������� ������
        /// </summary>
        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        /// <summary>
        /// �������� ��� �������� ViewModel �����
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
        /// �������� ��� �������� ������� ��������� ������� �����
        /// </summary>
        public bool IsLastEnterWasFailed
        {
            get => _isLastEnterWasFailed;
            set => this.RaiseAndSetIfChanged(ref _isLastEnterWasFailed, value);
        }
        /// <summary>
        /// �������� ��� �������� ������� ���������� ���������� 
        /// </summary>
        public bool IsInterfaceEnabled
        {
            get => _isInterfaceEnabled;
            set => this.RaiseAndSetIfChanged(ref _isInterfaceEnabled, value);
        }

        /// <summary>
        /// ��������, ����������� ��� ������� �� ������ "�����"
        /// </summary>
        public ReactiveCommand<string, Unit> DoAuthorizationCommand { get; }


        /// <summary>
        /// �����, ����������� �������� DoAuthorizationCommand
        /// </summary>
        /// <param name="parameter">�������� �������</param>
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
        /// ����, �������� �����
        /// </summary>
        private string _login;
        /// <summary>
        /// ����, �������� ������
        /// </summary>
        private string _password;

        /// <summary>
        /// ���� �������� ViewModel �����
        /// </summary>
        private CaptchaUCViewModel _captchaViewModel;

        /// <summary>
        /// ����, �������� ���������� � ������� ��������� ������� �����
        /// </summary>
        private bool _isLastEnterWasFailed = false;
        /// <summary>
        /// ����, �������� ���������� � ������� ���������� ���������� 
        /// </summary>
        private bool _isInterfaceEnabled = true;


        /// <summary>
        /// ����� ����������� �� ������ �����
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
        /// ����� ����������� �� �������� ���������� �����
        /// </summary>
        private void onCaptchaWasPassed()
        {
            DoAuthorizationCommandRun("");
        }
    }
}
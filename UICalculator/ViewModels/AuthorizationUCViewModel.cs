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
        /// �������� ����������� ViewModel
        /// </summary>
        public AuthorizationUCViewModel()
        {
            Login = string.Empty;
            Password = string.Empty;
            DoAuthorizationCommand = ReactiveCommand.Create<string>(DoAuthorizationCommandRun);
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

		}


        /// <summary>
        /// ����, �������� �����
        /// </summary>
        private string _login;
        /// <summary>
        /// ����, �������� ������
        /// </summary>
        private string _password;
    }
}
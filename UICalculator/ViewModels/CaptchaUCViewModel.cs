using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reactive;
using System.Text;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Microsoft.CodeAnalysis.Operations;
using ReactiveUI;

namespace UICalculator.ViewModels
{
	public class CaptchaUCViewModel : ViewModelBase
	{
		public const int CAPTHCA_LENGHT = 8;
        public const int CAPTHCA_NOISE_QUANTITY = 20;

        public const string CAPTCHA_CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";


        public CaptchaUCViewModel() 
		{
			CaptchaCanvas = new Canvas()
			{
				Height = 300,
				Width = 400,
				HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Stretch,
                ClipToBounds = true,
            };
			CaptchaText = string.Empty;
			UserInputText = string.Empty;
			CheckCaptchaCommand = ReactiveCommand.Create<string>(CheckCaptchaCommandRun);
            RegenerateCaptchaCommand = ReactiveCommand.Create<string>(RegenerateCaptchaCommandRun);
			RegenerateCaptchaCommandRun("");
        }


		public event Action CapchaWasPassed;
		public event Action CapchaWasFailed;


		public Canvas CaptchaCanvas
		{
			get => _captchaCanvas;
			set => this.RaiseAndSetIfChanged(ref _captchaCanvas, value);
		}

		public string CaptchaText
		{
			get => _captchaText;
			set => this.RaiseAndSetIfChanged(ref _captchaText, value);
		}
        public string UserInputText
        {
            get => _userInputText;
            set => this.RaiseAndSetIfChanged(ref _userInputText, value);
        }

        public ReactiveCommand<string, Unit> CheckCaptchaCommand { get; }
		public ReactiveCommand<string, Unit> RegenerateCaptchaCommand { get; }


        public void CheckCaptchaCommandRun(string parameter)
		{
			if(CaptchaText == UserInputText.ToUpper())
			{
				CapchaWasPassed?.Invoke();
				return;
			}
			CaptchaCanvas.Children.Clear();
			RegenerateCaptchaCommandRun("");
			CapchaWasFailed?.Invoke();
			return;
		}
		public void RegenerateCaptchaCommandRun(string parameter)
		{
			Random random = new Random((int)DateTime.Now.Ticks);
            CaptchaText = new string (Enumerable.Repeat(CAPTCHA_CHARS, CAPTHCA_LENGHT).Select(s => s[random.Next(s.Length)]).ToArray());

			for(int i = 0; i < CAPTHCA_NOISE_QUANTITY; i++)
			{
				Point StartPoint = new Point(random.Next((int)CaptchaCanvas.Width), random.Next((int)CaptchaCanvas.Height));
				Point EndPoint = new Point(random.Next((int)CaptchaCanvas.Width), random.Next((int)CaptchaCanvas.Height));
                Line noiseLine = new Line()
				{
					ZIndex = 1,
                    StartPoint = StartPoint,
                    EndPoint = EndPoint,
                    Stroke = new SolidColorBrush(new Color(255, (byte)random.Next(255), (byte)random.Next(255), (byte)random.Next(255))),
                    StrokeThickness = 4
                };
				CaptchaCanvas.Children.Add(noiseLine);
			}

			double coordinateXSymbols = 10;

			for(int i = 0; i < CaptchaText.Length; i++)
			{
				TextBlock textBlock = new TextBlock()
				{
					Text = CaptchaText[i].ToString(),
					FontSize = 50,
					[Canvas.LeftProperty] = coordinateXSymbols,
					[Canvas.TopProperty] = random.Next(20, 300 - 80),
                };
				coordinateXSymbols += 50;
                CaptchaCanvas.Children.Add(textBlock);
            }

			this.RaisePropertyChanged(nameof(CaptchaCanvas));
		}


        private Canvas _captchaCanvas;

		private string _captchaText;
		private string _userInputText;
	}
}
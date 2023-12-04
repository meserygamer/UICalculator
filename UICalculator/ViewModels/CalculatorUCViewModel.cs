using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using ReactiveUI;
using UICalculator.Models;

namespace UICalculator.ViewModels
{
	public class CalculatorUCViewModel : ViewModelBase
	{
		public CalculatorUCViewModel()
		{
			Value1 = 0;
			Value2 = 0;
			Result = 0;
			SelectedFunction = AvailableMathematicalActions[0];
			CalculateMathFunctionCommand = ReactiveCommand.Create<string>(CalculateMathFunctionCommandRun);
		}


		public double Value1
		{
			get => _value1;
			set => this.RaiseAndSetIfChanged(ref _value1, value);
		}
		public double Value2
		{
			get => _value2;
			set => this.RaiseAndSetIfChanged(ref _value2, value);
		}
		public double Result
		{
			get => _result;
			set => this.RaiseAndSetIfChanged(ref _result, value);
		}

		public MathematicalFunction SelectedFunction
		{
			get => _selectedFunction;
			set => this.RaiseAndSetIfChanged(ref _selectedFunction, value);
		}

		public ObservableCollection<MathematicalFunction> AvailableMathematicalActions { get; init; }
			= new ObservableCollection<MathematicalFunction>()
			{
				new MathematicalFunction((x1,x2) => x1 + x2, "+", "Сложение"),
				new MathematicalFunction((x1,x2) => x1 - x2, "-", "Вычитание"),
				new MathematicalFunction((x1,x2) => x1 * x2, "*", "Умножение"),
				new MathematicalFunction((x1,x2) => x1 / x2, "/", "Деление"),
			};

		public ReactiveCommand<string, Unit> CalculateMathFunctionCommand { get; }


        public void CalculateMathFunctionCommandRun(string parameter) => Result = SelectedFunction.Func.Invoke(Value1, Value2);


        private double _value1;
		private double _value2;
		private double _result;

		private MathematicalFunction _selectedFunction;
	}
}
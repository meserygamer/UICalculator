using ReactiveUI;

namespace UICalculator.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public bool IsAuthorizationPage
        {
            get => _isAuthorizationPage;
            set => this.RaiseAndSetIfChanged(ref _isAuthorizationPage, value);
        }

        public bool IsCalculatorPage
        {
            get => _isCalculatorPage;
            set => this.RaiseAndSetIfChanged(ref _isCalculatorPage, value);
        }

        private bool _isAuthorizationPage = false;

        private bool _isCalculatorPage = true;
    }
}

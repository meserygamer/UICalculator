using ReactiveUI;

namespace UICalculator.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            AuthorizationUCViewModel.NotifyAuthorizationWasSuccessComplete += AuthorizationUCViewModel_NotifyAuthorizationWasSuccessComplete;
        }


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

        public AuthorizationUCViewModel AuthorizationUCViewModel
        {
            get => _authorizationUCViewModel;
            set => this.RaiseAndSetIfChanged(ref _authorizationUCViewModel, value);
        }

        public CalculatorUCViewModel CalculatorUCViewModel
        {
            get => _calculatorUCViewModel;
            set => this.RaiseAndSetIfChanged(ref _calculatorUCViewModel, value);
        }


        private bool _isAuthorizationPage = true;
        private bool _isCalculatorPage = false;

        private AuthorizationUCViewModel _authorizationUCViewModel = new AuthorizationUCViewModel();

        private CalculatorUCViewModel _calculatorUCViewModel = new CalculatorUCViewModel();


        private void AuthorizationUCViewModel_NotifyAuthorizationWasSuccessComplete()
        {
            IsAuthorizationPage = false;
            IsCalculatorPage = true;
        }
    }
}

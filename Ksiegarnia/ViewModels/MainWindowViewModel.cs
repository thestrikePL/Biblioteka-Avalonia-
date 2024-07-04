using Ksiegarnia.Model;
using Ksiegarnia.Persistence;
using ReactiveUI;

namespace Ksiegarnia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private object _currentView;
        private IAppDbContext _appDbContext;
        private User _currentUser;

        public object CurrentView
        {
            get => _currentView;
            set => this.RaiseAndSetIfChanged(ref _currentView, value);
        }

        public User CurrentUser
        {
            get => _currentUser;
            set => this.RaiseAndSetIfChanged(ref _currentUser, value);
        }

        public MainWindowViewModel()
        {
            _appDbContext = new AppDbContext("Data Source=WIKTORPC\\WIKTORPC;Initial Catalog=ksiegarnia;Integrated Security=True;Connect Timeout=30;Encrypt=False");
            CurrentView = new LoginViewModel(this, _appDbContext);
        }

        public void ShowBooksView()
        {
            CurrentView = new BooksViewModel(this, _appDbContext);
        }

        public void ShowCreateBookView()
        {
            CurrentView = new CreateBookViewModel(this, _appDbContext);
        }

      

        public void ShowCategoryView()
        {
            CurrentView = new CategoryViewModel(this, _appDbContext);
        }

        public void ShowCreateCategoryView()
        {
            CurrentView = new CreateCategoryViewModel(this, _appDbContext);
        }

        public void UpdateCategoryView(Category category)
        {
            CurrentView = new UpdateCategoryViewModel(this, _appDbContext, category);
        }

        public void ShowCheckoutsView()
        {
            CurrentView = new CheckoutViewModel(this, _appDbContext);
        }

        public void ShowCreateCheckoutView()
        {
            CurrentView = new CreateCheckoutViewModel(this, _appDbContext);
        }

        public void UpdateCheckoutView(Checkout checkout)
        {
            CurrentView = new UpdateCheckoutViewModel(this, _appDbContext, checkout);
        }


        public void ShowLoginView()
        {
            CurrentView = new LoginViewModel(this, _appDbContext);
        }

        public void ShowRegisterView()
        {
            CurrentView = new RegisterViewModel(this, _appDbContext);
        }

    }
}

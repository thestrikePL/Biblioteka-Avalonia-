using Ksiegarnia.Model;
using Ksiegarnia.Persistence;
using Ksiegarnia.Service;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Ksiegarnia.ViewModels
{
    public class UpdateCheckoutViewModel : ViewModelBase
    {
        private ObservableCollection<Book> _books;

        private Checkout _checkout;

        private Book _Book;
        private string _LastName;
        private string _Phone;
        private string _From;
        private string _To;

        private MainWindowViewModel _mainWindowViewModel;
        private CheckoutService _checkoutService;
        private BookService _bookService;

        public ObservableCollection<Book> Books
        {
            get => _books;
            set => this.RaiseAndSetIfChanged(ref _books, value);
        }

        public Book CheckoutBook
        {
            get => _Book;
            set => this.RaiseAndSetIfChanged(ref _Book, value);
        }

        public string CheckoutLastName
        {
            get => _LastName;
            set => this.RaiseAndSetIfChanged(ref _LastName, value);
        }
        public string CheckoutPhone
        {
            get => _Phone;
            set => this.RaiseAndSetIfChanged(ref _Phone, value);
        }

        public string CheckoutFrom
        {
            get => _From;
            set => this.RaiseAndSetIfChanged(ref _From, value);
        }

        public string CheckoutTo
        {
            get => _To;
            set => this.RaiseAndSetIfChanged(ref _To, value);
        }

        public ReactiveCommand<Unit, Unit> UpdateCommand { get; }
        public ReactiveCommand<Unit, Unit> BackCommand { get; }

        public UpdateCheckoutViewModel(MainWindowViewModel mainWindowViewModel, IAppDbContext appDbContext, Checkout checkout)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _checkoutService = new CheckoutService(appDbContext);

            UpdateCommand = ReactiveCommand.Create(UpdateCheckout);
            BackCommand = ReactiveCommand.Create(Back);

            _bookService = new BookService(appDbContext);
            _books = new ObservableCollection<Book>();

            _checkout = checkout;
            _LastName = checkout.LastName;
            _Phone = checkout.Phone;
            _From = checkout.From.ToString("yyyy-MM-dd");
            _To = checkout.To.ToString("yyyy-MM-dd");
            _Book = checkout.Book;

            LoadBooks();
        }

        private async void UpdateCheckout()
        {
            if (string.IsNullOrEmpty(CheckoutLastName) || string.IsNullOrEmpty(CheckoutPhone) || CheckoutBook == null || string.IsNullOrEmpty(CheckoutFrom) || string.IsNullOrEmpty(CheckoutTo))
            {
                return;
            }

            await _checkoutService.UpdateCheckout(_checkout.Id, CheckoutBook, CheckoutLastName, CheckoutPhone, DateTime.Parse(CheckoutFrom), DateTime.Parse(CheckoutTo));
            _mainWindowViewModel.ShowCheckoutsView();
        }

        private void Back()
        {
            _mainWindowViewModel.ShowCheckoutsView();
        }

        private void LoadBooks()
        {
            _books.Clear();

            foreach (var book in _bookService.GetBooks())
                _books.Add(book);
        }
    }
}

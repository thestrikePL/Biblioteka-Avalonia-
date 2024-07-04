using Ksiegarnia.Model;
using Ksiegarnia.Persistence;
using Ksiegarnia.Service;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Ksiegarnia.ViewModels
{
    public class CreateBookViewModel : ViewModelBase
    {
        private MainWindowViewModel _mainWindowViewModel;
        private BookService _bookService;
        private CategoryService _categoryService;

        private string _Title;
        private string _Author;
        private Category _Category;
        private IEnumerable<Category> _CategoryList;

        public string BookTitle
        {
            get => _Title;
            set => this.RaiseAndSetIfChanged(ref _Title, value);
        }
        public string BookAuthor
        {
            get => _Author;
            set => this.RaiseAndSetIfChanged(ref _Author, value);
        }
        public Category BookCategory
        {
            get => _Category;
            set => this.RaiseAndSetIfChanged(ref _Category, value);
        }
        public IEnumerable<Category> CategoryList
        {
            get => _CategoryList;
            set => this.RaiseAndSetIfChanged(ref _CategoryList, value);
        }
        public ReactiveCommand<Unit, Unit> CreateBookCommand { get; }
        public ReactiveCommand<Unit, Unit> BackCommand { get; }

        public CreateBookViewModel(MainWindowViewModel mainWindowViewModel, IAppDbContext appDbContext)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _bookService = new BookService(appDbContext);
            _categoryService = new CategoryService(appDbContext);

            CreateBookCommand = ReactiveCommand.Create(CreateBook);
            BackCommand = ReactiveCommand.Create(Back);
            _CategoryList = _categoryService.GetCategory();
        }

        private async void CreateBook()
        {
            if (string.IsNullOrEmpty(_Title) || string.IsNullOrEmpty(_Author) || _Category == null)
            {
                return;
            }

            await _bookService.CreateBook(_Author, _Title, _Category, _mainWindowViewModel.CurrentUser);
            _mainWindowViewModel.ShowBooksView();
        }

        private void Back()
        {
            _mainWindowViewModel.ShowBooksView();
        }
    }
}

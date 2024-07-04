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
    public class BooksViewModel : ViewModelBase
    {
        private ObservableCollection<Book> _books;
        private ObservableCollection<Category> _categories;

        private Book _sBook;
        private Category _sCategory;

        private BookService _bookService;
        private CategoryService _categoryService;
        private MainWindowViewModel _mainWindowViewModel;

        public ObservableCollection<Book> Books 
        { 
            get => _books;
            set => this.RaiseAndSetIfChanged(ref _books, value);
        }

        public ObservableCollection<Category> Category
        {
            get => _categories;
            set => this.RaiseAndSetIfChanged(ref _categories, value);
        }

        public Book SBook
        {
            get => _sBook;
            set => this.RaiseAndSetIfChanged(ref _sBook, value);
        }

        public Category SCategory
        {
            get => _sCategory;
            set
            {
                this.RaiseAndSetIfChanged(ref _sCategory, value);
                LoadBooks();
            }
        }

       
        public ReactiveCommand<Unit, Unit> CreateBookCommand { get; }
        public ReactiveCommand<Unit, Unit> ShowCategoryCommand { get; }
        public ReactiveCommand<Unit, Unit> ShowCheckoutsCommand { get; }
        

        public BooksViewModel(MainWindowViewModel mainWindowViewModel, IAppDbContext appDbContext)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _bookService = new BookService(appDbContext);
            _categoryService = new CategoryService(appDbContext);

            _books = new ObservableCollection<Book>();
            _categories = new ObservableCollection<Category>();

            
            CreateBookCommand = ReactiveCommand.Create(CreateBook);
            ShowCategoryCommand = ReactiveCommand.Create(ShowCategory);
            ShowCheckoutsCommand = ReactiveCommand.Create(ShowCheckouts);
           

            LoadCategory();
            LoadBooks();
        }

        

        private void CreateBook()
        {
            _mainWindowViewModel.ShowCreateBookView();
        }

       

        private void LoadCategory()
        {
            _categories.Clear();

            foreach (var category in _categoryService.GetCategory())
            {
                _categories.Add(category);
            }
        }

        private void LoadBooks()
        {
            _books.Clear();

            foreach (var book in _bookService.GetBooks())
            {
                if (_sCategory != null && book.Category.Name != _sCategory.Name)
                    continue;

                _books.Add(book);
            }
        }

        private void ShowCategory()
        {
            _mainWindowViewModel.ShowCategoryView();
        }

        private void ShowCheckouts()
        {
            _mainWindowViewModel.ShowCheckoutsView();
        }

    }
}

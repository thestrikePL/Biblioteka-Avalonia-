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
    public class CategoryViewModel : ViewModelBase
    {
        private ObservableCollection<Category> _categories;
        private Category _sCategory;

        private CategoryService _categoryService;
        private MainWindowViewModel _mainWindowViewModel;

        public ObservableCollection<Category> Category
        { 
            get => _categories;
            set => this.RaiseAndSetIfChanged(ref _categories, value);
        }

        public Category SCategory
        {
            get => _sCategory;
            set => this.RaiseAndSetIfChanged(ref _sCategory, value);
        }

        public ReactiveCommand<Unit, Unit> RemoveCommand { get; }
        public ReactiveCommand<Unit, Unit> CreateCommand { get; }
        public ReactiveCommand<Unit, Unit> BackCommand { get; }
        public ReactiveCommand<Unit, Unit> UpdateCommand { get; }

        public CategoryViewModel(MainWindowViewModel mainWindowViewModel, IAppDbContext appDbContext)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _categoryService = new CategoryService(appDbContext);

            _categories = new ObservableCollection<Category>();

            RemoveCommand = ReactiveCommand.Create(RemoveCategory);
            CreateCommand = ReactiveCommand.Create(CreateCategory);
            BackCommand = ReactiveCommand.Create(Back);
            UpdateCommand = ReactiveCommand.Create(UpdateCategory);

            LoadCategory();
        }

        private async void RemoveCategory()
        {
            if (_sCategory == null)
                return;

            await _categoryService.RemoveCategory(_sCategory.Id);
            LoadCategory();
        }

        private void CreateCategory()
        {
            _mainWindowViewModel.ShowCreateCategoryView();
        }

        private void UpdateCategory()
        {
            if (_sCategory == null)
                return;

            _mainWindowViewModel.UpdateCategoryView(_sCategory);
        }

        private void Back()
        {
            _mainWindowViewModel.ShowBooksView();
        }

        private void LoadCategory()
        {
            _categories.Clear();

            foreach (var category in _categoryService.GetCategory())
            {
                _categories.Add(category);
            }
        }

    }
}

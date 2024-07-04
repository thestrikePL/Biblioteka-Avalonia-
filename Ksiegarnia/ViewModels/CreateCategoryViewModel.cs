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
    public class CreateCategoryViewModel : ViewModelBase
    {
        private string _Name;

        private MainWindowViewModel _mainWindowViewModel;
        private CategoryService _categoryService;

        public string CategoryName
        {
            get => _Name;
            set => this.RaiseAndSetIfChanged(ref _Name, value);
        }

        public ReactiveCommand<Unit, Unit> CreateCategoryCommand { get; }
        public ReactiveCommand<Unit, Unit> BackCommand { get; }

        public CreateCategoryViewModel(MainWindowViewModel mainWindowViewModel, IAppDbContext appDbContext)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _categoryService = new CategoryService(appDbContext);

            CreateCategoryCommand = ReactiveCommand.Create(CreateCategory);
            BackCommand = ReactiveCommand.Create(Back);
        }

        private async void CreateCategory()
        {
            if (string.IsNullOrEmpty(_Name))
            {
                return;
            }

            await _categoryService.CreateCategory(_Name);
            _mainWindowViewModel.ShowCategoryView();
        }

        private void Back()
        {
            _mainWindowViewModel.ShowCategoryView();
        }
    }
}

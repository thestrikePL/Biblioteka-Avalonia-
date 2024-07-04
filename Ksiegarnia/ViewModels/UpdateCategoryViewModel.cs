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
    public class UpdateCategoryViewModel : ViewModelBase
    {
        private MainWindowViewModel _mainWindowViewModel;
        private CategoryService _categoryService;

        private Category _category;
        private string _Name;

        public string CategoryName
        {
            get => _Name;
            set => this.RaiseAndSetIfChanged(ref _Name, value);
        }

        public ReactiveCommand<Unit, Unit> UpdateCategoryCommand { get; }
        public ReactiveCommand<Unit, Unit> BackCommand { get; }

        public UpdateCategoryViewModel(MainWindowViewModel mainWindowViewModel, IAppDbContext appDbContext, Category category)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _categoryService = new CategoryService(appDbContext);

            _category = category;
            _Name = category.Name;

            UpdateCategoryCommand = ReactiveCommand.Create(UpdateCategory);
            BackCommand = ReactiveCommand.Create(Back);
        }

        private async void UpdateCategory()
        {
            if (string.IsNullOrEmpty(_Name))
            {
                return;
            }

            await _categoryService.UpdateCategory(_category.Id, _Name);
            _mainWindowViewModel.ShowCategoryView();
        }

        private void Back()
        {
            _mainWindowViewModel.ShowCategoryView();
        }
    }
}

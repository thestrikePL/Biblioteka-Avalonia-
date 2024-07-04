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
    public class CheckoutViewModel : ViewModelBase
    {
        private ObservableCollection<Checkout> _checkouts;
        private Checkout _selectedCheckout;

        private CheckoutService _checkoutService;
        private MainWindowViewModel _mainWindowViewModel;

        public ObservableCollection<Checkout> Checkouts
        { 
            get => _checkouts;
            set => this.RaiseAndSetIfChanged(ref _checkouts, value);
        }

        public Checkout SelectedCheckout
        {
            get => _selectedCheckout;
            set => this.RaiseAndSetIfChanged(ref _selectedCheckout, value);
        }

        public ReactiveCommand<Unit, Unit> RemoveCommand { get; }
        public ReactiveCommand<Unit, Unit> CreateCommand { get; }
        public ReactiveCommand<Unit, Unit> BackCommand { get; }
        public ReactiveCommand<Unit, Unit> UpdateCommand { get; }

        public CheckoutViewModel(MainWindowViewModel mainWindowViewModel, IAppDbContext appDbContext)
        {
            _mainWindowViewModel = mainWindowViewModel;
            _checkoutService = new CheckoutService(appDbContext);

            _checkouts = new ObservableCollection<Checkout>();

            RemoveCommand = ReactiveCommand.Create(RemoveCheckout);
            CreateCommand = ReactiveCommand.Create(CreateCheckout);
            BackCommand = ReactiveCommand.Create(Back);
            UpdateCommand = ReactiveCommand.Create(UpdateCheckout);
            LoadCheckouts();
        }

        private async void RemoveCheckout()
        {
            if (_selectedCheckout == null)
                return;

            await _checkoutService.RemoveCheckout(_selectedCheckout.Id);
            LoadCheckouts();
        }

        private void CreateCheckout()
        {
            _mainWindowViewModel.ShowCreateCheckoutView();
        }

        private void UpdateCheckout()
        {
            if (_selectedCheckout == null)
                return;

            _mainWindowViewModel.UpdateCheckoutView(_selectedCheckout);
        }

        private void Back()
        {
            _mainWindowViewModel.ShowBooksView();
        }

        private void LoadCheckouts()
        {
            _checkouts.Clear();

            foreach (var checkout in _checkoutService.GetCheckouts())
            {
                _checkouts.Add(checkout);
            }
        }

    }
}

using InventoryManagementGUI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace InventoryManagementGUI.ViewModel
{
    class NavigationManagerVM : ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand HomeCommand { get; set; }
        public ICommand SupplierCommand { get; set; }
        public ICommand ProductsCommand { get; set; }
        public ICommand OrdersCommand { get; set; }
        public ICommand TransactionsCommand { get; set; }
        public ICommand SettingsCommand { get; set; }
        public ICommand LogoutCommand { get; set; }

        private void Home(object obj) => CurrentView = new HomeVM();
        private void Supplier(object obj) => CurrentView = new SupplierVM();
        private void Product(object obj) => CurrentView = new ProductManagerVM();
        private void Order(object obj) => CurrentView = new OrderVM();
        private void Transaction(object obj) => CurrentView = new TransactionVM();
        private void Setting(object obj) => CurrentView = new SettingVM();


        public NavigationManagerVM()
        {
            HomeCommand = new RelayCommand(Home);
            SupplierCommand = new RelayCommand(Supplier);
            ProductsCommand = new RelayCommand(Product);
            OrdersCommand = new RelayCommand(Order);
            TransactionsCommand = new RelayCommand(Transaction);
            SettingsCommand = new RelayCommand(Setting);

            // Startup Page
            CurrentView = new HomeVM();
        }
    }
}

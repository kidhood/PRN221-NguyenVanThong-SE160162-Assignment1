using InventoryManagementBO.Models;
using InventoryManagementGUI.Dialog;
using InventoryManagementRepository.Interface;
using InventoryManagementService;
using InventoryManagementService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InventoryManagementGUI.View
{
    /// <summary>
    /// Interaction logic for Customers.xaml
    /// </summary>
    public partial class Suppliers : UserControl
    {
        private readonly ISupplierService supplierService;
        public Suppliers()
        {
            InitializeComponent();
            supplierService = new SupplierService();
        }

        private void btn_PagingClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                int pageNumber = int.Parse(button.Content.ToString());
                supplierDataGrid.ItemsSource = supplierService.GetSupplierByPaging(pageNumber);
            }
        }

        private void btn_SearchByName(object sender, RoutedEventArgs e)
        {
            supplierDataGrid.ItemsSource = supplierService.GetProductByName(textBoxSearch.Text);
        }

        private void btn_AddNew(object sender, RoutedEventArgs e)
        {
            SupplierDetail supplierDetail = new SupplierDetail();
            supplierDetail.ShowDialog();
            supplierDataGrid.ItemsSource = supplierService.GetSupplierByPaging(1);
        }

        private void btn_Update(object sender, RoutedEventArgs e)
        {
            var supplierSelected = (Supplier)supplierDataGrid.SelectedItem;
            SupplierDetail supplierDetail = new SupplierDetail(supplierSelected, true);
            var isShow = supplierDetail.ShowDialog();

            if (isShow != null && isShow.Value)
            {
                supplierDataGrid.ItemsSource = supplierService.GetSupplierByPaging(1);
            }
            else
            {
                supplierDataGrid.ItemsSource = supplierService.GetSupplierByPaging(1);
            }
        }

        private void btn_Delete(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this supplier?",
            "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var supplierSelected = (Supplier)supplierDataGrid.SelectedItem;
                supplierSelected.IsDelete = true;
                if (supplierService.UpdateSupplier(supplierSelected))
                {
                    MessageBox.Show("Delete Supplier successfully");
                    supplierDataGrid.ItemsSource = supplierService.GetSupplierByPaging(1);
                }
                else
                {
                    MessageBox.Show("Delete Supplier fail");
                }
            }
        }
    }
}

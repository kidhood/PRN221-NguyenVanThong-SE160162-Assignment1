using InventoryManagementBO.Models;
using InventoryManagementService;
using InventoryManagementService.Interface;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace InventoryManagementGUI.Dialog
{
    /// <summary>
    /// Interaction logic for ProductDetail.xaml
    /// </summary>
    public partial class SupplierDetail : Window
    {
        private readonly ISupplierService _supplierService;
        private readonly IProductService _productService;
        private Supplier supplierUpdate;
        private bool _isUpdate;

        public ObservableCollection<KeyValuePair<string, string>> SupplierList { get; set; }
        public SupplierDetail(Supplier supplier = null, bool isUpdate = false)
        {
            InitializeComponent();

            _supplierService = new SupplierService();
            _productService = new ProductService();

            supplierUpdate = supplier;
            _isUpdate = isUpdate;
            if (isUpdate)
            {
                this.SetDataToProduct(supplier);
            }
        }

        public void SetDataToProduct(Supplier supplier)
        {
            txtName.textBox.Text = supplier.Name;
            txtEmail.textBox.Text = supplier.Email.ToString();
            txtPhone.textBox.Text = supplier.Phone;
            txtAddress.textBox.Text = supplier.Address.ToString();
           

        }

        private Supplier GetSupplierFromInput()
        {
            Supplier supplier = new Supplier();
            supplier.Name = txtName.textBox.Text;
            supplier.Email = txtEmail.textBox.Text;
            supplier.Address = txtAddress.textBox.Text;
            supplier.Phone = txtPhone.textBox.Text;
            supplier.IsDelete = false;

            if (_isUpdate)
            {
                supplier.Id = supplierUpdate.Id;
            }

            return supplier;
        }

        private void btn_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_Save(object sender, RoutedEventArgs e)
        {
            if (!CheckIsValidate())
            {
                MessageBox.Show("Please enter fully input");
                return;
            }


            if(_isUpdate)
            {
                if (this._supplierService.UpdateSupplier(this.GetSupplierFromInput())){
                    MessageBox.Show("Update supplier successfully");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Update supplier fail");
                }
            }
            else
            {
                if (this._supplierService.RegisterSupplier(this.GetSupplierFromInput()))
                {
                    MessageBox.Show("Create supplier successfully");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Create supplier fail");
                }
            }
            
        }

        private bool CheckIsValidate()
        {
            if(CheckIsNullOrEmpty(new List<string> { txtName.textBox.Text, txtAddress.textBox.Text, txtEmail.textBox.Text
            , txtPhone.textBox.Text }))
            {
                return false;
            }

            return true;
        }

        private bool CheckNumberIsValidateInput()
        {
            return true;
        }

        private bool CheckIsNullOrEmpty(List<string> str)
        {
            return str.Any(x => string.IsNullOrEmpty(x));
        }

        private bool CheckNumber(List<string> str)
        {
            return str.Any(x => !int.TryParse(x, out _));
        }
    }
}

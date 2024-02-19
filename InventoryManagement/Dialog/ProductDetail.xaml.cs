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
    public partial class ProductDetail : Window
    {
        private readonly ISupplierService _supplierService;
        private readonly IProductService _productService;
        private Product productUpdate;
        private bool _isUpdate;

        public ObservableCollection<KeyValuePair<string, string>> SupplierList { get; set; }
        public ProductDetail(Product product = null, bool isUpdate = false)
        {
            InitializeComponent();

            _supplierService = new SupplierService();
            _productService = new ProductService();
            SupplierList = new ObservableCollection<KeyValuePair<string, string>>(_supplierService.GetSuppliers().Select(x => new KeyValuePair<string, string>(Convert.ToString(x.Id), x.Name)).ToList());
            cboSupplier.DataList = SupplierList;

            productUpdate = product;
            _isUpdate = isUpdate;
            if (isUpdate)
            {
                this.SetDataToProduct(product);
            }
        }

        public void SetDataToProduct(Product product)
        {
            txtName.textBox.Text = product.Name;
            txtPrice.textBox.Text = product.Price.ToString();
            txtDescription.textBox.Text = product.Description;
            txtQuantity.textBox.Text = product.Quantity.ToString();
            txtImage.textBox.Text = product.ImagePath;

            cboSupplier.comboBox.SelectedItem = SupplierList.FirstOrDefault(x => x.Key == product.SupplierId.ToString());

        }

        private Product GetProductFromInput()
        {
            Product product = new Product();
            product.Name   = txtName.textBox.Text;
            product.Description = txtDescription.textBox.Text;
            product.ImagePath = txtImage.textBox.Text;
            product.IsDelete = false;
            var supplier = (KeyValuePair<string, string>)cboSupplier.comboBox.SelectedItem;
            product.SupplierId = int.Parse(supplier.Key);
            product.Price = double.Parse(txtPrice.textBox.Text.Trim());
            product.Quantity = int.Parse(txtQuantity.textBox.Text.Trim());

            if (_isUpdate)
            {
                product.Id = productUpdate.Id;
            }

            return product;
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

            if (!CheckNumberIsValidateInput())
            {
                MessageBox.Show("Please enter an number for price and quantity");
                return;
            }

            if(_isUpdate)
            {
                if (this._productService.UpdateProduct(this.GetProductFromInput())){
                    MessageBox.Show("Update product successfully");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Update product fail");
                }
            }
            else
            {
                if (this._productService.RegisterProduct(this.GetProductFromInput()))
                {
                    MessageBox.Show("Create product successfully");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Create product fail");
                }
            }
            
        }

        private bool CheckIsValidate()
        {
            if(CheckIsNullOrEmpty(new List<string> { txtName.textBox.Text, txtDescription.textBox.Text, txtImage.textBox.Text
            , txtPrice.textBox.Text, txtQuantity.textBox.Text}))
            {
                return false;
            }

            if(cboSupplier.comboBox.SelectedItem == default)
            {
                return false;
            }

            return true;
        }

        private bool CheckNumberIsValidateInput()
        {
            if(!double.TryParse(txtPrice.textBox.Text, out _))
            {
                return false;
            }

            return !this.CheckNumber(new List<string> { txtQuantity.textBox.Text});
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

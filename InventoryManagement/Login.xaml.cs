using InventoryManagementBO.Utilities;
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
using System.Windows.Shapes;

namespace InventoryManagementGUI
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly IAccountService accountService;
        public Login()
        {
            InitializeComponent();
            accountService = new AccountService();
        }

        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var userName = txtUser.Text;
            var pass = txtPass.Password;

            var account = accountService.GetAccountByUserNameAndPassword(userName, pass);
            if(account == null)
            {
                MessageBox.Show("Account not correct!");
            }
            else
            {
                switch (account.Role)
                {
                    case (int)AccountRoleCode.Staff:
                        StaffWindow staff = new StaffWindow();
                        staff.Show();
                        break;
                    case (int)AccountRoleCode.Manager:
                        ManagerWindow manager = new ManagerWindow();
                        manager.Show();
                        break;
                    case (int)AccountRoleCode.Admin:
                        AdminWindow admin = new AdminWindow();
                        admin.Show();
                        break;
                }

                this.Close();
            }
            
        }
    }
}

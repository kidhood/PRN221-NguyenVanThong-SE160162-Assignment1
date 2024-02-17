using InventoryManagementBO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagementGUI.ViewModel
{
    class CustomerVM : Utilities.ViewModelBase
    {
        private readonly Account _account;

        public CustomerVM()
        {
            _account = new Account();
        }
    }
}

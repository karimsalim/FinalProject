using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;

namespace BackEndASP.ViewModel
{
    public class EmployeeViewModel
    {
        public Employee CurrentEmployee { get; set; }
        public SelectList ChangeManager { get; set; }
        public SelectList ChangeEmployee { get; set; }
        public List<Client> ListClient { get; set; }

    }
}
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
        public SelectList ListManager { get; set; }
    }
}
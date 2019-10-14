using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BackEndASP.ViewModel
{
    public class ClientViewModel
    {
        public Client currentClient { get; set; }
        public int idConseiller { get; set; }
    }
}
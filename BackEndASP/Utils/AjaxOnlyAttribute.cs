using System;
using System.Reflection;
using System.Web.Mvc;

namespace BackEndASP.Controllers
{
    /// <summary>
    /// Classe permetant de filtrer uniquement les appels ajax dans les méthodes du controlleur
    /// </summary>
    internal class AjaxOnlyAttribute : ActionMethodSelectorAttribute
    {
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            return controllerContext.RequestContext.HttpContext.Request.IsAjaxRequest();
        }
    }
}
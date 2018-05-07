using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;

namespace Ajf.CoreSolver.WebApi
{
    [ExcludeFromCodeCoverage]
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}

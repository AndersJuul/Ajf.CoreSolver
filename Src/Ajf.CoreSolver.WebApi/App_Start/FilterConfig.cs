using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;

namespace Ajf.CoreSolver.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class FilterConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}

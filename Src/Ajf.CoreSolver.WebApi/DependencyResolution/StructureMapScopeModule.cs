namespace Ajf.CoreSolver.WebApi.DependencyResolution {
    using System.Web;

    using Ajf.CoreSolver.WebApi.App_Start;

    using StructureMap.Web.Pipeline;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class StructureMapScopeModule : IHttpModule {
        #region Public Methods and Operators

        /// <summary>
        /// 
        /// </summary>
        public void Dispose() {
        }

        /// <summary>
        /// 
        /// </summary>
        public void Init(HttpApplication context) {
            context.BeginRequest += (sender, e) => StructuremapMvc.StructureMapDependencyScope.CreateNestedContainer();
            context.EndRequest += (sender, e) => {
                HttpContextLifecycle.DisposeAndClearAll();
                StructuremapMvc.StructureMapDependencyScope.DisposeNestedContainer();
            };
        }

        #endregion
    }
}
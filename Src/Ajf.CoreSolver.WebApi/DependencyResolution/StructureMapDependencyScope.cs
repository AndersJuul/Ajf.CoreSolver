// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StructureMapDependencyScope.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Ajf.CoreSolver.WebApi.DependencyResolution {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using Microsoft.Practices.ServiceLocation;

    using StructureMap;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The structure map dependency scope.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class StructureMapDependencyScope : ServiceLocatorImplBase {
        #region Constants and Fields

        private const string NestedContainerKey = "Nested.Container.Key";

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// 
        /// </summary>
        public StructureMapDependencyScope(IContainer container) {
            if (container == null) {
                throw new ArgumentNullException("container");
            }
            Container = container;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public IContainer Container { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IContainer CurrentNestedContainer {
            get {
                return (IContainer)HttpContext.Items[NestedContainerKey];
            }
            set {
                HttpContext.Items[NestedContainerKey] = value;
            }
        }

        #endregion

        #region Properties

        private HttpContextBase HttpContext {
            get {
                var ctx = Container.TryGetInstance<HttpContextBase>();
                return ctx ?? new HttpContextWrapper(System.Web.HttpContext.Current);
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// 
        /// </summary>
        public void CreateNestedContainer() {
            if (CurrentNestedContainer != null) {
                return;
            }
            CurrentNestedContainer = Container.GetNestedContainer();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose() {
            if (CurrentNestedContainer != null) {
                CurrentNestedContainer.Dispose();
            }

            Container.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        public void DisposeNestedContainer() {
            if (CurrentNestedContainer != null) {
                CurrentNestedContainer.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<object> GetServices(Type serviceType) {
            return DoGetAllInstances(serviceType);
        }

        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        protected override IEnumerable<object> DoGetAllInstances(Type serviceType) {
            return (CurrentNestedContainer ?? Container).GetAllInstances(serviceType).Cast<object>();
        }

        /// <summary>
        /// 
        /// </summary>
        protected override object DoGetInstance(Type serviceType, string key) {
            IContainer container = (CurrentNestedContainer ?? Container);

            if (string.IsNullOrEmpty(key)) {
                return serviceType.IsAbstract || serviceType.IsInterface
                    ? container.TryGetInstance(serviceType)
                    : container.GetInstance(serviceType);
            }

            return container.GetInstance(serviceType, key);
        }

        #endregion
    }
}

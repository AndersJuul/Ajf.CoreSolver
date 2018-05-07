// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Web Advanced">
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

namespace Ajf.CoreSolver.WebApi.DependencyResolution
{
    using Ajf.CoreSolver.Shared;
    using AutoMapper;
    using EasyNetQ;
    using StructureMap.Configuration.DSL;
    using StructureMap.Graph;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class DefaultRegistry : StructureMap.Registry {

        public DefaultRegistry() {
            var appSettings = new AppSettings();
            var bus = RabbitHutch.CreateBus(appSettings.EasyNetQConfig);

            Scan(
                scan => {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                });
            Scan(
                scan => {
                    scan.AssemblyContainingType<ICalculationRequestValidator>();
                    scan.WithDefaultConventions();
                });

            // Auto mapper config
            For<IMapper>().Use(MapperProvider.GetMapper());
            
            For<IBus>().Use(bus);
            For<IAppSettings>().Use(appSettings);
        }
    }
}
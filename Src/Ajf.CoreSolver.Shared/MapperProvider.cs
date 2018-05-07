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

using System;
using System.Diagnostics.CodeAnalysis;
using Ajf.CoreSolver.DbModels;
using Ajf.CoreSolver.Models;
using Ajf.CoreSolver.Models.Internal;
using AutoMapper;

namespace Ajf.CoreSolver.Shared
{
    [ExcludeFromCodeCoverage]
    public static class MapperProvider
    {
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Calculation, CalculationEntity>();
                cfg.CreateMap<CalculationRequest, Calculation>();
                cfg.CreateMap<CalculationStatusDto, CalculationStatus>()
                    .ConvertUsing(value =>
                    {
                        switch (value)
                        {
                            case CalculationStatusDto.None:
                                return CalculationStatus.None;
                            case CalculationStatusDto.CalculationQueued:
                                return CalculationStatus.CalculationQueued;
                            default:
                                throw new ArgumentException("Unknown CalculationStatusDto; couldn't map");
                        }
                    });
            });
            var mapper = config.CreateMapper();
            return mapper;
        }
    }
}
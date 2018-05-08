using Ajf.CoreSolver.Shared;
using Ajf.CoreSolver.Shared.Service;
using Ajf.Nuget.Logging;
using Ajf.Nuget.TopShelf;
using AutoMapper;
using Serilog;

namespace Ajf.CoreSolver.Service
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Log.Logger = StandardLoggerConfigurator.GetEnrichedLogger();

            using (var wrapper = new TopshelfWrapper<Worker>(
                () =>
                {
                    Mapper.Initialize(cfg =>
                    {
                        //cfg.CreateMap<FileProcessedEvent, FileReadyForCleanupEvent>();
                        //cfg.CreateMap<FileUploadedEvent, FileReadyForProcessingEvent>();
                        //cfg.CreateMap<FileReadyForProcessingEvent, FileProcessedEvent>();
                    });
                },
                s =>
                {
                    var appSettings = new AppSettings();
                    IBusAdapter bus = new BusAdapter(appSettings);

                    s.ConstructUsing(name =>
                    {
                        var dbContextProvider = new DbContextProvider();
                        var calculationRepository =
                            new CalculationRepository(dbContextProvider, MapperProvider.GetMapper());
                        var handleCalculationRequested =
                            new HandleCalculationRequested(bus, appSettings, calculationRepository);

                        return new Worker(bus, handleCalculationRequested);
                    });
                }))
            {
                wrapper.Run();
            }
        }
    }
}
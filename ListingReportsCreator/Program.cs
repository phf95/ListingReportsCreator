using Microsoft.Extensions.DependencyInjection;
using System;

namespace ListingReportsCreator
{
    class Program
    {
        static void Main(string[] args)
    {
        // Create service collection and configure our services
        var services = ConfigureServices();
        // Generate a provider
        var serviceProvider = services.BuildServiceProvider();

        // Kick off actual code
        serviceProvider.GetService<StartUp>().Run();
    }

    private static IServiceCollection ConfigureServices()
    {
        IServiceCollection services = new ServiceCollection();
        services.AddTransient<BusinessLogic.Services.IListingReports, BusinessLogic.ListingReports>();
        services.AddTransient<Infrastructure.CsvHelper.ICsvHelper, Infrastructure.CsvHelper.CsvHelper>();

        services.AddTransient<StartUp>();
        return services;
    }
}
}

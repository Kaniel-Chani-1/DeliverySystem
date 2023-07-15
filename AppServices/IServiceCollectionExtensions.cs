using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Repositories;
using Microsoft.Extensions.Configuration;
using AppServices.Profiles;
using AutoMapper;

namespace AppServices
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection,IConfiguration configuration)
        {
            serviceCollection.AddScoped<IGeneticServices, GeneticServices>();
            serviceCollection.AddScoped<IAlgorithmService, AlgorithmService>();
            serviceCollection.AddScoped<IClientService, ClientService>();
            serviceCollection.AddScoped<IExcelOrdersService, ExcelOrdersService>();
            serviceCollection.AddScoped<IIndividualServices, IndividualServices>();
            serviceCollection.AddScoped<IInlayService, InlayService>();
            serviceCollection.AddScoped<IClientService, ClientService>();
            serviceCollection.AddScoped<IEmployeeService, EmployeeService>();
            serviceCollection.AddScoped<IStatusOrderService, StatusOrderService>();
            serviceCollection.AddScoped<IMapService, MapsService>();
            serviceCollection.AddAutoMapper(typeof(ClientProfile));
            serviceCollection.AddRepositories(configuration);
            return serviceCollection;
        }
    }
}

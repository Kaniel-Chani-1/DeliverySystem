using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Repositories.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Repositories
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection serviceCollection,IConfiguration configuration)
        {
            serviceCollection.AddScoped<IOrdersRepository, OrderRepository>();
            serviceCollection.AddScoped<IClientRepository, ClientsRepository>();
            serviceCollection.AddScoped<ICityRepository, CityRepository>();
            serviceCollection.AddScoped<ICarsRepository, CarsRepository>();
            serviceCollection.AddScoped<IDetailsInlayRepository, DetailsInlaysRepository>();
            serviceCollection.AddScoped<IDetailsOfTheContentsOfReservationRepository, DetailsOfTheContentsOfReservationRepository>();
            serviceCollection.AddScoped<IDifferentShipingAdressRepository, DifferentShipingAdressRepository>();
            serviceCollection.AddScoped<IEmployeesRepository, EmployeesRepository>();
            serviceCollection.AddScoped<IInlaysRepository, InlaysRepository>();
            serviceCollection.AddScoped<IPackingTypesRepository, PackingTypesRepository>();
            serviceCollection.AddScoped<IStatusOrderRepository, StatusOrderRepository>();
            serviceCollection.AddScoped<IDetailsOfShiftsRepository, DetailsOfShiftsRepository>();
            serviceCollection.AddScoped<IShiftsRepository, ShiftsRepository>();
            //serviceCollection.AddDbContext<DeliverySystemContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DeliverySystemConnection")));
            // serviceCollection.AddDbContext<DeliverySystemContext>(opt => opt.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=H:\‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏ASP3-4.6.21\DeliverySystemSolution\DB\DeliverySystem.mdf;Integrated Security=True;Connect Timeout=30‏‏‏‏‏‏‏‏‏‏"));
            //return serviceCollection;
            //serviceCollection.AddDbContext<DeliverySystemContext>(opt => opt.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=Scaffold-DbContext 'Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=H:\‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏ASP3-4.6.21\DeliverySystemSolution\DB\DeliverySystem.mdf‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏;Integrated Security=True;Connect Timeout=30'  Microsoft.EntityFrameworkCore.SqlServer –Context DeliverySystemContext -OutputDir Models  -ForceDeliverySystem.mdf;Integrated Security=True;Connect Timeout=30"));
            serviceCollection.AddDbContext<DeliverySystemContext>(opt => opt.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\DB\DeliverySystem.mdf;Integrated Security=True;Connect Timeout=30"));
           // serviceCollection.AddDbContext<DeliverySystemContext>(opt => opt.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏‏ASP3-11.6.21\DeliverySystemSolution\DB\DeliverySystem.mdf;Integrated Security=True;Connect Timeout=30"));
            return serviceCollection;
        }
    }
}

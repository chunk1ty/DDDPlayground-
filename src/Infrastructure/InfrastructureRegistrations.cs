﻿using Domain.Aggregates.CarAdAggregate.Contracts;
using Domain.Aggregates.DealerAggregate.Contracts;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureRegistrations
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CarRentalDbContext>(c => c.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<ICarAdWriteRepository, CarAdWriteRepository>();
            services.AddScoped<ICarAdReadRepository, CarAdReadRepository>();
            services.AddScoped<IDealerRepository, DealerRepository>();
            services.AddScoped(typeof(EfGenericRepository<>));
        }
    }
}

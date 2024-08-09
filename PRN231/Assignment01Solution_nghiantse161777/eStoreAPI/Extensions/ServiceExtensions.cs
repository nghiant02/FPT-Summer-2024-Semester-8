using BusinessObject.Models;
using DataAccess;
using DataAccess.Interface;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceExtensions
{
    public static IServiceCollection AddScopeService(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure Database Context
        services.AddDbContext<eStoreDBContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnectionStringDB")));

        // Add Repositories
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();

        // Add DAO
        services.AddScoped<MemberDAO>();
        services.AddScoped<ProductDAO>();
        services.AddScoped<OrderDAO>();
        services.AddScoped<OrderDetailDAO>();

        return services;
    }
}

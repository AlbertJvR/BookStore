using BookStore.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookStore.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<BookStoreDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("BookStore")));
        
        // If you remove this interface, the below registration is unnecessary as 'AddDbContext' registers with the DI,
        // and adding this again will cause weird behaviour and all exceptions are annoyingly swallowed
        services.AddScoped<IBookStoreDbContext, BookStoreDbContext>();
    
        return services;
    }
}
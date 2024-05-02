using Books.BL.Mapping;
using Common.Domain;
using Common.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Books.BL;

public static class BooksServicesDi
{
    public static IServiceCollection AddBooksServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperProfile));
        services.AddTransient<IBooksService, BooksService>();
        services.AddTransient<IRepository<User>, BaseRepository<User>>();
        services.AddTransient<IRepository<Book>, BaseRepository<Book>>();
        return services;
    }
}
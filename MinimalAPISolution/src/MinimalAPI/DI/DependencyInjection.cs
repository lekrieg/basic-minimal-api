using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MinimalAPI.Data;
using MinimalAPI.Data.Repositories;
using MinimalAPI.Mapping;
using MinimalAPI.UseCases;
using MinimalAPI.UseCases.Interfaces;

namespace MinimalAPI.DI;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("ConnectionString"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

        services.AddAutoMapper(typeof(MappingProfile));

        services.AddScoped<ITodoRepository, TodoRepository>();

        services.AddScoped<IGetAllTodoUseCase, GetAllTodoUseCase>();
        services.AddScoped<IGetAllCompleteTodoUseCase, GetAllCompleteTodoUseCase>();
        services.AddScoped<IGetByIdTodoUseCase, GetByIdTodoUseCase>();
        services.AddScoped<IAddTodoUseCase, AddTodoUseCase>();
        services.AddScoped<IUpdateTodoUseCase, UpdateTodoUseCase>();
        services.AddScoped<IDeleteTodoUseCase, DeleteTodoUseCase>();

        return services;
    }
}

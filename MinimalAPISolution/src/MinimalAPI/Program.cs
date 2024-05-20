using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MinimalAPI.ExtensionMethods;
using MinimalAPI.Dtos;
using MinimalAPI.UseCases;
using MinimalAPI.UseCases.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddServices(builder.Configuration);

        // Add OpenApi
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure Swagger
        if(app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // Mapping endpoints
        app.RegisterTodoItemsEndpoints();

        app.Run();
    }
}
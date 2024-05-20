using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Dtos;
using MinimalAPI.UseCases.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinimalAPI.ExtensionMethods;

public static class TodoItemsEndpoints
{
    public static void RegisterTodoItemsEndpoints(this WebApplication app)
    {
        var todoItems = app.MapGroup("/TodoItems");
        todoItems.MapGet("/", GetAllTodo).WithOpenApi().Produces<IEnumerable<TodoDTO>>(StatusCodes.Status200OK).Produces(StatusCodes.Status400BadRequest);
        todoItems.MapGet("/Complete", GetAllCompleteTodo).WithOpenApi().Produces<IEnumerable<TodoDTO>>(StatusCodes.Status200OK).Produces(StatusCodes.Status400BadRequest);
        todoItems.MapGet("/{id}", GetByIdTodo).WithOpenApi().Produces<TodoDTO>(StatusCodes.Status200OK).Produces(StatusCodes.Status404NotFound);
        todoItems.MapPost("/", AddTodo).WithOpenApi().Produces<TodoDTO>(StatusCodes.Status201Created).Produces(StatusCodes.Status400BadRequest);
        todoItems.MapPut("/{id}", UpdateTodo).WithOpenApi().Produces(StatusCodes.Status204NoContent);
        todoItems.MapDelete("/{id}", DeleteTodo).WithOpenApi().Produces(StatusCodes.Status404NotFound);
    }

    static async Task<IResult> GetAllTodo(IGetAllTodoUseCase useCase)
    {
        var result = await useCase.Execute();
        if (result == null || !result.Any())
        {
            return TypedResults.BadRequest();
        }
        return TypedResults.Ok(result);
    }

    static async Task<IResult> GetAllCompleteTodo(IGetAllCompleteTodoUseCase useCase)
    {
        var result = await useCase.Execute();
        if (result == null || !result.Any())
        {
            return TypedResults.BadRequest();
        }
        return TypedResults.Ok(result);
    }

    static async Task<IResult> GetByIdTodo([FromRoute] int id, IGetByIdTodoUseCase useCase)
    {
        var result = await useCase.Execute(id);
        if (result == null)
        {
            return TypedResults.NotFound();
        }
        return TypedResults.Ok(result);
    }

    static async Task<IResult> AddTodo([FromBody] TodoDTO request, IAddTodoUseCase useCase)
    {
        var result = await useCase.Execute(request);
        if (result == null)
        {
            return TypedResults.BadRequest();
        }
        return TypedResults.Created($"/TodoItems/{result.Id}", result);
    }

    static async Task<IResult> UpdateTodo([FromRoute] int id, [FromBody] TodoDTO request, IUpdateTodoUseCase useCase)
    {
        request.Id = id;
        await useCase.Execute(request);

        return TypedResults.NoContent();
    }

    static async Task<IResult> DeleteTodo([FromRoute] int id, IDeleteTodoUseCase useCase)
    {
        await useCase.Execute(id);

        return TypedResults.NotFound();
    }
}

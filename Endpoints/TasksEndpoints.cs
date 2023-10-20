using Dapper.Contrib.Extensions;
using static TasksApiDapper.Data.TaskContext;

namespace TasksApiDapper.Endpoints;

public static class TasksEndpoints
{
    public static void MapTasksEndpoints(this WebApplication app)
    {
        app.MapGet("/", () => $"Bem vindo a API de tarefas {DateTime.Now}");

        app.MapGet("/tasks", async (GetConnection connectionGatter) =>
        {
            using var con = await connectionGatter();
            var tasks = con.GetAll<Task>().ToList();

            if (tasks is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(tasks);     
        });

        app.MapGet("/taks/{id}", async (GetConnection connectionGetter, int id) =>
        {
            using var con = await connectionGetter();
            var task = con.Get<Task>(id);
            return Results.Ok(task);
        });

        app.MapPost("/tasks", async (GetConnection connectionGetter, Task Task) =>
        {
            using var con = await connectionGetter();
            var id = con.Insert(Task);
            return Results.Created($"/tasks/{id}", Task);  
        });

        app.MapPut("/tasks", async (GetConnection connectionGetter, Task Task) =>
        {
            using var con = await connectionGetter();
            var id = con.Update(Task);
            return Results.Ok();
        });

        app.MapDelete("/tasks/{id}", async (GetConnection connectionGetter, int id) =>
        {
            using var con = await connectionGetter();
            var deleted = con.Get<Task>(id);

            if(deleted is null)
                return Results.NotFound();

            con.Delete(deleted);
            return Results.Ok(deleted);

        });


    }
}

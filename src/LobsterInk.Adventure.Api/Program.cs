using LobsterInk.Adventure.Api.Extensions;
using LobsterInk.Adventure.Api.Middleware;
using LobsterInk.Adventure.Infrastructure.DataAccess.Migrations;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services
    .AddApiControllers()
    .AddVersioning()
    .AddSwagger()
    .AddUseCases()
    .AddPresenters()
    .AddServices()
    .AddValidators()
    .AddDomainServices()
    .AddRepositories()
    .AddSqlServer(builder.Configuration);

var app = builder.Build();

app.UseSwaggerDocumentation();
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

DbMigration.Perform(builder.Configuration.GetConnectionString("DefaultConnection"));

app.Run();
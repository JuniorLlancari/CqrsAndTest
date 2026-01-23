using Azure.Identity;
using CQRS.Api.ExceptionHandlers;
using CQRS.Application;
using CQRS.Application.Mapping;
using CQRS.External;
using CQRS.Persistence;
using CQRS.Persistence.Data;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();




var keyVaultUrl = Environment.GetEnvironmentVariable("KEYVAULTURL") ?? string.Empty;
builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUrl), new DefaultAzureCredential());




builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();


builder.Services
    .AddApplication()
    .AddExternal(builder.Configuration)
    .AddPersistencie(builder.Configuration);


builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddCors(o => o.AddPolicy("corsApp", builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();
app.UseCors("corsApp");
app.UseExceptionHandler();



await DataSeeder.CreateDbIfNotExists(app);








if (app.Environment.IsDevelopment() || true)
{
    app.UseSwagger();
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

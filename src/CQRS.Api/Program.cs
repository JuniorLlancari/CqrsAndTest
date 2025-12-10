using Azure.Identity;
using CQRS.Api.ExceptionHandlers;
using CQRS.Application;
using CQRS.Application.Mapping;
using CQRS.External;
using CQRS.Persistence;
using CQRS.Persistence.Data;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);


var keyVaultUrl = builder.Configuration["keyVaultUrl"] ?? string.Empty;


builder.Services.AddControllers();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();


 

if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
{
    string tenantId = Environment.GetEnvironmentVariable("TENANT_TD") ?? string.Empty;
    string clientId = Environment.GetEnvironmentVariable("CLIENT_ID") ?? string.Empty;
    string clientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET") ?? string.Empty;

    var tokenCredentials = new ClientSecretCredential(tenantId, clientId, clientSecret);
    builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUrl), tokenCredentials);
}
else
{
    //builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUrl), new DefaultAzureCredential());

}



 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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





using (var scope = app.Services.CreateScope())
{
    var scopedProvider = scope.ServiceProvider;
    try
    {
        var context = scopedProvider.GetRequiredService<CQRSDbContext>();
        await CQRSDbContextSeed.InitialiseDatabaseAsync(context);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, "An error occurred seeding the DB.");
    }
}






if (app.Environment.IsDevelopment() || true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

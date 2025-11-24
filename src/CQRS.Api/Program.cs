using CQRS.Application;
using CQRS.Application.Mapping;
using CQRS.Persistence;
using CQRS.Persistence.Data;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddApplication();
builder.Services.AddPersistencie(builder.Configuration);


//builder.Services.AddMediatR(typeof(GetCursoQuery.GetCursoQueryHandler).Assembly);
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddCors(o => o.AddPolicy("corsApp", builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

app.UseCors("corsApp");





//using (var scope = app.Services.CreateScope())
//{
//    var scopedProvider = scope.ServiceProvider;
//    try
//    {
//        var context = scopedProvider.GetRequiredService<CQRSDbContext>();
//        await CQRSDbContextSeed.InitialiseDatabaseAsync(context);
//    }
//    catch (Exception ex)
//    {
//        app.Logger.LogError(ex, "An error occurred seeding the DB.");
//    }
//}






if (app.Environment.IsDevelopment() || true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

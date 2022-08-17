using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using VShop.ProductApi.Configuration;
using VShop.ProductApi.Context;
using VShop.ProductApi.Repositorio;
using VShop.ProductApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x => 
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwagger();

var conection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseMySql(conection, ServerVersion.AutoDetect(conection)));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryServices, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddCors(opt => 
{
    opt.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("https://localhost:7175")
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    );
});

builder.Services.AdicionarAuthentication(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API VShop v1");
    });
}



app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();

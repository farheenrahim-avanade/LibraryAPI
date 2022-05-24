using LibraryAPI.Data;
using LibraryAPI.Data.Repositories;
using LibraryAPI.Models;
using LibraryAPI.Services;
using Microsoft.EntityFrameworkCore;


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins"; //cors
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<LibraryItemContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ICrudRepository<LibraryItem, int>, LibraryItemRepository>();
builder.Services.AddScoped<ICrudService<LibraryItem, int>, LibraryItemService>();
builder.Services.AddScoped<ICrudRepository<User, int>, UserRepository>();
builder.Services.AddScoped<ICrudService<User, int>, UserService>();
builder.Services.AddControllers();

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Adding cors
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);//To EnableCors - CrossOrigin

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

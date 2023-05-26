using Microsoft.EntityFrameworkCore;
using MultiTenantTest.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<TestDbContext>(options =>
//{
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
//});
builder.Services.AddScoped<TenantProvider>();
builder.Services.Configure<TenantSettings>(builder.Configuration.GetSection("TenantSettings"));
builder.Services.AddDbContext<TestDbContext>((serviceProvider, options) =>
{
    var tenantProvider = serviceProvider.GetRequiredService<TenantProvider>();
    var connectionString = tenantProvider.GetConnectionString();
    options.UseSqlServer(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

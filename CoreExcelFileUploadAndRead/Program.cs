using CoreExcelFileUploadAndRead.Database;
using CoreExcelFileUploadAndRead.Mapping;
using CoreExcelFileUploadAndRead.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var dbConnectionString = builder.Configuration["ConnectionStrings:SQLServer"];

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DatabaseContext>(
    options => options.UseSqlServer(
        dbConnectionString,
        x => x.MigrationsAssembly(typeof(DatabaseContext).Assembly.FullName)));
builder.Services.AddTransient<ExcelFileUploader>();
builder.Services.AddTransient<ExcelFileLoader>();
builder.Services.AddAutoMapper(typeof(MapProfile));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/File/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=File}/{action=Index}/{id?}");

app.Run();

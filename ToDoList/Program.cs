using ToDoList.Data;
using Microsoft.EntityFrameworkCore;
using ToDoList.Interfaces;
using ToDoList.Observer;
using ToDoList.Services;
using System.Threading;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ToDoListDBContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));
builder.Services.AddHttpClient();
builder.Services.AddScoped<TaakEditListener>();
builder.Services.AddScoped<TaakDeleteListener>();
builder.Services.AddScoped<Taskmanager>();
builder.Services.AddScoped<TaakContext>();
ThreadPoolConfig.ConfigureThreadPool(builder.Services);
builder.Services.AddScoped<ITaskRepository, TaakRepository>();
builder.Services.AddScoped<ApiFacade>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

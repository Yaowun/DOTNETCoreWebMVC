using Microsoft.EntityFrameworkCore;
using TodoList.Data;

var builder = WebApplication.CreateBuilder(args);

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

if (environment == "Production")
{
    //var connectionString = Environment.GetEnvironmentVariable("DB_URL");

    //if (string.IsNullOrEmpty(connectionString))
    //{
    //    throw new InvalidOperationException("DB_URL environment variable is not set in Render.");
    //}
    
    var dbUser = Environment.GetEnvironmentVariable("DB_USER");
    var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
    var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
    var dbPort = Environment.GetEnvironmentVariable("DB_PORT");
    var dbName = Environment.GetEnvironmentVariable("DB_NAME");
    var connectionString = $"User ID={dbUser};Password={dbPassword};Host={dbHost};Port={dbPort};Database={dbName};";

    builder.Services.AddDbContext<TodoContext>(options => options.UseNpgsql(connectionString));
}
else
{
    var connectionString = builder.Configuration.GetConnectionString("TodoContext");

    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("ConnectionStrings:TodoContext is not set in appsettings.json.");
    }

    builder.Services.AddDbContext<TodoContext>(options => options.UseNpgsql(connectionString));
    //builder.Services.AddDbContext<TodoContext>(options => options.UseSqlServer(connectionString));
}

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

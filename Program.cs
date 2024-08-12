using BookLoanApp.Data;

using BookLoanApp.Services.Authentication;
using BookLoanApp.Services.BookService;
using BookLoanApp.Services.HomeService;
using BookLoanApp.Services.LoanService;
using BookLoanApp.Services.ReportService;
using BookLoanApp.Services.SessionService;
using BookLoanApp.Services.UserService;
using Microsoft.EntityFrameworkCore;
using Npgsql;


var builder = WebApplication.CreateBuilder(args);

// Configurar o DbContext usando a string de conexão
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql("Host=viaduct.proxy.rlwy.net;Port=20177;Username=postgres;Password=dITRyJVqrrSIvtQNHqHtlzFvhIzMlFCA;Database=railway;SslMode=Require;Trust Server Certificate=True");
});


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IBookInterface, BookService >();
builder.Services.AddScoped<IUserInterface, UserService>();
builder.Services.AddScoped<IAuthenticationInterface, AuthenticationService>();
builder.Services.AddScoped<ISessionInterface, SessionService>();
builder.Services.AddScoped<IHomeInterface, HomeService>();
builder.Services.AddScoped<ILoanInterface, LoanService>();
builder.Services.AddScoped<IReportInterface, ReportService>();

builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});



var app = builder.Build();

// Aplicar migrações ao iniciar a aplicação
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        dbContext.Database.Migrate();
        Console.WriteLine("Migrações aplicadas com sucesso.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao aplicar migrações: {ex.Message}");
    }
}
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

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

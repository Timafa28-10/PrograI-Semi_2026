using ConectaRH.Business.Services;
using ConectaRH.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ---------------- Capa de presentación (MVC) ----------------
builder.Services.AddControllersWithViews(o => o.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));

// ---------------- Capa de datos (MySQL) ----------------
var cs = builder.Configuration.GetConnectionString("ConectaRH")
         ?? throw new InvalidOperationException("Falta la cadena de conexión 'ConectaRH' en appsettings.json");
builder.Services.AddDbContext<ConectaRhContext>(o =>
    o.UseMySql(cs, new MySqlServerVersion(new Version(8, 0, 36))));

// ---------------- Capa de lógica de negocio ----------------
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<CandidatoService>();
builder.Services.AddScoped<EmpresaService>();
builder.Services.AddScoped<OfertaService>();
builder.Services.AddScoped<PostulacionService>();
builder.Services.AddScoped<AdminService>();

// ---------------- Autenticación por cookie ----------------
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(o =>
    {
        o.LoginPath = "/Cuenta/Login";
        o.AccessDeniedPath = "/Cuenta/Login";
        o.ExpireTimeSpan = TimeSpan.FromDays(14);
        o.SlidingExpiration = true;
        o.Cookie.HttpOnly = true;
        o.Cookie.Name = "ConectaRH.Auth";
    });

var app = builder.Build();

// Crea las tablas si no existen y siembra roles + administrador inicial
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ConectaRhContext>();
    await db.Database.EnsureCreatedAsync();

    var adminCfg = builder.Configuration.GetSection("AdministradorInicial");
    var adminCorreo = adminCfg["Correo"] ?? "admin@conectarh.com";
    var adminPass = adminCfg["Contrasena"] ?? "Admin12345";
    var adminNombre = adminCfg["Nombre"] ?? "Administrador";
    await DbSeeder.SembrarAsync(db, adminNombre, adminCorreo, AuthService.GenerarHash(adminPass));
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

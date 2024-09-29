using Atak.Application.Services;
using Atak.Core.Entities;
using Atak.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Net;
using System.Net.Mail;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSingleton(s =>
{
    var configuracao = s.GetRequiredService<IConfiguration>();
    return new SmtpClient(configuracao["Smtp:Host"])
    {
        Port = int.Parse(configuracao["Smtp:Port"]),
        Credentials = new NetworkCredential(
            configuracao["Smtp:Username"],
            configuracao["Smtp:Password"]),
        EnableSsl = bool.Parse(configuracao["Smtp:EnableSsl"])
    };
});


builder.Services.AddScoped<EmailService>();
builder.Services.AddDbContext<ApplicationDbContext>(opcoes =>
    opcoes.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<UsuarioAplicacao, IdentityRole<int>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddTransient<GeradorDeDados>();
builder.Services.AddTransient<ServicoDeExcel>();

builder.Services.AddControllersWithViews();

var app = builder.Build();


var culturasSuportadas = new[] { "pt-BR" };
var opcoesLocalizacao = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("pt-BR"),
    SupportedCultures = culturasSuportadas.Select(cultura => new CultureInfo(cultura)).ToList(),
    SupportedUICultures = culturasSuportadas.Select(cultura => new CultureInfo(cultura)).ToList()
};

app.UseRequestLocalization(opcoesLocalizacao);


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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

using System.IdentityModel.Tokens.Jwt;
using System.Text;
using FarmToFork.Business;
using FarmToFork.Business.DTOs;
using FarmToFork.Core;
using FarmToFork.Core.Models;
using FarmToFork.Infrastructure;
using FarmToFork.Infrastructure.Context;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection").ToString();


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 8;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
});


builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(connectionString)
);

builder.Services.AddDbContext<AppDbContext>(c =>
    c.UseSqlServer(connectionString));

builder.Services.AddIdentity<ApplicationUser, ApplicationUserRole>().AddRoles<ApplicationUserRole>()
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();




// Add services to the container.

builder.Services.AddControllersWithViews();

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    })
    .AddJwtBearer(cfg =>
    {
        cfg.RequireHttpsMetadata = false;
        cfg.SaveToken = true;
        cfg.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["JwtIssuer"],
            ValidAudience = builder.Configuration["JwtIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtKey"])),
            ClockSkew = TimeSpan.Zero // remove delay of token when expire
        };
    });

builder.Services.AddServicesFromInfrastructure(builder.Configuration);
builder.Services.AddServicesFromBusiness(builder.Configuration);

var directory = System.AppContext.BaseDirectory.Split(Path.DirectorySeparatorChar);
var slice = new ArraySegment<string>(directory, 0, directory.Length - 4);
var path = Path.Combine(slice.ToArray());

bool exists = System.IO.Directory.Exists($"{path}/Images");
bool sqlLiteDbExist = System.IO.Directory.Exists($"{path}/Data");

if (!exists)
{
    System.IO.Directory.CreateDirectory($"{path}/Images");
}

if (!sqlLiteDbExist)
{
    System.IO.Directory.CreateDirectory($"{path}/Data");
}

//builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, CustomClaimsPrincipalFactory>();

//CORS
builder.Services.AddCors(o => o.AddPolicy("AllowAllCORS", builder =>
{
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
}));

builder.Services.AddSwaggerGen();

builder.Services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<RegisterModelValidator>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors("AllowAllCORS");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseStaticFiles(
    new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(app.Environment.ContentRootPath, "Images")),
        RequestPath = "/images"
    }
);
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("v1/swagger.json", "MyAPI V1");
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");



app.MapFallbackToFile("index.html"); ;

app.Run();

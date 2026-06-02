using Microsoft.EntityFrameworkCore;
using CanopyViewer.Data;
using CanopyViewer.Models;
using CanopyViewer.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System;

var builder = WebApplication.CreateBuilder(args);

//use env variables to build connection string
string connectionString;

var host = Environment.GetEnvironmentVariable("PGHOST");
var port = Environment.GetEnvironmentVariable("PGPORT") ?? "5432";
var database = Environment.GetEnvironmentVariable("PGDATABASE");
var username = Environment.GetEnvironmentVariable("PGUSER");
var password = Environment.GetEnvironmentVariable("PGPASSWORD");

Console.WriteLine(host);
Console.WriteLine(port);
Console.WriteLine(database);
Console.WriteLine(username);
Console.WriteLine(password);

connectionString = $"Host={host};Port={port};Database={database};Username={username};Password={password};SslMode=Require";
Console.WriteLine(connectionString);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<AppDbContext>(options =>
   options.UseNpgsql(connectionString));

// Cookie authentication for sessions
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
    CookieAuthenticationDefaults.AuthenticationScheme, options =>
        {
            options.LoginPath = "/Login";
            options.LogoutPath = "/Logout";

        });

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();
});
builder.Services.AddScoped<EmailService>();
builder.Services.AddHostedService<RecurringWorkOrderService>();
var app = builder.Build();

// Test users for logins
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (db.Users.Count() <= 2)
    {
        db.Users.Add(new User
        {
            Username = "admin",
            PasswordHash = PasswordService.Hash("admin123"),
            Role = Role.Admin
        });

        db.Users.Add(new User
        {
            Username = "user",
            PasswordHash = PasswordService.Hash("user123"),
            Role = Role.User
        });

        db.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();

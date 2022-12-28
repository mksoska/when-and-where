using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using WhenAndWhere.BL;
using WhenAndWhere.BL.Facades;
using WhenAndWhere.BL.Services;
using WhenAndWhere.DAL;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.Infrastructure.EFCore.Repository;
using WhenAndWhere.Infrastructure.EFCore.UnitOfWork;
using WhenAndWhere.Infrastructure.Repository;
using WhenAndWhere.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Components;
using WhenAndWhere.Blazor.Authorization;
using RouteData = Microsoft.AspNetCore.Components.RouteData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var sqliteConnection = new SqliteConnection("Data Source=../WhenAndWhere.DAL/WhenAndWhere.sqlite;Cache=Shared");
sqliteConnection.Open();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new EFCoreModule(sqliteConnection));
    builder.RegisterAutoMapper();
});

builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<WhenAndWhereDBContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ShowPolicy", policy =>
        policy.Requirements.Add(new OperationAuthorizationRequirement()));
});
builder.Services.AddSingleton<IAuthorizationHandler, MeetupOwnerAuthHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, MeetupAdminAuthHandler>();


builder.Services.AddDbContext<WhenAndWhereDBContext>(builder => builder.UseSqlite("Data Source=../WhenAndWhere.DAL/WhenAndWhere.sqlite;Cache=Shared"));
builder.Services.AddTransient<DbContext>(x => x.GetRequiredService<WhenAndWhereDBContext>());
builder.Services.AddTransient<IUnitOfWork>(x => x.GetRequiredService<EFUnitOfWork>());
builder.Services.AddTransient<IRepository<Meetup>, EFGenericRepository<Meetup>>();
builder.Services.AddTransient<MeetupService>();
builder.Services.AddTransient<WhenAndWhereFacade>();

var app = builder.Build();

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
using System.Collections.Immutable;
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
using System.Linq;
using WhenAndWhere.BL.DTOs;
using WhenAndWhere.BL.Query;
using WhenAndWhere.Infrastructure.EFCore.Query;
using WhenAndWhere.Infrastructure.Query;
using RouteData = Microsoft.AspNetCore.Components.RouteData;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

const string connectionString = "Data Source=../WhenAndWhere.DAL/WhenAndWhere.sqlite;Cache=Shared";
var sqliteConnection = new SqliteConnection(connectionString);
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
    options.AddPolicy("MeetupEdit", policy =>
        policy.Requirements = new List<IAuthorizationRequirement>
        {
            Roles.Owner,
            Roles.Administrator
        });
    options.AddPolicy("JoinedUser", policy =>
        policy.Requirements = new List<IAuthorizationRequirement>
        {
            Roles.User
        });
    options.AddPolicy("MeetupOwner", policy =>
        policy.Requirements = new List<IAuthorizationRequirement>
        {
            Roles.Owner
        });
    options.AddPolicy("ManageParticipants", policy =>
        policy.Requirements = new List<IAuthorizationRequirement>
        {
            Roles.Administrator,
            Roles.Moderator
        });
});

builder.Services.AddSingleton<IAuthorizationHandler, PolicyAuthorizationHandler>();

builder.Services.AddDbContext<WhenAndWhereDBContext>(builder => builder.UseSqlite(connectionString));
builder.Services.AddTransient<DbContext>(x => x.GetRequiredService<WhenAndWhereDBContext>());
builder.Services.AddTransient<IUnitOfWork>(x => x.GetRequiredService<EFUnitOfWork>());

builder.Services.AddTransient<IQuery<Meetup>, EntityFrameworkQuery<Meetup>>();
builder.Services.AddTransient<IQuery<Option>, EntityFrameworkQuery<Option>>();
builder.Services.AddTransient<IQuery<Role>, EntityFrameworkQuery<Role>>();
builder.Services.AddTransient<IQuery<User>, EntityFrameworkQuery<User>>();
builder.Services.AddTransient<IQuery<UserMeetup>, EntityFrameworkQuery<UserMeetup>>();
builder.Services.AddTransient<IQuery<UserOption>, EntityFrameworkQuery<UserOption>>();
builder.Services.AddTransient<IQuery<UserRole>, EntityFrameworkQuery<UserRole>>();

builder.Services.AddTransient<IRepository<Meetup>, EFGenericRepository<Meetup>>();
builder.Services.AddTransient<IRepository<Option>, EFGenericRepository<Option>>();
builder.Services.AddTransient<IRepository<Role>, EFGenericRepository<Role>>();
builder.Services.AddTransient<IRepository<User>, EFGenericRepository<User>>();
builder.Services.AddTransient<IRepository<UserMeetup>, EFGenericRepository<UserMeetup>>();
builder.Services.AddTransient<IRepository<UserOption>, EFGenericRepository<UserOption>>();
builder.Services.AddTransient<IRepository<UserRole>, EFGenericRepository<UserRole>>();

builder.Services.AddTransient<QueryObjectGeneric<MeetupDTO, Meetup>>();
builder.Services.AddTransient<QueryObjectGeneric<OptionDTO, Option>>();
builder.Services.AddTransient<QueryObjectGeneric<RoleDTO, Role>>();
builder.Services.AddTransient<QueryObjectGeneric<UserDTO, User>>();
builder.Services.AddTransient<QueryObjectGeneric<UserMeetupDTO, UserMeetup>>();
builder.Services.AddTransient<QueryObjectGeneric<UserOptionDTO, UserOption>>();
builder.Services.AddTransient<QueryObjectGeneric<UserRoleDTO, UserRole>>();

builder.Services.AddTransient<MeetupService>();
builder.Services.AddTransient<OptionService>();
builder.Services.AddTransient<RoleService>();
builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<UserMeetupService>();
builder.Services.AddTransient<UserOptionService>();
builder.Services.AddTransient<UserRoleService>();

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
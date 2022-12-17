using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using WhenAndWhere.BL;
using WhenAndWhere.BL.Services;
using WhenAndWhere.DAL;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.Infrastructure.EFCore.Repository;
using WhenAndWhere.Infrastructure.EFCore.UnitOfWork;
using WhenAndWhere.Infrastructure.Repository;
using WhenAndWhere.Infrastructure.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var sqliteConnection = new SqliteConnection("Data Source=WhenAndWhere.sqlite;Cache=Shared");
sqliteConnection.Open();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new EFCoreModule(sqliteConnection));
    builder.RegisterAutoMapper();
});

builder.Services.AddDefaultIdentity<User>().AddEntityFrameworkStores<WhenAndWhereDBContext>();
builder.Services.ConfigureApplicationCookie();

builder.Services.AddDbContext<WhenAndWhereDBContext>(builder => builder.UseSqlite("Data Source=WhenAndWhere.sqlite;Cache=Shared"));
builder.Services.AddTransient<DbContext>(x => x.GetRequiredService<WhenAndWhereDBContext>());
builder.Services.AddTransient<IUnitOfWork>(x => x.GetRequiredService<EFUnitOfWork>());
builder.Services.AddTransient<IRepository<Meetup>, EFGenericRepository<Meetup>>();
builder.Services.AddTransient<MeetupService>();

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
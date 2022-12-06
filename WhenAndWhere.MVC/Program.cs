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

var sqliteConnection = new SqliteConnection("Data Source=WhenAndWhere.sqlite;Cache=Shared");
sqliteConnection.Open();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new EFCoreModule(sqliteConnection));
    builder.RegisterAutoMapper();
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<WhenAndWhereDBContext>(builder => builder.UseSqlite("Data Source=WhenAndWhere.sqlite;Cache=Shared"));
builder.Services.AddTransient<DbContext>(x => x.GetRequiredService<WhenAndWhereDBContext>());
builder.Services.AddTransient<IUnitOfWork>(x => x.GetRequiredService<EFUnitOfWork>());
builder.Services.AddTransient<IRepository<Meetup>, EFGenericRepository<Meetup>>();
builder.Services.AddTransient<MeetupService>();
//builder.Services.AddTransient<ICourseRepository, CourseRepository>();
//builder.Services.AddTransient<IRepository<GradeDto>, GradeRepository>();
//builder.Services.AddTransient<IGradeRepository, GradeRepository>();
//builder.Services.AddTransient<IRepository<StudentDto>, StudentRepository>();
//builder.Services.AddTransient<IStudentRepository, StudentRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
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
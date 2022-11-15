using Autofac;
using AutoMapper;

namespace WhenAndWhere;

internal static class BootstrapperExtensions
{
    internal static void RegisterAutoMapper(this ContainerBuilder builder)
    {
        // Registration of AutoMapper profiles
        // Kudos to Martin Loitzl
        // https://blog.loitzl.com/posts/autofac-and-automapper-a-perfect-match
        // Get all Profiles and add them to the MapperConfiguration
        builder.Register(context =>
        {
            var profiles = context.Resolve<IEnumerable<Profile>>();
            var config = new MapperConfiguration(x =>
            {
                foreach (var profile in profiles)
                {
                    x.AddProfile(profile);
                }
            });

            return config;
        }).SingleInstance().AutoActivate().AsSelf();
        // Resolve the MapperConfiguration and call CreateMapper()  
        builder.Register(context =>
        {
            var ctx = context.Resolve<IComponentContext>();
            var config = ctx.Resolve<MapperConfiguration>();
            return config.CreateMapper(
              t => ctx.Resolve(t) // The magic is happening here: Autofac will now fulfill all dependcies defined in MapperConfigurations
            );
        });
    }
}

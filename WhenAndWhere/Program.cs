using Autofac;
using WhenAndWhere;
using WhenAndWhere.BL.Interfaces;
using WhenAndWhere.BL.Services;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.DTO;
using WhenAndWhere.Infrastructure.Repository;
using WhenAndWhere.Infrastructure.UnitOfWork;

using var _ioc = new Bootstrapper(Bootstrapper.Provider.EFCore);

using (var scope = _ioc.Container.BeginLifetimeScope())
{
    var meetupService = scope.Resolve<MeetupService>();
    var users = meetupService.GetMeetupJoinedUsers(2).Result;
    var roles = meetupService.GetMeetupRoles(2).Result;

    Console.WriteLine("Line");

    users.ForEach(u => Console.WriteLine(u.Name));
    roles.ForEach(r => Console.WriteLine(r.RoleName));
}

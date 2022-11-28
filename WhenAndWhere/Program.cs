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
    var userMeetupService = scope.Resolve<UserMeetupService>();
    var users = meetupService.GetInvitedUsers(2).Result;
    var roles = meetupService.GetRoles(2).Result;

    Console.WriteLine("Line");

    users.ForEach(um => Console.WriteLine(meetupService.GetById(um.MeetupId).Result.Name));
    roles.ForEach(r => Console.WriteLine(r.RoleName));
}

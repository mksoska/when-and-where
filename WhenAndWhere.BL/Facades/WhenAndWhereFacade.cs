using System;
using WhenAndWhere.BL.DTOs;
using WhenAndWhere.BL.Filter;
using WhenAndWhere.BL.Services;
using WhenAndWhere.DAL.Enums;

namespace WhenAndWhere.BL.Facades;

public class WhenAndWhereFacade
{
    private readonly UserService _userService;
    private readonly UserMeetupService _userMeetupService;
    private readonly MeetupService _meetupService;
    private readonly UserOptionService _userOptionService;
    private readonly OptionService _optionService;
    private readonly UserRoleService _userRoleService;
    private readonly RoleService _roleService;

    public WhenAndWhereFacade(
            UserService userService,
            UserMeetupService userMeetupService,
            MeetupService meetupService,
            UserOptionService userOptionService,
            OptionService optionService,
            UserRoleService userRoleService,
            RoleService roleService
        )
    {
        _userService = userService;
        _userMeetupService = userMeetupService;
        _meetupService = meetupService;
        _userOptionService = userOptionService;
        _optionService = optionService;
        _userRoleService = userRoleService;
        _roleService = roleService;
    }

    public async Task<List<UserRoleDTO>> GetMeetupUserRoles(int meetupId)
    {
        var result = new List<UserRoleDTO>();

        foreach (var role in await _meetupService.GetRoles(meetupId))
        {
            result.AddRange(await _roleService.GetAssignedUsers(role.Id));
        }
        
        return result;
    }

    public async Task<List<UserOptionDTO>> GetMeetupUserVotes(int meetupId)
    {
        var result = new List<UserOptionDTO>();

        foreach (var option in await _meetupService.GetOptions(meetupId))
        {
            result.AddRange(await _optionService.GetVoters(option.Id));
        }

        return result;
    }

    public async Task<List<UserRoleDTO>> GetMeetupUserRoles(int userId, int meetupId)
    {
        return (await GetMeetupUserRoles(meetupId)).Where(ur => ur.UserId == userId).ToList();
    }
    
    public async Task<List<UserOptionDTO>> GetMeetupUserVotes(int userId, int meetupId)
    {
        return (await GetMeetupUserVotes(meetupId)).Where(uo => uo.UserId == userId).ToList();
    }

    public async Task ChangeUserRole(int userId, int meetupId, string roleName)
    {
        if (string.IsNullOrEmpty(roleName))
        {
            await DeleteUserRoles(userId, meetupId);
            return;
        }
        var role = _roleService.GetByName(meetupId, roleName);
        if (role != null)
        {
            await DeleteUserRoles(userId, meetupId);
            await _userRoleService.Create(new UserRoleDTO { UserId = userId, RoleId = role.Id });
        }
    }

    public async Task DeleteUserRoles(int userId, int meetupId)
    {
        var meetupRoles = _roleService.GetAllByMeetup(meetupId);

        foreach (var role in meetupRoles)
        {
            await _userRoleService.Delete(userId, role.Id);
        }
    }

    public async Task DeleteOptions(int userId, int meetupId)
    {
        var userOptions = _optionService.GetAllByUserMeetup(userId, meetupId);

        foreach (var option in userOptions)
        {
            await _optionService.Delete(option.Id);
        }
    }

    public async Task DeleteUserVotes(int userId, int meetupId)
    {
        var userVotes = await GetMeetupUserVotes(userId, meetupId);

        foreach (var vote in userVotes)
        {
            await _userOptionService.Delete(vote.UserId, vote.OptionId);
        }
    }
    
    public async Task<bool> HasUserRole(int userId, int meetupId, string roleName)
    {
        var role = _roleService.GetByName(meetupId, roleName);
        if (role == null)
        {
            return false;
        }
        return await _userRoleService.GetById(userId, role.Id) != null;
    }

    public List<MeetupDTO> SearchMeetups(int userId, string searchString, int requestedPageNumber, int pageSize, out int totalItemsCount)
    {
        var query = new QueryFilterDto<UserMeetupDTO>
        {
            Values = new UserMeetupDTO { UserId = userId },
            WhereColumns = new List<string> { "UserId" },
            SortAscending = false,
            SortCriteria = "DateInvited",
            RequestedPageNumber = requestedPageNumber,
            PageSize = pageSize
        };
        var queryResult = _userMeetupService.ExecuteQuery(query);

        totalItemsCount = queryResult.TotalItemsCount;
        
        var userMeetups = queryResult.Items
            .Select(um => _meetupService.GetById(um.MeetupId).Result!)
            .ToList();

        return userMeetups
            .Select(m => new { m, u = _userService.GetById(m.OwnerId).Result! })
            .Where(i =>
                i.m.Name.Contains(searchString) ||
                i.m.Description.Contains(searchString) ||
                i.u.UserName.Contains(searchString) ||
                i.u.FirstName.Contains(searchString) ||
                i.u.Surname.Contains(searchString))
            .Select(i => i.m)
            .ToList();
    }

    public async Task<bool> IsAnyOptionVoted(int userId, int meetupId)
    {
        var meetupOptions = await _meetupService.GetOptions(meetupId);

        foreach (var option in meetupOptions)
        {
            if (_userOptionService.GetById(userId, option.Id) != null)
            {
                return true;
            }
        }

        return false;
    }
}


using System;
using AutoMapper;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.DTO;
using WhenAndWhere.Infrastructure.Repository;

namespace WhenAndWhere.BL.Services;

public class UserOptionService : GenericService<UserOptionDTO, UserOption>
{
    public UserOptionService(IRepository<UserOption> repository, IMapper mapper) : base(repository, mapper)
    {
    }

    //public async Task AddVote(UserOptionDTO userOption)
    //{
    //    await Create(userOption);
    //}

    //public async Task RemoveVote(int userId, int optionId)
    //{
    //    await Delete(userId, optionId);
    //}
}


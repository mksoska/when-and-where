using AutoMapper;
using WhenAndWhere.BL.Interfaces;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.DTO;
using WhenAndWhere.Infrastructure.UnitOfWork;

namespace WhenAndWhere.BL.Services;

public class MeetupService : IMeetupService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public MeetupService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<MeetupDTO> GetMeetup(int id)
    {
        var meetup = await _unitOfWork.MeetupRepository.GetById(id);
        return _mapper.Map<MeetupDTO>(meetup);
    }

    public async Task CreateMeetup(MeetupDTO meetupDto)
    {
        var meetup = _mapper.Map<Meetup>(meetupDto);
        _unitOfWork.MeetupRepository.Insert(meetup);
        await _unitOfWork.Commit();
    }

    public async Task UpdateMeetup(MeetupDTO meetupDto)
    {
        var meetup = _mapper.Map<Meetup>(meetupDto);
        _unitOfWork.MeetupRepository.Update(meetup);
        await _unitOfWork.Commit();
    }

    public async Task DeleteMeetup(int id)
    {
        _unitOfWork.MeetupRepository.Delete(id);
        await _unitOfWork.Commit();
    }
}
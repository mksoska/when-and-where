using WhenAndWhere.DTO;

namespace WhenAndWhere.BL.Interfaces;

public interface IMeetupService
{
    Task<List<MeetupDTO>> GetAllMeetups();

    Task<MeetupDTO> GetMeetup(int id);

    Task CreateMeetup(MeetupDTO meetupDto);

    Task UpdateMeetup(MeetupDTO meetupDto);

    Task DeleteMeetup(int id);
}

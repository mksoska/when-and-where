using WhenAndWhere.DTO;

namespace WhenAndWhere.BL.Interfaces
{
    public interface IMeetupService
    {

        Task<MeetupDTO> GetMeetup(int id);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhenAndWhere.DAL.Models;

namespace WhenAndWhere.BL.Interfaces
{
    public interface IMeetupService
    {

        Task<MeetupDTO> GetMeetup(int id);
    }
}

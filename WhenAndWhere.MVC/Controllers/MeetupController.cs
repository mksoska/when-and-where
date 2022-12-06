using AutoMapper;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
using Microsoft.AspNetCore.Mvc;
using WhenAndWhere.BL.DTOs;
using WhenAndWhere.DAL.Models;
using WhenAndWhere.Infrastructure.Repository;
using WhenAndWhere.MVC.Models;

namespace WhenAndWhere.MVC.Controllers
{
    public class MeetupController : Controller
    {
        private readonly IRepository<Meetup> _meetupRepository;
        private readonly IMapper _mapper;

        public MeetupController(IRepository<Meetup> meetupRepository, IMapper mapper)
        {
            _meetupRepository = meetupRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var model = new MeetupsIndexViewModel()
            {
                MeetupDTOs = _mapper.Map<IEnumerable<MeetupDTO>>(await _meetupRepository.GetAll())
            };

            return View(model);
        }
    }
}

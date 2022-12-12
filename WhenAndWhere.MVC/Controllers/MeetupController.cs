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

        [HttpGet]
        public ActionResult Create()
        {
            return View(new MeetupCreateViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> Create(MeetupCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dto = model.ToDto();
            _meetupRepository.Insert(_mapper.Map<Meetup>(dto));

            return RedirectToAction(nameof(Index));
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using AutoMapperExample.DAL.Repositoryes;
using AutoMapperExample.DAL.Models;
using AutoMapper;
using AutoMapperExample.ViewModels;

namespace AutoMapperExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly IRepository<User> _repository;
        public HomeController()
        {
            _repository = new UserRepository();
        }

        [HttpGet]
        public IActionResult Index()
        {
            //создание конфигурации сопоставления
            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, IndexUserViewModel>());
            //Настройка automapper
            var mapper = new Mapper(config);
            //сопоставление
            var users = mapper.Map<List<IndexUserViewModel>>(_repository.GetAll());

            return View(users);
        }

        [HttpPost]
        public ActionResult Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Настройка конфигурации AutoMapper
                var config = new MapperConfiguration(cfg => cfg.CreateMap<CreateUserViewModel, User>()
                    .ForMember("Name", opt => opt.MapFrom(c => c.FirstName + " " + c.LastName))
                    .ForMember("Email", opt => opt.MapFrom(src => src.Login)));
                var mapper = new Mapper(config);
                // Выполняем сопоставление
                User user = mapper.Map<CreateUserViewModel, User>(model);
                _repository.Create(user);
                _repository.Save();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();
            // Настройка конфигурации AutoMapper
            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, EditUserViewModel>()
                    .ForMember("Login", opt => opt.MapFrom(src => src.Email)));
            var mapper = new Mapper(config);
            // Выполняем сопоставление
            EditUserViewModel user = mapper.Map<User, EditUserViewModel>(_repository.Get(id.Value));
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Настройка конфигурации AutoMapper
                var config = new MapperConfiguration(cfg => cfg.CreateMap<EditUserViewModel, User>()
                    .ForMember("Email", opt => opt.MapFrom(src => src.Login)));
                var mapper = new Mapper(config);
                // Выполняем сопоставление
                User user = mapper.Map<EditUserViewModel, User>(model);
                _repository.Update(user);
                _repository.Save();
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}

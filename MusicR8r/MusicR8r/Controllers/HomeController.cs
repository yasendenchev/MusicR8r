using AutoMapper;
using MusicR8r.Models;
using MusicR8r.Models.Home;
using MusicR8r.Services;
using MusicR8r.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;

namespace MusicR8r.Controllers
{
    public class HomeController : Controller
    {
        private IGenreService genreService;
        private readonly IMapper mapper;

        public HomeController(IGenreService genreService, IMapper mapper)
        {
            this.genreService = genreService;
            this.mapper = mapper;
        }

        public ActionResult Index()
        {

            //var genres = this.genreService
            //.GetAllAndDeleted();

            //var model = genres.ProjectTo<GenreViewModel>().ToList();
           
            //var genre = this.service.GetById(new Guid("CEFECC0D-E894-482A-B7F6-1E1FD31D818A"));


            var viewModel = new HomeViewModel()
            {
                //Genres = models
            };

            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
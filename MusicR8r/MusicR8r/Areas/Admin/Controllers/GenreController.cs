using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MusicR8r.Data;
using MusicR8r.Data.Model.Models;
using MusicR8r.Services.Contracts;
using AutoMapper;
using MusicR8r.Models;
using AutoMapper.QueryableExtensions;
using MusicR8r.Areas.Admin.Models;

namespace MusicR8r.Areas.Admin.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreService genreService;
        private readonly IMapper mapper;

        public GenreController(IGenreService genreService, IMapper mapper)
        {
            this.genreService = genreService;
            this.mapper = mapper;
        }

        //private MsSqlDbContext db = new MsSqlDbContext();

        // GET: Admin/Genre
        public ActionResult Index()
        {
            var genres = this.genreService.GetAll();

            var models = genres.ProjectTo<GenreViewModel>();

            return View(models);
        }

        // GET: Admin/Genre/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Genre/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name")] AddGenreViewModel addGenreViewModel)
        {
            if (ModelState.IsValid)
            {
                //genre.Id = Guid.NewGuid();
                //db.Genres.Add(genre);
                //db.SaveChanges();
                var genre = new Genre(addGenreViewModel.Name);
                this.genreService.Add(genre);
                return RedirectToAction("Index");
            }

            return View(addGenreViewModel);
        }

        // GET: Admin/Genre/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Genre genre = this.genreService.GetById((Guid)id);
            if (genre == null)
            {
                return HttpNotFound();
            }
            GenreViewModel viewModel = Mapper.Map<GenreViewModel>(genre);


            //returns genre
            return View(viewModel);
        }

        // POST: Admin/Genre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            this.genreService.DeleteById(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

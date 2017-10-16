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
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MusicR8r.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GenresController : Controller
    {
        private readonly IGenreService genreService;
        private readonly IMapper mapper;

        public GenresController(IGenreService genreService, IMapper mapper)
        {
            if (genreService == null)
            {
                throw new ArgumentNullException();
            }
            if (mapper == null)
            {
                throw new ArgumentNullException();
            }
            this.genreService = genreService;
            this.mapper = mapper;
        }

        // GET: Admin/Genre
        public ActionResult All()
        {
            return View();
        }

        public async Task<ActionResult> ListGenres([DataSourceRequest] DataSourceRequest request)
        {
            var genres = this.genreService.GetAll();

            var models = genres.ProjectTo<GenreViewModel>().ToList();
            DataSourceResult result = await models.ToDataSourceResultAsync(request);
            return Json(result);
        }

        // GET: Admin/Genre/Create
        [ValidateInput(false)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Genre/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create([Bind(Include = "Name")] GenreViewModel genreViewModel)
        {
            if (string.IsNullOrEmpty(genreViewModel.Name))
            {
                ModelState.AddModelError("Name", "Name is required");
            }

            if (Regex.IsMatch(genreViewModel.Name, "^[A-Z][-a-zA-Z]+$"))
            {
                ModelState.AddModelError("Name", "Name must contain only letters");
            }

            if (ModelState.IsValid)
            {
                this.genreService.AddGenre(genreViewModel.Name);

                return RedirectToAction("All");
            }

            return View(genreViewModel);
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

            GenreViewModel genreViewModel = Mapper.Map<GenreViewModel>(genre);

            //returns genre
            return View(genreViewModel);
        }

        // POST: Admin/Genre/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            this.genreService.DeleteById(id);
            return RedirectToAction("All");
        }
    }
}

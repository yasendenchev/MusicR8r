using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MusicR8r.Areas.Admin.Models;
using MusicR8r.Data;
using MusicR8r.Services;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MusicR8r.Data.Model.Models;
using Kendo.Mvc.UI;
using System.Threading.Tasks;
using Kendo.Mvc.Extensions;

namespace MusicR8r.Areas.Admin.Controllers
{
    public class ArtistsController : Controller
    {
        private readonly IArtistService artistService;
        private readonly IMapper mapper;

        public ArtistsController(IArtistService artistService, IMapper mapper)
        {
            this.artistService = artistService;
            this.mapper = mapper;
        }

        // GET: Admin/Artist
        public ActionResult Index()
        {
            //var artists = this.artistService.GetAll();

            //var models = artists.ProjectTo<ArtistViewModel>();
            return View(/*models*/);
        }

        public async Task<ActionResult> ListArtists([DataSourceRequest] DataSourceRequest request)
        {
            var artists = this.artistService.GetAll();

            var models = artists.ProjectTo<ArtistViewModel>().AsEnumerable();
            DataSourceResult result = await models.ToDataSourceResultAsync(request);
            var json = Json(result);
            return json;
        }

        // GET: Admin/Artist/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var artist = this.artistService.GetById((Guid)id);
            var artistViewModel = Mapper.Map<ArtistViewModel>(artist);

            if (artistViewModel == null)
            {
                return HttpNotFound();
            }
            return View(artistViewModel);
        }

        // GET: Admin/Artist/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Artist/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,CountryOfOrigin,Bio")] AddArtistViewModel addArtistViewModel)
        {
            if (ModelState.IsValid)
            {
                var artist = new Artist(addArtistViewModel.Name, addArtistViewModel.CountryOfOrigin, addArtistViewModel.Bio);
                this.artistService.Add(artist);
                return RedirectToAction("Index");
            }

            return View(addArtistViewModel);
        }

        // GET: Admin/Artist/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Artist artist = this.artistService.GetById((Guid)id);

            if (artist == null)
            {
                return HttpNotFound();
            }

            var artistViewModel = Mapper.Map<ArtistViewModel>(artist);

            return View(artistViewModel);
        }

        // POST: Admin/Artist/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CountryOfOrigin,Bio")] ArtistViewModel artistViewModel)
        {
            if (ModelState.IsValid)
            {
                this.artistService.Update(artistViewModel.Id, artistViewModel.Name, artistViewModel.CountryOfOrigin, artistViewModel.Bio);
                return RedirectToAction("Index");
            }
            return View(artistViewModel);
        }

        // GET: Admin/Artist/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Artist artist = this.artistService.GetById((Guid)id);

            ArtistViewModel artistViewModel = Mapper.Map<ArtistViewModel>(artist);

            if (artistViewModel == null)
            {
                return HttpNotFound();
            }

            return View(artistViewModel);
        }

        // POST: Admin/Artist/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            this.artistService.DeleteById(id);
            return RedirectToAction("Index");
        }

        //        protected override void Dispose(bool disposing)
        //        {
        //            if (disposing)
        //            {
        //                db.Dispose();
        //            }
        //            base.Dispose(disposing);
        //        }
    }
}
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

namespace MusicR8r.Areas.Admin.Controllers
{
    public class ArtistController : Controller
    {
        private readonly IArtistService artistService;
        private readonly IMapper mapper;

        public ArtistController(IArtistService artistService, IMapper mapper)
        {
            this.artistService = artistService;
            this.mapper = mapper;
        }

        // GET: Admin/Artist
        public ActionResult Index()
        {
            var artists = this.artistService.GetAll();

            var models = artists.ProjectTo<ArtistViewModel>();
            return View(models);
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

        //        // GET: Admin/Artist/Create
        //        public ActionResult Create()
        //        {
        //            return View();
        //        }

        //        // POST: Admin/Artist/Create
        //        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public ActionResult Create([Bind(Include = "Id,Name,CountryOfOrigin,Bio")] ArtistViewModel artistViewModel)
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                artistViewModel.Id = Guid.NewGuid();
        //                db.ArtistViewModels.Add(artistViewModel);
        //                db.SaveChanges();
        //                return RedirectToAction("Index");
        //            }

        //            return View(artistViewModel);
        //        }

        //        // GET: Admin/Artist/Edit/5
        //        public ActionResult Edit(Guid? id)
        //        {
        //            if (id == null)
        //            {
        //                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //            }
        //            ArtistViewModel artistViewModel = db.ArtistViewModels.Find(id);
        //            if (artistViewModel == null)
        //            {
        //                return HttpNotFound();
        //            }
        //            return View(artistViewModel);
        //        }

        //        // POST: Admin/Artist/Edit/5
        //        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //        [HttpPost]
        //        [ValidateAntiForgeryToken]
        //        public ActionResult Edit([Bind(Include = "Id,Name,CountryOfOrigin,Bio")] ArtistViewModel artistViewModel)
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                db.Entry(artistViewModel).State = EntityState.Modified;
        //                db.SaveChanges();
        //                return RedirectToAction("Index");
        //            }
        //            return View(artistViewModel);
        //        }

        //        // GET: Admin/Artist/Delete/5
        //        public ActionResult Delete(Guid? id)
        //        {
        //            if (id == null)
        //            {
        //                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //            }
        //            ArtistViewModel artistViewModel = db.ArtistViewModels.Find(id);
        //            if (artistViewModel == null)
        //            {
        //                return HttpNotFound();
        //            }
        //            return View(artistViewModel);
        //        }

        //        // POST: Admin/Artist/Delete/5
        //        [HttpPost, ActionName("Delete")]
        //        [ValidateAntiForgeryToken]
        //        public ActionResult DeleteConfirmed(Guid id)
        //        {
        //            ArtistViewModel artistViewModel = db.ArtistViewModels.Find(id);
        //            db.ArtistViewModels.Remove(artistViewModel);
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }

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
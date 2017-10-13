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
using MusicR8r.Services.Providers;
using AutoMapper;
using Kendo.Mvc.UI;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Kendo.Mvc.Extensions;
using MusicR8r.Data.Model.Models;

namespace MusicR8r.Areas.Admin.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly IAlbumService albumService;
        private readonly IMapper mapper;

        public AlbumsController(IAlbumService albumService, IMapper mapper)
        {
            this.albumService = albumService;
            this.mapper = mapper;
        }

        //// GET: Admin/Album
        //public ActionResult All()
        //{
        //    return View();
        //}

        //public async Task<ActionResult> ListAlbums([DataSourceRequest] DataSourceRequest request)
        //{
        //    var albums = this.albumService.GetAll();

        //    var models = albums.ProjectTo<AlbumViewModel>().AsEnumerable();
        //    DataSourceResult result = await models.ToDataSourceResultAsync(request);
        //    return Json(result);
        //}

        // GET: Admin/Album/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Album albumModel = this.albumService.GetById((Guid)id);

            if (albumModel == null)
            {
                return HttpNotFound();
            }

            AlbumViewModel albumViewModel = Mapper.Map<AlbumViewModel>(albumModel);

            return View(albumViewModel);
        }

        // GET: Admin/Album/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Album/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Year,ArtistName")] AlbumViewModel albumViewModel, Guid artistId)
        {
            if (ModelState.IsValid)
            {
                var album = new Album();
                //albumViewModel.Id = Guid.NewGuid();
                //this.albumService.Add()
                //db.SaveChanges();
                //return RedirectToAction("Index");
            }

            return View(albumViewModel);
        }

        public ActionResult All(Guid? artistId)
        {
            if (artistId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            IQueryable<Album> albums = this.albumService.GetByArtist((Guid)artistId);


            if (albums == null)
            {
                return HttpNotFound();
            }

            var albumViewModel = albums.ProjectTo<AlbumViewModel>().AsEnumerable();

            return View(albumViewModel);


        }

        // GET: Admin/Album/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Album album = this.albumService.GetById((Guid)id);

            if (album == null)
            {
                return HttpNotFound();
            }

            var albumViewModel = this.mapper.Map<AlbumViewModel>(album);

            return View(albumViewModel);
        }

        // POST: Admin/Album/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Year")] AlbumViewModel albumViewModel)
        {
            if (ModelState.IsValid)
            {
                this.albumService.Update(albumViewModel.Id, albumViewModel.Name, albumViewModel.Year);
                return RedirectToAction("All");
            }
            return View(albumViewModel);
        }

        // GET: Admin/Album/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = this.albumService.GetById((Guid)id);

            AlbumViewModel albumViewModel = this.mapper.Map<AlbumViewModel>(album);
            if (albumViewModel == null)
            {
                return HttpNotFound();
            }
            return View(albumViewModel);
        }

        // POST: Admin/Album/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Album album = this.albumService.GetById(id);
            AlbumViewModel albumViewModel = this.mapper.Map<AlbumViewModel>(album);
            this.albumService.DeleteById(id);
            return RedirectToAction("All");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}

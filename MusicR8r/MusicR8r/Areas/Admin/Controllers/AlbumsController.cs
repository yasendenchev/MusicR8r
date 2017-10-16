using AutoMapper;
using AutoMapper.QueryableExtensions;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using MusicR8r.Areas.Admin.Models;
using MusicR8r.Data.Model.Models;
using MusicR8r.Services.Providers;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MusicR8r.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AlbumsController : Controller
    {
        private readonly IAlbumService albumService;
        private readonly IMapper mapper;

        public AlbumsController(IAlbumService albumService, IMapper mapper)
        {
            if(albumService == null)
            {
                throw new ArgumentNullException();
            }

            if (mapper == null)
            {
                throw new ArgumentNullException();
            }

            this.albumService = albumService;
            this.mapper = mapper;
        }

        // GET: Admin/Album
        public ActionResult All()
        {
            return View();
        }

        public async Task<ActionResult> ListAlbums([DataSourceRequest] DataSourceRequest request, Guid artistId)
        {
            var albums = this.albumService.GetByArtist(artistId);

            var models = albums.ProjectTo<AlbumViewModel>().AsEnumerable();
            DataSourceResult result = await models.ToDataSourceResultAsync(request);
            var json = Json(result, JsonRequestBehavior.AllowGet);
            return json;
        }

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
                this.albumService.AddAlbum(albumViewModel.Name, albumViewModel.Year, artistId);
                return RedirectToAction("All");
            }

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
    }
}

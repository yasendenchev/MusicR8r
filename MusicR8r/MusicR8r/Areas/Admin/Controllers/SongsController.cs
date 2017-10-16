using AutoMapper;
using AutoMapper.QueryableExtensions;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using MusicR8r.Areas.Admin.Models;
using MusicR8r.Data.Model.Models;
using MusicR8r.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MusicR8r.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SongsController : Controller
    {
        private readonly ISongService songService;
        private readonly IGenreService genreService;
        private readonly IMapper mapper;

        public SongsController(ISongService songService, IGenreService genreService, IMapper mapper)
        {
            if (songService == null)
            {
                throw new ArgumentNullException();
            }
            if (genreService == null)
            {
                throw new ArgumentNullException();
            }
            if (mapper == null)
            {
                throw new ArgumentNullException();
            }
            this.songService = songService;
            this.genreService = genreService;
            this.mapper = mapper;
        }


        // GET: Admin/Songs
        public ActionResult All()
        {
            return View();
        }

        public async Task<ActionResult> ListSongs([DataSourceRequest] DataSourceRequest request, Guid? albumId)
        {
            var songs = this.songService.GetByAlbum((Guid)albumId);

            var models = songs.ProjectTo<SongViewModel>().AsEnumerable();
            DataSourceResult result = await models.ToDataSourceResultAsync(request);
            var json = Json(result, JsonRequestBehavior.AllowGet);
            return json;
        }

        // GET: Admin/Songs/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Song songModel = this.songService.GetById((Guid)id);

            if (songModel == null)
            {
                return HttpNotFound();
            }

            SongViewModel songViewModel = Mapper.Map<SongViewModel>(songModel);

            return View(songViewModel);
        }

        // GET: Admin/Songs/Create
        public ActionResult Create()
        {
            var songViewModel = new SongSubmitViewModel();
            songViewModel.Genres = this.genreService.GetAll().ProjectTo<GenreViewModel>().ToList();
            return View(songViewModel);
        }

        // POST: Admin/Songs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SongSubmitViewModel songViewModel, Guid albumId)
        {
            if (ModelState.IsValid)
            {
                this.songService.AddSong(songViewModel.Name, songViewModel.Minutes * 60 + songViewModel.Seconds, songViewModel.GenreId, albumId);
                return RedirectToAction("All");
            }

            return View(songViewModel);
        }

        // GET: Admin/Songs/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SongSubmitViewModel songSubmitViewModel = this.mapper.Map<SongSubmitViewModel>(this.songService.GetById((Guid)id));
            songSubmitViewModel.Genres = this.genreService.GetAll().ProjectTo<GenreViewModel>().ToList();
            return View(songSubmitViewModel);
        }

        // POST: Admin/Songs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SongSubmitViewModel songViewModel)
        {
            if (ModelState.IsValid)
            {
                this.songService.Update(songViewModel.Id, songViewModel.GenreId, songViewModel.Name, songViewModel.Minutes * 60 + songViewModel.Seconds);
                return RedirectToAction("All");
            }
            return View(songViewModel);
        }

        // GET: Admin/Songs/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SongViewModel songViewModel = this.mapper.Map<SongViewModel>(this.songService.GetById((Guid)id));

            // TODO: IN EVERY CONTROLLER AND ACTION
            if (songViewModel == null)
            {
                return HttpNotFound();
            }
            return View(songViewModel);
        }

        // POST: Admin/Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            this.songService.DeleteById(id);
            return RedirectToAction("All");
        }
    }
}

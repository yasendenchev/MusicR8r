using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MusicR8r.Data;
using MusicR8r.Models;
using MusicR8r.Services.Contracts;
using AutoMapper;
using System.Threading.Tasks;
using Kendo.Mvc.UI;
using AutoMapper.QueryableExtensions;
using Kendo.Mvc.Extensions;

namespace MusicR8r.Controllers
{
    public class SongsController : Controller
    {
        private readonly ISongService songService;

        private readonly IMapper mapper;

        public SongsController(ISongService songService, IMapper mapper)
        {
            if (songService == null)
            {
                throw new ArgumentNullException();
            }
            if (mapper == null)
            {
                throw new ArgumentNullException();
            }
            this.songService = songService;
            this.mapper = mapper;
        }
        // GET: Songs
        public ActionResult All()
        {
            return View();
        }

        public async Task<ActionResult> ListSongs([DataSourceRequest] DataSourceRequest request, Guid? albumId)
        {
            var songs = this.songService.GetByAlbum((Guid)albumId);

            var models = songs.ProjectTo<SongViewModel>().ToList();
            DataSourceResult result = await models.ToDataSourceResultAsync(request);
            var json = Json(result, JsonRequestBehavior.AllowGet);
            return json;
        }

        // GET: Songs/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var song = this.songService.GetById((Guid)id);

            SongViewModel songViewModel = this.mapper.Map<SongViewModel>(song);

            if (songViewModel == null)
            {
                return HttpNotFound();
            }

            return View(songViewModel);
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

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

namespace MusicR8r.Controllers
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

        // GET: Admin/Album
        public ActionResult All()
        {
            return View();
        }

        public async Task<ActionResult> ListAlbums([DataSourceRequest] DataSourceRequest request)
        {
            var albums = this.albumService.GetAll();

            var models = albums.ProjectTo<AlbumViewModel>().ToList();
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
